using UnityEngine;

namespace Code.Logic
{
    public class ButtonSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audio;

        public void Play()
        {
            _audio.Play();
        }
    }
}
