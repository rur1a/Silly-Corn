using JetBrains.Annotations;
using UnityEngine;

namespace Code.Hero
{
    public class HeroSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _walking;

        [UsedImplicitly]
        public void Play() => 
            _walking.Play();

        [UsedImplicitly]
        public void Stop() => 
            _walking.Stop();
        
    }
}