namespace NetworkComputeFramework.RunMode
{
    public enum RunState
    {
        IDLE,

        LOAD_BEGIN,
        LOAD_DONE,

        MAP_BEGIN,
        MAP_DONE,

        REDUCE_BEGIN,
        REDUCE_DONE,

        FAILURE
        
    }
}
