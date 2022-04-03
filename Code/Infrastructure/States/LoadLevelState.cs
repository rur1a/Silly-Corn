using Cinemachine;
using Code.Character.Health;
using Code.Hand;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Services;
using Code.Infrastructure.Services.PersistantProgress;
using Code.Infrastructure.StaticData;
using Code.Logic;
using Code.UI.Hud;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.States
{
    public class LoadLevelState : IPayloadState<string>
    {
        private const string SpawnPointTag = "SpawnPoint";
        private const string HandSpawnPointTag = "HandSpawnPoint";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistantProgressService _progressService;
        private readonly IStaticDataService _staticData;


        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory, IPersistantProgressService progressService, IStaticDataService staticData)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticData = staticData;
        }

        public void Enter(string payload)
        {
            _loadingCurtain.Show();
            _gameFactory.Cleanup(); 
            _sceneLoader.Load(payload, OnLoaded);
        }

        public void Exit() => 
            _loadingCurtain.Hide();

        private void OnLoaded()
        {
            InitLevelObjects();
            InformProjectsReaders();
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InformProjectsReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_progressService.Progress);
            }
        }

        private void InitLevelObjects()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            LevelStaticData levelStaticData = _staticData.ForLevel(sceneName);
            
            GameObject player = _gameFactory.CreateHero(at: levelStaticData.PlayerPosition);
            GameObject hand = _gameFactory.CreateHand(levelStaticData.HandPosition);
            
            InitSpawners(levelStaticData);
            InitUI(player, hand);
        }

        private void InitUI(GameObject player, GameObject hand)
        {
            GameObject hud = _gameFactory.CreateHud();
            player.GetComponent<HeroUI>()
                .Construct(player.GetComponentInChildren<IHealth>(), hud.GetComponentInChildren<HpBar>());
            CameraSwitch cameraSwitch = CameraFollow(hand, player);
            
            var miniGame = new MiniGame(cameraSwitch, hand.GetComponent<HandMovement>(), hud.GetComponentInChildren<Timer>());
            miniGame.Start();
        }


        private void InitSpawners(LevelStaticData levelData)
        {
            foreach (CharacterSpawnerData spawnerData in levelData.CharacterSpawners)
                _gameFactory.CreateSpawner(spawnerData.Position, spawnerData.Id, spawnerData.Type);
        }

        private CameraSwitch CameraFollow(GameObject hand, GameObject  player)
        {
            var cameraSwitch = Camera.main.GetComponent<CameraSwitch>();
            cameraSwitch.Construct(
                hand.GetComponentInChildren<CinemachineVirtualCamera>(),
                player.GetComponentInChildren<CinemachineVirtualCamera>());
            return cameraSwitch;
        }
    }
}