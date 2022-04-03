using System.Collections.Generic;
using UnityEngine;

namespace Code.Character
{
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private List<SpriteRenderer> _emotion;
        private readonly int _death = Animator.StringToHash("death");

        public void PlayDeath()
        {
            Instantiate(_emotion.GetRandom(), transform.position.AddY(4), Quaternion.identity, transform);
            _animator.SetTrigger(_death);
        }
    }
}