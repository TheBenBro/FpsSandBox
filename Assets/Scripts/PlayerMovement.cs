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
    bool isSliding;
    bool isMoving;
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z).normalized;
        velocity.y += gravity * Time.deltaTime;
        if (z == 0 && x==0) 
        { 
            isMoving = false;
            speed = 0f;
        }
        else if(z > 0 || z < 0 || x > 0 || x < 0)
        {
            isMoving = true;
        }

       
        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Slide(move);
        }
        controller.Move((velocity + (move * maxSpeed)) * Time.deltaTime);
    }
    //SLIDE COOLDOWN
    IEnumerator SlideCoolDown(Vector3 move_)
    {
        float startTime = Time.time; 
        while (Time.time < startTime + dashDuration)
        {
            controller.Move(move_ * dashMultiplier * Time.deltaTime);
            
        }
        Debug.Log("CanSlide");
        canSlide = true;
        yield return null;
    }
    void Slide(Vector3 move_)
    {
        float tmp;
        tmp = speed;
        if (canSlide)
        {
            canSlide = false;
            StartCoroutine(SlideCoolDown(move_));
        }
    }
}
