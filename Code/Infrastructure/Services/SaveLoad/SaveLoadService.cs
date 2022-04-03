using Code.Data;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Services.PersistantProgress;
using UnityEngine;

namespace Code.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string Progress = "Progress";
        private readonly IPersistantProgressService _progressService;
        private readonly IGameFactory _gameFactory;

        public SaveLoadService(IPersistantProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
            {
                progressWriter.UpdateProgress(_progressService.Progress);
            }
            PlayerPrefs.SetString(Progress, _progressService.Progress.ToJson());
        }

        public PlayerProgress LoadProgress() => 
            PlayerPrefs.GetString(Progress)?.ToDeserialized<PlayerProgress>();
    }
}