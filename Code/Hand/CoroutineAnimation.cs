using System;
using System.Collections;
using UnityEngine;

namespace Code.Hand
{
    public class CoroutineAnimation
    {
        private readonly MonoBehaviour _target;
        private Coroutine _previous;
        public CoroutineAnimation(MonoBehaviour target)
        {
            _target = target;
        }

        public Coroutine Start(float duration, Action<float> changes)
        {
            return Start(duration, changes, () => false);
        }
        public Coroutine Start(float duration, Action<float> changes, Func<bool> when)
        {
            if(_previous!=null) 
                _target.StopCoroutine(_previous);
            _previous = _target.StartCoroutine(Animation(duration, changes, when));
            return _previous;
        }
        private IEnumerator Animation(float duration, Action<float> changes, Func<bool> when)
        {
            for (var i = .0f; i < 1; i += Time.deltaTime / duration)
            {
                if(when()) break;
                changes.Invoke(i);
                yield return null;
            }
        }

        public void OnDisable()
        {
            if(_previous!=null) 
                _target.StopCoroutine(_previous);
        }
    }
}
