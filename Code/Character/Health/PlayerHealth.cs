using System;
using Code.Data;
using Code.Infrastructure.Services.PersistantProgress;
using UnityEngine;

namespace Code.Character.Health
{
    public class PlayerHealth : MonoBehaviour, ISavedProgress, IHealth
    {
        [SerializeField] private int MaxHP;
        private State _state;
        public event Action HealthChanged;
        
        public int Current
        {
            get => _state.CurrentHp;
            set
            {
                if(Current == value) 
                    return;
                _state.CurrentHp = value;
                HealthChanged?.Invoke();
            }
        }
        public int Max
        {
            get => _state.MaxHP;
            set => _state.MaxHP = value;
        }

        private void Awake()
        {
            _state = new State();
            _state.MaxHP = MaxHP;
            _state.ResetHP();
        }

        public void TakeDamage(int damage)
        {
            if(Current<=0) 
                return;
            Current -= damage;
        }

        public void Kill() => 
            Current = 0;

        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.WorldData.State;
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.State.CurrentHp = Current;
            progress.WorldData.State.MaxHP = Max;
        }
    }
}