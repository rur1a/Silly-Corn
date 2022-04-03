using System.Collections.Generic;
using Code.Infrastructure.Services;
using Code.Infrastructure.Services.Pause;
using Code.Infrastructure.Services.PersistantProgress;
using Code.Logic;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(Vector3 at);
        GameObject CreateHand(Vector3 transform);
        void CreateSpawner(Vector3 at, string id, CharacterType type);
        GameObject CreateCharacter(CharacterType type, Transform parent);
        List<ISavedProgress> ProgressWriters { get; }
        List<ISavedProgressReader> ProgressReaders { get; }
        List<IPauseHandler> PauseHandlers { get; }
        void Cleanup();
        GameObject CreateHud();
    }
}