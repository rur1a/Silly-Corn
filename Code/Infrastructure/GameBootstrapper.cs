using Code.Infrastructure.States;
using Code.Logic;
using UnityEngine;

namespace Code.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;
        private Game _game; 
        private void Awake()
        {
            _game = new Game(this, Instantiate(_loadingCurtain));
            _game.StateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}
