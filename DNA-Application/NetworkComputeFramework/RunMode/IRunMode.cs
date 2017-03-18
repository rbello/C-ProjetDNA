using NetworkComputeFramework.Node;
using System;

namespace NetworkComputeFramework.RunMode
{

    public delegate void RunModeStateHandler(RunState newState);

    public interface IRunMode
    {
        void Init(Action success, Action<Exception> failure, params object[] args);

        void Start(Action success, Action<Exception> failure, params object[] args);

        event RunModeStateHandler OnStateChanged;

        Exception LastError { get; }

        WorkerPool WorkerPool { get; }
    }
}
