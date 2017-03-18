using NetworkComputeFramework.Worker;
using System;

namespace NetworkComputeFramework.RunMode
{

    public interface IRunMode
    {

        void Init(Action success, Action<Exception> failure, params object[] args);

        void Start(Action<object> success, Action<Exception> failure, params object[] args);

        event Action<RunState> OnStateChanged;

        WorkerPool WorkerPool { get; }

        void Stop();

        Exception LastException { get; }

    }
}
