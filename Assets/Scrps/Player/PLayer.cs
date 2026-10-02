using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PLayer : MonoBehaviour
{
    public Transform cameraTransform;

    public float moveSpeed = 5f;
    public float turnSpeed = 10f;

    public float jumpForce = 6f;
    public LayerMask groundMask;
    public float groundCheckDistance = 0.2f;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private bool jumpRequested;
    public bool isGrounded;

    public float gravedad = -20f;
    private Vector3 velocidadVertical;
    public float fuerzaSalto = 8f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // la rotación la manejamos nosotros, no la física

        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        ReadMovementInput();

        if (isGrounded && Keyboard.current.spaceKey.IsPressed())
        {
            velocidadVertical.y = fuerzaSalto;
            
            velocidadVertical.y += gravedad * Time.deltaTime;
            //moveDirection(velocidadVertical * Time.deltaTime);
            Vector3 velocity = rb.linearVelocity;
            //velocity.y = 1f;
            rb.linearVelocity = velocity;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        CheckGrounded();
        ApplyMovement();
        ApplyRotation();
       // ApplyJump();
       isGrounded = true;
    }

    private void ReadMovementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (cameraTransform == null)
        {
            moveDirection = Vector3.zero;
            return;
        }

        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = cameraTransform.right;
        camRight.y = 0f;
        camRight.Normalize();

        moveDirection = camForward * vertical + camRight * horizontal;
        if (moveDirection.sqrMagnitude > 1f)
        {
            moveDirection.Normalize();
        }
    }

    private void ApplyMovement()
    {
        Vector3 horizontalMove = moveDirection * moveSpeed;

        rb.linearVelocity = new Vector3(horizontalMove.x, rb.linearVelocity.y, horizontalMove.z);
    }

    private void ApplyRotation()
    {
        if (moveDirection.sqrMagnitude < 0.001f) return;

        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime));
    }

   /* private void ApplyJump()
    {

    }*/

    private void CheckGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance + 0.15f, groundMask);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * (groundCheckDistance + 0.15f));
    }
}
