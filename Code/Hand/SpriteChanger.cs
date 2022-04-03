using UnityEngine;

namespace Code.Hand
{
    public class SpriteChanger : MonoBehaviour
    {
        [SerializeField] private Sprite _empty;
        [SerializeField] private Sprite _grabbed;
        [SerializeField] private SpriteRenderer _renderer;

        private void Start() => 
            _renderer.sprite = _empty;

        public void Change()
        {
            (_empty, _grabbed) = (_grabbed, _empty);
            _renderer.sprite = _empty;
        }
    }
}