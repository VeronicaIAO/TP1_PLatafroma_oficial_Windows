using System;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public float gravity = -20f;
    private Vector3 verticalVelocity;
    public float fuerzaSalto = 8f; 

    public Vector3 respawnPoint;

    public float respawnGuardTime = 0.2f; // evita que Die() se dispare dos veces seguidas
    private bool isRespawning = false;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
        respawnPoint = transform.position;
    }

    void Update()
    {
        ReadMovementInput();

        if (isGrounded == true && Keyboard.current.spaceKey.IsPressed())
        {
            isGrounded = false;
            verticalVelocity.y = fuerzaSalto;
            
            verticalVelocity.y += gravity * Time.deltaTime;
            Vector3 velocity = rb.linearVelocity;
            rb.linearVelocity = velocity;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
           
        }
    }

    void FixedUpdate()
    {
        ApplyMovement();
        ApplyRotation();
    }

    void OnCollisionEnter(Collision other)
    {
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


     public void SetSpawnPoint(Vector3 position)
    {
        respawnPoint = position;
    }
 
    public void Die()
    {
        if (isRespawning) return; // corta cualquier doble disparo del mismo frame/trigger
        isRespawning = true;

        Debug.Log($"[PLayer] Die() llamado en '{name}' (instance ID {GetInstanceID()}). Respawneando en: {respawnPoint}");

        rb.linearVelocity = Vector3.zero;
        rb.position = respawnPoint;
        Physics.SyncTransforms();
 
        Invoke(nameof(ClearRespawnGuard), respawnGuardTime);
    }
 
    private void ClearRespawnGuard()
    {
        isRespawning = false;
    }
}
