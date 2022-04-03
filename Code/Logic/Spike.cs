using Code.Hero;
using UnityEngine;

namespace Code.Logic
{
    public class Spike : MonoBehaviour
    {
        [SerializeField] private Transform _checkPoint;
        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.TryGetComponent(out HeroMovement _))
                other.collider.gameObject.transform.position = _checkPoint.position;
        }
    }
}
