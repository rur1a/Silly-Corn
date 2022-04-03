using Code.Infrastructure.Services;

namespace Code.Infrastructure.States
{
    public interface IGameStateMachine : IService
    {
        void Enter<T>() where T  : class, IState;
        void Enter<T, TU>(TU payload) where T : class, IPayloadState<TU>;
    }
}