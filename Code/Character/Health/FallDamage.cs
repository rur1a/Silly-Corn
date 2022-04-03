using Code.Hero;
using UnityEngine;

namespace Code.Character.Health
{ 
    public class FallDamage : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private HeroMovement _movement;
        [SerializeField] private float _damageMultiplier;

        private bool _isFalling;
        private float _startHeight;
        private bool EndFalling => !Grounded == false && _isFalling;
        private bool StartFalling => !Grounded && _isFalling == false;
        private bool Grounded => _movement.Grounded;

        private void Update()
        {
            if (StartFalling)
            {
                _startHeight = _movement.transform.position.y;
                _isFalling = true;
            }
            else if (EndFalling)
            {
                float height = _startHeight - _movement.transform.position.y;
                _playerHealth.TakeDamage((int)(_damageMultiplier*height));
                _isFalling = false;
            }
        }
    }
}