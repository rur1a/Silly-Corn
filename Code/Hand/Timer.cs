using System;
using System.Collections;
using Code.Infrastructure.Services.Pause;
using TMPro;
using UnityEngine;

namespace Code.Hand
{
    [RequireComponent(typeof(TMP_Text))]
    public class Timer : MonoBehaviour, IPauseHandler
    {
        public event Action OnTimerEnd;
        [Min(0), SerializeField] private float _time;
        private TMP_Text _text;
        private bool _paused;

        private void Awake() => 
            _text = GetComponent<TMP_Text>();
        
        public void StartCount() => 
            StartCoroutine(GrabTime());

        private IEnumerator GrabTime()
        {
            float time = _time;
            while (time > 0)
            {
                if(!_paused)
                {
                    time -= Time.deltaTime;
                    UpdateView(time);
                }
                yield return null;
            }
            OnTimerEnd?.Invoke();
        }

        private void UpdateView(float time) => 
            _text.text = time.ToString();
        public void Unpause() => 
            _paused = false;
        public void Pause() => 
            _paused = true;

    }
}
