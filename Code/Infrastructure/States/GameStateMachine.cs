using System;
using System.Collections.Generic;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Services;
using Code.Infrastructure.Services.PersistantProgress;
using Code.Infrastructure.Services.SaveLoad;
using Code.Logic;

namespace Code.Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type,IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, ServiceLocator services)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadMenuState)] = new LoadMenuState(this, sceneLoader, loadingCurtain),
                [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistantProgressService>(), services.Single<ISaveLoadService>()),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingCurtain, services.Single<IGameFactory>(), services.Single<IPersistantProgressService>(), services.Single<IStaticDataService>()),
                [typeof(GameLoopState)] = new GameLoopState(this)
            };
        }
        public void Enter<T>() where T  : class, IState
        {
            var state = ChangeState<T>();
            state.Enter();
        }

        public void Enter<T, TU>(TU payload) where T : class, IPayloadState<TU>
        {
            var state = ChangeState<T>();
            state.Enter(payload);
        }

        private T ChangeState<T>() where T : class, IExitableState
        {
            _activeState?.Exit();
            var state = GetState<T>();
            _activeState = state;
            return state;
        }

        private T GetState<T>() where T : class, IExitableState => 
            _states[typeof(T)] as T;
    }
}