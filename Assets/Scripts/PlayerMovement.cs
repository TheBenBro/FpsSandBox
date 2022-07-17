using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpHeight = 3f;
    float speed;
    public float maxSpeed = 12f;
    public float dashMultiplier = 5f;
    public float dashDuration;
    public float dashCooldown;
    public float gravity = -9.81f;

    Vector3 velocity;
    bool isGrounded;
    bool canSlide = true;
    bool isMoving;
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics .CheckSphere(groundCheck.position,groundDistance,groundMask);
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (z == 0 && x==0) 
        { 
            isMoving = false;
            AnimationStateController.StopWalking();
            speed = 0f;
        }
        else if(z > 0 || z < 0 || x > 0 || x < 0)
        {
            isMoving = true;
            speed = Mathf.Lerp(speed, maxSpeed, Time.deltaTime);
            if (speed > maxSpeed - 1f)
            {
                speed = maxSpeed;
            }
            AnimationStateController.StartWalking();
        }
        Debug.Log(speed);
        AnimationStateController.SetVelocity(speed);
        Vector3 move = transform.right * x + transform.forward * z;
       
        controller.Move(move * speed * Time.deltaTime);
        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Slide();
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    IEnumerator SlideCoolDown(float speed_)
    {
        Debug.Log("Dashing");
        yield return new WaitForSeconds(dashDuration);
        for (float i = speed_;  speed > i; speed = Mathf.Lerp(speed, i, 150 * Time.deltaTime))
        {
            if(speed < 12.5)
            {
                speed = 12;
            }
        }
        yield return new WaitForSeconds(dashCooldown);
        Debug.Log("Can Dash Again");
        canSlide = true;
    }
    void Slide()
    {
        float tmp;
        tmp = speed;
        if (isGrounded && canSlide)
        {
            canSlide = false;
            speed = speed * dashMultiplier;
            StartCoroutine(SlideCoolDown(tmp));
        }
    }
}
