using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    bool isWalking;
    bool gunPickedUp = false;
   // static int VelocityHash;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //VelocityHash = Animator.StringToHash("Velocity");
    }
    private void Update()
    {
        isWalking = animator.GetBool("IsWalking");
        gunPickedUp = animator.GetBool("gunPickedUp");
    }
    public static void SetVelocity(float velocity_)
    {
       // animator.SetFloat("Velocity", velocity_);
    }
    public void SetGunPickedUp(bool gun_)
    {
        gunPickedUp=gun_;
    }

    public void StartWalking()
    {
        if (!isWalking && !gunPickedUp)
        {
            animator.SetBool("IsWalking", true);
            animator.SetBool("gunPickedUp", false);
        }
        else if (!isWalking && gunPickedUp)
        {
            animator.SetBool("IsWalking", true);
            animator.SetBool("gunPickedUp", true);
        }

    }
    public void StopWalking()
    {
        if (isWalking && !gunPickedUp)
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("gunPickedUp", false);
        }
        else if (isWalking && gunPickedUp)
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("gunPickedUp", true);
        }
    }
    public void PickupAnimation()
    {
        if (gunPickedUp)
        {
            animator.SetBool("gunPickedUp", true);
        }
        if (!gunPickedUp)
        {
            animator.SetBool("gunPickedUp", false);
        }
    }

}
