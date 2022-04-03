using System;
using Code.Character;
using Code.Character.Health;
using UnityEngine;

namespace Code.Hand
{
    [RequireComponent(typeof(FixedJoint))]
    public class Grab : MonoBehaviour
    {
        [SerializeField] private SpriteChanger _changer;
        [SerializeField] private AudioSource _grabSound;
        private FixedJoint _fixedJoint;
        public event Action Grabbed;

        private void Awake()
        {
            _fixedJoint = GetComponent<FixedJoint>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.TryGetComponent(out ITakeable player) || _fixedJoint.connectedBody != null) 
                return;
            
            ConnectTarget(player);
            _grabSound.Play();
            _changer.Change();
            Grabbed?.Invoke();
        }

        private void ConnectTarget(ITakeable player)
        {
            player.Rigidbody.constraints = RigidbodyConstraints.None;
            _fixedJoint.connectedBody = player.Rigidbody;
            player.StopMovement();
        }

        public void DestroyTarget()
        {
            if (_fixedJoint.connectedBody == null)
                return;

            PlayerHealth? player = _fixedJoint.connectedBody.GetComponentInChildren<PlayerHealth>();
            if (player != null)
            {
                player.Kill();
                return;
            }
            
            Destroy(_fixedJoint.connectedBody.gameObject);
            _changer.Change();
        }
    }
}