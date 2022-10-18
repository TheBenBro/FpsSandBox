using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public AnimationStateController animator;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public LayerMask blinkMask;
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
            
            velocity.y = -0.5f;
        }
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z).normalized;
        //velocity.y += gravity * Time.deltaTime;
        if (z == 0 && x==0) 
        { 
            isMoving = false;
            animator.StopWalking();
            speed = 0f;
        }

        else if(z > 0 || z < 0 || x > 0 || x < 0)
        {
            isMoving = true;
            animator.StartWalking();
        }

        if (Input.GetButtonDown("Jump") && isGrounded) {
            Debug.Log("Jump");
            rb.AddForce(new Vector3(0.0f,Mathf.Sqrt(jumpHeight * -2 * gravity),0.0f),ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Blink(move);
        }
     
        if(rb.velocity.magnitude < maxSpeed)
        {
             rb.position += (move * maxSpeed) * Time.deltaTime;
        }
    }

    void Blink(Vector3 direction_)
    {
        RaycastHit hit; 
        Physics.Raycast(transform.position, direction_, out hit, 3.0f, blinkMask,QueryTriggerInteraction.Ignore);
        if (hit.collider == null)
        {
            rb.position = transform.position + (direction_ * 3.0f);
        }
        else
        {
            rb.position = hit.point + (-direction_); ;
        }
    }
}
