using System;
using UnityEngine;

namespace Code.Character
{
    [RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
    public class Character : MonoBehaviour, ITakeable
    {
        [SerializeField] private CharacterAnimator _animator;
        private Collider _collider;
        public event Action Grabbed;
        public Rigidbody Rigidbody { get; private set; }
        
        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
        }
        public void StopMovement()
        {
            _collider.enabled = false;
            enabled = false;
            _animator.PlayDeath();
            Grabbed?.Invoke();
        }
    }
}
