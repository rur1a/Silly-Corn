using System.Collections.Generic;
using Code.Character.Health;
using Code.Hand;
using Code.Hero;
using Code.Infrastructure.Assets;
using Code.Infrastructure.Services;
using Code.Infrastructure.Services.Pause;
using Code.Infrastructure.Services.PersistantProgress;
using Code.Infrastructure.StaticData;
using Code.Logic;
using Code.Logic.EnemySpawners;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<IPauseHandler> PauseHandlers { get; } = new List<IPauseHandler>();
        
        private readonly ServiceLocator _services;
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        
        private GameObject _player;
        private HandMovement _handMovement;

        public GameFactory(IAssetProvider assets, IStaticDataService staticData, ServiceLocator services)
        {
            _assets = assets;
            _staticData = staticData;
            _services = services;
        }

        public GameObject CreateHero(Vector3 at)
        {
            string playerPath = AssetsPath.PlayerPath;
            _player = InstantiateRegistered(playerPath, at);
            _player.GetComponentInChildren<HeroMovement>().Construct(_services.Single<IGroundCheckerService>());
            _player.GetComponentInChildren<PlayerDeath>().Construct(_services.Single<IPersistantProgressService>(),
                _services.Single<IPauseService>());
            return _player;
        }

        public GameObject CreateHand(Vector3 at)
        {
            string handPath = AssetsPath.HandPath;
            GameObject hand = InstantiateRegistered(handPath, at);
            _handMovement = hand.GetComponent<HandMovement>();
            _handMovement.Construct(_player.transform);
            return hand;
        }

        public GameObject CreateHud() => 
            InstantiateRegistered(AssetsPath.HudPath);

        public GameObject CreateCharacter(CharacterType type, Transform parent)
        {
            CharacterStaticData characterData = _staticData.ForCharacter(type);
            GameObject character = Object.Instantiate(characterData.Prefab, parent.position, Quaternion.identity);
            _handMovement.AddTarget(character.transform);
            return character;
        }

        public void CreateSpawner(Vector3 at, string id, CharacterType type)
        {
            var spawner = InstantiateRegistered(AssetsPath.Spawner, at).GetComponent<SpawnPoint>();
            spawner.Construct(this);
            spawner.Id = id;
            spawner.Type = type;
        }
        
        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);
            ProgressReaders.Add(progressReader);
        }

        private void Register(IPauseHandler pauseHandler) =>
            PauseHandlers.Add(pauseHandler);

        private void RegisterPauseHandlers(GameObject gameObject)
        {
            foreach (IPauseHandler progressReader in gameObject.GetComponentsInChildren<IPauseHandler>())
                Register(progressReader);
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        private GameObject InstantiateRegistered(string gameObjectPath, Vector3 position)
        {
            GameObject gameObject = _assets.Instantiate(gameObjectPath, position);
            RegisterProgressWatchers(gameObject);
            RegisterPauseHandlers(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegistered(string gameObjectPath)
        {
            GameObject gameObject = _assets.Instantiate(gameObjectPath);
            RegisterProgressWatchers(gameObject);
            RegisterPauseHandlers(gameObject);
            return gameObject;
        }
    }
}