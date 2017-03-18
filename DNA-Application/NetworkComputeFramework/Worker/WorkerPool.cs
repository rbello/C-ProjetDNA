using NetworkComputeFramework.Data;
using NetworkComputeFramework.MapReduce;
using NetworkComputeFramework.Node;
using System;
using System.Collections.Generic;
using NetworkComputeFramework.RunMode;
using System.Collections;
using System.Threading;

namespace NetworkComputeFramework.Worker
{
    public class WorkerPool : IEnumerable<IWorker>
    {

        public event Action<INode> OnNodeConnected;
        public event Action<INode> OnNodeDisconnected;
        public event Action<string, LogLevel> OnWorkerPoolMessage;

        IList<INode> nodes = new List<INode>();
        private Action<RunState> changeStateFunc;

        public int WorkersCount { get; protected set; }

        public WorkerPool(Action<RunState> changeStateFunc)
        {
            this.changeStateFunc = changeStateFunc;
        }

        public void AddNode(INode node)
        {
            if (nodes.Contains(node))
                throw new ArgumentException("Node allready exists in worker pool");
            nodes.Add(node);
            WorkersCount += node.Workers.Count;
            OnNodeConnected?.Invoke(node);
        }

        public IEnumerable<IWorker> Workers
        {
            get { return this; }
        }

        public void Process<T>(Job<T> job)
        {
            new Thread(delegate ()
            {
                ProcessSynch(job);
            }).Start();
        }

        protected void ProcessSynch<T>(Job<T> job)
        {
            // Change running state
            changeStateFunc.Invoke(RunState.MAPPING_DATA);
            // Compute chunk length
            int chunkLength = (int)(job.DataReader.Length / WorkersCount);
            chunkLength = 150000;
            // Create mapper
            IMapper<T> mapper = job.CreateMapper(chunkLength);
            // Logs
            OnWorkerPoolMessage?.Invoke("Data length: " + mapper.DataLength + " records", LogLevel.Info);
            OnWorkerPoolMessage?.Invoke("Chunk length: " + mapper.ChunkLength + " records (remains " 
                + mapper.ChunkRemains + ")", LogLevel.Verbose);
            OnWorkerPoolMessage?.Invoke("Chunk count: " + mapper.ChunkCount, LogLevel.Info);

            int i = 0;
            foreach (DataChunk<T> chunk in mapper)
            {
                IWorker worker;
                while ((worker = GetAvailableWorker()) == null) {
                    Thread.Sleep(100);
                }
                ++i;
                OnWorkerPoolMessage?.Invoke("Give chunk " + i + " to " + worker + " (length: " + chunk.Data.Length + ")", LogLevel.Debug);
                new Thread(new ParameterizedThreadStart(delegate (object id)
                {
                    try
                    {
                        worker.Execute(chunk, job.CreateReducer());
                        OnWorkerPoolMessage?.Invoke(worker + " has finished chunk" + id, LogLevel.Debug);
                    }
                    catch (Exception ex)
                    {
                        OnWorkerPoolMessage?.Invoke("Exception in worker " + worker + " : " + ex.GetType().Name
                            + " - " + ex.Message, LogLevel.Error);
                    }
                    finally
                    {
                        worker.Available = true;
                    }
                })).Start(i);
            }

            OnWorkerPoolMessage?.Invoke("All data was distributed!", LogLevel.Info);
        }

        public IWorker GetAvailableWorker()
        {
            foreach (IWorker worker in Workers)
            {
                if (worker.Available)
                {
                    worker.Available = false;
                    return worker;
                }
            }
            return null;
        }

        public IEnumerator<IWorker> GetEnumerator()
        {
            foreach (INode node in nodes)
            {
                foreach (IWorker worker in node.Workers)
                {
                    yield return worker;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
