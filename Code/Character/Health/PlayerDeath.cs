using Code.Hero;
using Code.Infrastructure.Services;
using Code.Infrastructure.Services.Pause;
using Code.Infrastructure.Services.PersistantProgress;
using UnityEngine;

namespace Code.Character.Health
{
    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _health;
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private HeroMovement _movement;
        
        private IPersistantProgressService _persistantProgressService;
        private IPauseService _pauseService;

        public void Construct(IPersistantProgressService persistantProgressService, IPauseService pauseService)
        {
            _persistantProgressService = persistantProgressService;
            _pauseService = pauseService;
        }

        private void Start() => 
            _health.HealthChanged += HealthChanged;

        private void OnDestroy() => 
            _health.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (_health.Current <= 0) 
                Die();
        }

        private void Die()
        {
            _animator.PlayDeath();
            _persistantProgressService.ClearProgress();
            _pauseService.Pause();            
        }
    }
}