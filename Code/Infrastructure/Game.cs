using Code.Infrastructure.Services;
using Code.Infrastructure.States;
using Code.Logic;

namespace Code.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine { get; }

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, ServiceLocator.Container);
        }
    }
}