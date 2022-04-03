using System.Collections;
using Code.Infrastructure.Services.Pause;
using UnityEngine;

namespace Code.Character.Health
{
    public class ContinuousDamage : MonoBehaviour, IPauseHandler
    {
        [SerializeField] private PlayerHealth _health;
        [SerializeField] private float _delay;
        [SerializeField] private int _amount;
        private WaitForSeconds _waitForSecond;
        private bool _paused;

        private void Start()
        {
            _waitForSecond = new WaitForSeconds(_delay);
            StartCoroutine(Damage());
        }

        private IEnumerator Damage()
        {
            while (true)
            {
                if (!_paused) 
                    _health.TakeDamage(_amount);
                yield return _waitForSecond;
            }
        }

        public void Unpause() => 
            _paused = false;

        public void Pause() =>
            _paused = true;
    }
}