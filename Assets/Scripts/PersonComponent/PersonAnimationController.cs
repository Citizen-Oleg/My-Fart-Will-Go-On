using UnityEngine;

namespace PersonComponent
{
    public class PersonAnimationController
    {
        private static readonly int IsRun = Animator.StringToHash("IsRun");

        private readonly Animator _animator;
        
        public PersonAnimationController(Animator animator)
        {
            _animator = animator;
            _animator.Play("Idle", -1, Random.Range(0f, 1f));
        }
        
        public void SetRun(bool isRun)
        {
            _animator.SetBool(IsRun, isRun);
        }
    }
}