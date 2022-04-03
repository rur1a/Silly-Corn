using Code.Data;
using Code.Infrastructure.Services.PersistantProgress;
using Code.Infrastructure.Services.SaveLoad;

namespace Code.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistantProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistantProgressService progressService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.LevelName);
        }

        public void Exit() { }

        private void LoadProgressOrInitNew() => 
            _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();

        private PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress("Level_Design");
            progress.WorldData.State.MaxHP = 100;
            progress.WorldData.State.ResetHP();
            return progress;
        }
    }
}