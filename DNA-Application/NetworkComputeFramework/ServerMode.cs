using System;

namespace NetworkComputeFramework
{
    public class ServerMode : AbstractRunMode
    {
        private int portNumber;
        private int localThreadsCount;

        protected override void Start(params object[] args)
        {
            portNumber = Convert.ToInt32(args[0]);
            localThreadsCount = Convert.ToInt32(args[1]);
        }

        public void LoadAndStartProcessingFile(string fileName, string jobName, Action success, Action<Exception> failure)
        {
            success();
        }
    }
}
