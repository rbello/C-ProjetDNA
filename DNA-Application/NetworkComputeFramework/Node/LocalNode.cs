using NetworkComputeFramework.Worker;
using System;

namespace NetworkComputeFramework.Node
{
    public class LocalNode : INode
    {

        public LocalNode(int localThreadsCount)
        {
            Workers = new ThreadWorker[localThreadsCount];
            for (int i = 0; i < localThreadsCount; ++i)
            {
                Workers[i] = new ThreadWorker(this, i);
            }
        }

        public IWorker[] Workers { get; protected set; }

        public void Init()
        {
            
        }

        public override string ToString()
        {
            return "LocalNode[127.0.0.1][" + Workers.Length + " workers]";
        }
    }
}
