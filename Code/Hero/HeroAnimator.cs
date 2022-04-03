using UnityEngine;

namespace Code.Hero
{
    public class HeroAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private readonly int _moveHash       = Animator.StringToHash("move");
        private readonly int _climbShelfHash = Animator.StringToHash("climbShelf");
        private readonly int _climb          = Animator.StringToHash("climb");
        private readonly int _death          = Animator.StringToHash("death");


        public void Move(float speed) => 
            _animator.SetFloat(_moveHash, speed);

        public void Climb(float speed) => 
            _animator.SetFloat(_climb, speed);

        public void ClimbOnShelf() => 
            _animator.SetTrigger(_climbShelfHash);

        public void PlayDeath() => 
            _animator.SetTrigger(_death);
    }
}
