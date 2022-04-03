using System.Collections;
using UnityEngine;

namespace Code.Hero
{
    public class  ClimbingAnimationTrigger : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        private Coroutine _coroutine;
        private HeroMovement _heroMovement;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out _heroMovement)) 
                _coroutine = StartCoroutine(Climbing());
        }
        private IEnumerator Climbing()
        {
            yield return new WaitUntil(()=>
                UnityEngine.Input.GetKeyDown(KeyCode.E));
            _heroMovement.StartClimb(_target.position);
        }

        private void OnTriggerExit(Collider other)
        {
            if(_coroutine!=null) 
                StopCoroutine(_coroutine);
        }
    }
}

