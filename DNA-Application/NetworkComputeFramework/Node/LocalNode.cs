using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkComputeFramework.Node
{
    public class LocalNode : Node
    {
        private int localThreadsCount;

        public LocalNode(int localThreadsCount)
        {
            this.localThreadsCount = localThreadsCount;
        }

        public void Start()
        {
            
        }

        public override string ToString()
        {
            return "LocalNode[127.0.0.1][" + localThreadsCount + " workers]";
        }
    }
}
