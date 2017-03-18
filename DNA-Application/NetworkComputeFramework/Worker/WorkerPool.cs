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

        public void Process<T>(Job<T> job, Action<object> onSuccess, Action<Exception> onFailure)
        {
            new Thread(delegate ()
            {
                ProcessSynch(job, onSuccess, onFailure);
            }).Start();
        }

        protected void ProcessSynch<T>(Job<T> job, Action<object> onSuccess, Action<Exception> onFailure)
        {
            // Change running state
            changeStateFunc.Invoke(RunState.MAP_BEGIN);
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

            while (mapper.Active)
            {

                // Handle interruption
                if (job.Interrupted)
                {
                    //TODO: Improve interruption on chunk polling
                    onFailure.Invoke(new ThreadInterruptedException());
                    return;
                }

                // Get next chunk of data
                DataChunk<T> chunk = mapper.NextChunk();
                if (chunk == null)
                {
                    // No chunk is available but the mapped is active
                    changeStateFunc.Invoke(RunState.MAP_DONE);
                    Thread.Sleep(100);
                    continue;
                }

                // Chunk is booked with worker
                chunk.State = ChunkState.Booked;

                // Find an available worker
                IWorker worker;
                while ((worker = GetAvailableWorker()) == null)
                {
                    Thread.Sleep(100);
                    // Handle interruption
                    if (job.Interrupted)
                    {
                        //TODO: Improve interruption on worker polling
                        onFailure.Invoke(new ThreadInterruptedException());
                        return;
                    }
                }

                // Assign chunk to worker
                OnWorkerPoolMessage?.Invoke("Give chunk " + chunk.Id + " to " + worker + " (length: " + chunk.Data.Length + ")", LogLevel.Debug);

                new Thread(new ParameterizedThreadStart(delegate (object data)
                {
                    DataChunk<T> chunk2 = (DataChunk<T>)data;
                    try
                    {
                        // Execute job and store result
                        job.Results.Add(chunk2.Id, worker.Execute(chunk, job));
                        if (job.Interrupted) return;
                        OnWorkerPoolMessage?.Invoke(worker + " has finished reducing chunk " + chunk2.Id, LogLevel.Debug);
                        chunk2.State = ChunkState.Done;
                    }
                    catch (Exception ex)
                    {
                        OnWorkerPoolMessage?.Invoke("Exception in worker " + worker + " : " + ex.GetType().Name
                            + " - " + ex.Message + " (chunk " + chunk2.Id + ")", LogLevel.Error);
                        OnWorkerPoolMessage?.Invoke(ex.ToString(), LogLevel.Debug);
                        chunk2.State = ChunkState.Available;
                    }
                    finally
                    {
                        worker.Available = true;
                    }
                })).Start(chunk);
            }

            // Clear memory used by mapper
            mapper.Dispose();

            // Change running state
            OnWorkerPoolMessage?.Invoke("All chunks are reduced, reducing results...", LogLevel.Info);
            changeStateFunc.Invoke(RunState.REDUCE_BEGIN);

            try
            {
                object finalResult = job.CreateReducer().Reduce(job.Results);
                // Process is finished
                onSuccess.Invoke(finalResult);
            }
            catch (Exception ex)
            {
                onFailure.Invoke(ex);
            }

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
