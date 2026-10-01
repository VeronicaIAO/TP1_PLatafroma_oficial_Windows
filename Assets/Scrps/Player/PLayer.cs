using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PLayer : MonoBehaviour
{
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;        
    private Vector3 PlayerMovementInput;
    public LayerMask FloorMask;
    public Transform FeetTransform;
    public Transform cam;
    public Rigidbody rb;
    public float speed;
    public float jumpForce;
    public bool isGrounded;
    

    // Update is called once per frame
    void Update()
    {
        isGrounded = true;
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        MovePlayer();
    }

    private void MovePlayer()
    {
        isGrounded = true;
        speed = +speed;
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput)*speed;
        rb.linearVelocity = new Vector3(MoveVector.x, rb.linearVelocity.y, MoveVector.z);

        if (Keyboard.current.spaceKey.wasPressedThisFrame &&  isGrounded == true)
        {
            speed = +speed;
            rb.AddForce(Vector3.up* jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        Vector3 moveDir = Vector3.zero;

        float targetAngle = Mathf.Atan2(PlayerMovementInput.x, PlayerMovementInput.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            speed = -speed;
            moveDir = Quaternion.Euler(0f, -targetAngle, 0f) *(-Vector3.back);
        }
    }
}
