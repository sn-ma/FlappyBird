using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBirdClone.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimController : MonoBehaviour
    {
        private Animator animator;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void SetIsFlying(bool isFlying)
        {
            animator.SetBool("isFlying", isFlying);
        }

        public void SetDead()
        {
            animator.SetTrigger("die");
        }
    }
}