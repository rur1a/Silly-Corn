namespace Code.Infrastructure.States
{
    public interface IState : IExitableState
    {
        void Enter();
    }
    public interface IPayloadState<in T> : IExitableState
    {
        void Enter(T payload);
    }

    public interface IExitableState
    {
        void Exit();
    }
}