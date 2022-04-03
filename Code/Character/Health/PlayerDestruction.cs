using System.Collections.Generic;
using UnityEngine;

namespace Code.Character.Health
{
    public class PlayerDestruction : MonoBehaviour
    {
        [SerializeField] private float _force, _upwards, _radius;
        [SerializeField] private PlayerHealth _health;
        [SerializeField] private List<Rigidbody> _parts;

        private Rigidbody _current;
        private int _previousHealth;
        

        private void Start()
        {
            _previousHealth = _health.Max;
            _parts.Reverse();
            _health.HealthChanged += ShowDamage;
        }

        private void OnDestroy()
        {
            _health.HealthChanged -= ShowDamage;
        }

        private void ShowDamage()
        {
            if (_health.Current <= 0)
            {
                Explode(_parts.Count);
                return;
            }
            int intAmount = GetProportionalAmount();
            if (intAmount < 1) return;
            Explode(intAmount);
            _previousHealth = _health.Current;
        }

        private int GetProportionalAmount()
        {
            float amount = (1.0f * _parts.Count * (_previousHealth - _health.Current) / _health.Max);
            var intAmount = (int)(amount + .5f);
            return intAmount;
        }

        private void Explode(int amount)
        {
            int endIndex = amount < _parts.Count ? amount : _parts.Count;
            for (var i = 0; i < endIndex; ++i)
            {
                _current = _parts[i];
                _current.isKinematic = false;
                _current.useGravity = true;
                _current.AddExplosionForce(_force, transform.position, _radius, _upwards, ForceMode.Impulse);
                Destroy(_current.gameObject, 2);
            }
            _parts.RemoveRange(0, endIndex);
        }
    }
}

