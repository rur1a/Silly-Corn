using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Infrastructure.Services.Pause;
using UnityEngine;

namespace Code.Hand
{
    public class HandMovement : MonoBehaviour, IPauseHandler
    {
        public event Action Return;
        
        [SerializeField] private float _speed;
        [SerializeField] private float _safeDistance;
        [SerializeField] private Grab _grab;
        
        private Transform _mainTarget;
        private List<Transform> _sideTargets = new List<Transform>();
        private Vector3 _origin;
        private bool _paused;

        public void Construct(Transform player)
        {
            _mainTarget = player;
        }

        public void AddTarget(Transform position) =>
            _sideTargets.Add(position);
        
        
        public void StartGrabbing()
        {
            _origin = transform.position;
            _grab.Grabbed += () =>
            {
                StopAllCoroutines();
                StartCoroutine(MoveBack());
            };
             StartCoroutine(MoveQueue());
        }
        
        private IEnumerator MoveQueue()
        {
            yield return Follow(_mainTarget);
            Transform character = FindNearestCharacter();
            if (character!=null)
                yield return Move(to: character.position);
            yield return MoveBack();
        }

        private Transform FindNearestCharacter()
        {
            Transform character = _sideTargets
                .Where(c => c != null)
                .OrderBy(c => Vector3.Distance(transform.position, c.position))
                .FirstOrDefault();
            return character;
        }

        private IEnumerator MoveBack()
        {
            yield return StartCoroutine(Move(_origin));
            _grab.DestroyTarget();
            Return?.Invoke();
        }

        private IEnumerator Move(Vector3 to)
        {
            Vector3 from = transform.position;
            float duration = Vector3.Distance(from, to) / _speed;
            
            for (float i = 0; i < 1; i += Time.deltaTime / duration)
            {
                if (!_paused)
                {
                    transform.position = Vector3.Lerp(from, to, i);
                }
                yield return null;
            }
        }

        private IEnumerator Follow(Transform target)
        {
            Vector3 from = transform.position;
            Vector3 to = target.position + 10 * transform.position.DirectionTo(target.position);
            float duration = Vector3.Distance(from, target.position) / _speed;
            
            for (float i = 0; i < 1; i += Time.deltaTime / duration)
            {
                if(!_paused)
                {
                    transform.position = 
                    transform.position.z < target.position.z - _safeDistance 
                        ? target.position.Where(target.position.x, target.position.y, Mathf.Lerp(@from.z, to.z, i)) 
                        : transform.position.Where(z: Mathf.Lerp(@from.z, to.z, i));
                    
                }
                yield return null;
            }
        }
        public void Unpause() => 
            _paused = false;
        public void Pause() => 
            _paused = true;
    }
}