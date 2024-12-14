using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public void ChangeAnimation(string animationName)
    {
        animator.CrossFade(animationName, 0.1f);
    }

}
