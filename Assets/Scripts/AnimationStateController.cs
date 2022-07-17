using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    static Animator animator;
    static int VelocityHash;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //VelocityHash = Animator.StringToHash("Velocity");
    }
    public static void SetVelocity(float velocity_)
    {
        animator.SetFloat("Velocity", velocity_);
    }

    public static void StartWalking()
    {
        animator.SetBool("IsWalking", true);
    }
    public static void StopWalking()
    {
        animator.SetBool("IsWalking", false);
    }
}
