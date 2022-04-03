using Code.Logic;

namespace Code.Infrastructure.States
{
    public class LoadMenuState : IPayloadState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;

        public LoadMenuState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }
        public void Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLoaded);
            _loadingCurtain.Show();
        }

        private void OnLoaded() => 
            _gameStateMachine.Enter<GameLoopState>();

        public void Exit() => 
            _loadingCurtain.Hide();
    }
}