using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    public float speed = 10.0f;
    public float jumpSpeed = 10.0f;
    public float gravity = -20.0f;

    private Vector3 moveDirection = Vector3.zero;

    public Transform cam;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    public bool isFalling = false;
    public bool isGrounded = false;
    public bool isJumping = false;
    public bool isRunning = false;
    public bool isStanding = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialPosition.y += 50;
    }
    void Update() {
        float xDisplacement = Input.GetAxis("Horizontal");
        float zDisplacement = Input.GetAxis("Vertical");
        moveDirection.y -= gravity * Time.deltaTime;
        isGrounded = true;
        isStanding = true;        
        isJumping = false;
        isGrounded = controller.isGrounded;
        if (isFalling)
            xDisplacement = zDisplacement = 0.0f;
        
        if (controller.isGrounded) 
        {
            isFalling = false;
            moveDirection = new Vector3(xDisplacement * speed, 0, zDisplacement * speed);
            if (Keyboard.current.spaceKey.IsPressed())
            {
                moveDirection.y = jumpSpeed;
                isJumping = true;
                isRunning = false;
            }                          
        }
        else
        {
            moveDirection = new Vector3(xDisplacement * speed, moveDirection.y, zDisplacement * speed);
        }

        if (moveDirection.x != 0.0f || moveDirection.z != 0.0f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            moveDir.y = moveDirection.y;
            moveDir.x *= speed;
            moveDir.z *= speed; 
            controller.Move(moveDir * Time.deltaTime);
            if (controller.isGrounded)
            {
                isRunning = true;
                isStanding = false;
            }                
            else
            {
                isRunning = false;
                isStanding = true;
            }                
        }
        else
        {
            controller.Move(moveDirection * Time.deltaTime);            
            if (controller.isGrounded)
            {
                isRunning = false;
                isStanding = true;
           }                
            else
            {
                isRunning = false;
                isStanding = true;

            }
        }
        if (transform.position.y < -20)
        {
            transform.position = initialPosition;
            transform.rotation = initialRotation;
            isFalling = true;
            isStanding = false;
        }
    }
}
