using System;

namespace NetworkComputeFramework
{

    public delegate void RunModeStateHandler(RunState newState);

    public interface IRunMode
    {
        //void Start(params object[] args);

        void Start(Action success, Action<Exception> failure, params object[] args);

        event RunModeStateHandler OnStateChanged;

        Exception LastError { get; }
    }
}
