using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    public Transform cam;
    public GameManager GameManager;

    [Header("Movimiento")]
    public float speed = 10.0f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    [Header("Salto y Gravedad")]
    public float jumpHeight = 2.0f;
    public float gravity = -20.0f; // La gravedad debe ser negativa
    private float verticalVelocity;

    [Header("Estados")]
    public bool isFalling = false;
    public bool isGrounded = false;
    public bool isJumping = false;
    public bool isRunning = false;

    public event Action OnJumpButtonPressed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        // Guardar la posición inicial en el GameManager
        GameManager.Instance.SetCheckpoint(transform.position);
    }

    void Update()
    {
        // 1. Detección de suelo
        isGrounded = controller.isGrounded;

        if (isGrounded && verticalVelocity < 0)
        {
            // Mantiene al jugador pegado al suelo si camina por rampas
            verticalVelocity = -2f; 
            isJumping = false;
        }

        // 2. Movimiento Horizontal (Input)
        float xDisplacement = Input.GetAxis("Horizontal");
        float zDisplacement = Input.GetAxis("Vertical");
        Vector3 inputDirection = new Vector3(xDisplacement, 0f, zDisplacement).normalized;
        Vector3 moveDir = Vector3.zero;

        if (inputDirection.magnitude >= 0.1f)
        {
            // Calcular ángulo hacia la cámara
            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Dirección frontal ajustada
            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // Lógica de correr con Shift
            float currentSpeed = speed;
            if (Keyboard.current.shiftKey.isPressed && isGrounded)
            {
                currentSpeed *= 2f;
                isRunning = true;
            }
            else
            {
                isRunning = true;
            }

            moveDir *= currentSpeed;
        }
        else
        {
            isRunning = false;
        }

        // 3. Salto
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            // Fórmula cinemática para un salto perfecto según la altura deseada
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isJumping = true;
            OnJumpButtonPressed?.Invoke();
        }

        // 4. Aplicar Gravedad
        verticalVelocity += gravity * Time.deltaTime;
        moveDir.y = verticalVelocity;

        // 5. Mover al jugador
        controller.Move(moveDir * Time.deltaTime);

        // 6. Reseteo por caída al vacío (Fallback de seguridad)
        if (transform.position.y < -10f)
        {
            controller.enabled = false;
            transform.position = GameManager.Instance.currentSpawnPoint;
            verticalVelocity = 0f;
            controller.enabled = true;
        }
    }
}