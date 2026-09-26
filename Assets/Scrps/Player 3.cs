using UnityEngine;
using UnityEngine.InputSystem;

public class Player3 : MonoBehaviour
{
    public float speed = 10f;
    public Vector3 jump;
    public float jumpForce = 1.0f;
    public bool isTouchingGround = true;

    [Header("Raycast de suelo")]
    [Tooltip("Distancia extra del rayo por debajo de la base de la cápsula")]
    public float groundCheckDistance = 0.2f;
    [Tooltip("Capas consideradas 'suelo'. Dejar en Everything si no usás layers todavía")]
    public LayerMask groundLayer = ~0;

    Rigidbody rb;
    CapsuleCollider capsule;
    Vector2 moveInput; // guarda el input leído en Update, se aplica en FixedUpdate

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        jump = new Vector3(0.0f, 1.0f, 0.0f);

        // Evita que la física rote el cuerpo al chocar (opcional pero recomendable)
        rb.freezeRotation = true;
    }

    void CheckGround()
    {
        // Origen del rayo: el punto más bajo de la cápsula
        float halfHeight = capsule != null ? capsule.height / 2f : 1f;
        Vector3 origin = transform.position + Vector3.up * 0.05f; // pequeño offset para no arrancar dentro del suelo
        float rayLength = halfHeight + groundCheckDistance;

        isTouchingGround = Physics.Raycast(origin, Vector3.down, rayLength, groundLayer);

        // Visualización en el editor (Scene view)
        Debug.DrawRay(origin, Vector3.down * rayLength, isTouchingGround ? Color.green : Color.red);
    }

    void Update()
    {
        CheckGround();

        // --- Lectura de input (siempre en Update) ---
        float h = 0f, v = 0f;

        if (Keyboard.current.wKey.IsPressed()) v -= 1f;   // adelante
        if (Keyboard.current.sKey.IsPressed()) v += 1f;   // atrás
        if (Keyboard.current.dKey.IsPressed()) h += 1f;   // derecha
        if (Keyboard.current.aKey.IsPressed()) h -= 1f;   // izquierda

        moveInput = new Vector2(h, v);

        if (Keyboard.current.spaceKey.IsPressed() && isTouchingGround)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isTouchingGround = false;
        }
    }

    void FixedUpdate()
    {
        // --- Aplicación del movimiento (siempre en FixedUpdate, junto con la física) ---
        Vector3 move = (Vector3.forward * moveInput.y + Vector3.right * moveInput.x).normalized;
        Vector3 targetVelocity = move * speed;

        // Mantiene la velocidad vertical actual (gravedad, salto) y solo controla X/Z
        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
    }
}