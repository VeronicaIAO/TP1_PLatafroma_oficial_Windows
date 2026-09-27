using UnityEngine;
using UnityEngine.InputSystem;

public class Player3 : MonoBehaviour
{
    public float speed = 10f;
    [Tooltip("Velocidad de giro con A/D, en grados por segundo")]
    public float turnSpeed = 150f;
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
    Vector3 moveInput; // guarda el input leído en Update, se aplica en FixedUpdate

    public CharacterController controller;
    public Transform cam;
    float currentVelocity; 
    float smoothTime = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        jump = new Vector3(0.0f, 1.0f, 0.0f);

        // Evita que la física haga tambalear al personaje al chocar, pero deja libre el eje Y para poder girar con A/D
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
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
        float h;
        float v;
        moveInput = new Vector3 (v, 0, h);
        float targetAngle = Mathf.Atan2(moveInput.x, moveInput.z)*Mathf.Rad2Deg+ cam.localEulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.localEulerAngles.y, targetAngle, ref currentVelocity, smoothTime);

        

        if (Keyboard.current.wKey.IsPressed())
        {
            transform.position -=  moveInput*speed*Time.deltaTime;
        }

        if (Keyboard.current.sKey.IsPressed())
        {
            transform.position +=  moveInput*speed*Time.deltaTime;
        }

     /* if (Keyboard.current.aKey.IsPressed())
        {
            transform.position +=  moveInput*speed*Time.deltaTime;
            transform.rotation = Quaternion.Euler(0f, - angle, 0f);
        }

        if (Keyboard.current.dKey.IsPressed()) 
        {
            transform.position +=  moveInput*speed*Time.deltaTime;
            transform.rotation = Quaternion.Euler(0f,angle, 0f);
        }*/

      /*if (Keyboard.current.dKey.IsPressed())
        {
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * moveInput;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            transform.position +=  moveInput;
            controller.Move(moveDir*speed*Time.deltaTime);
        }
        
        if (Keyboard.current.aKey.IsPressed())
        {
            Vector3 moveDir = Quaternion.Euler(0f,-targetAngle, 0f) * (- moveInput);
            transform.rotation = Quaternion.Euler(0f,-angle, 0f);
            transform.position -=  moveInput;
            controller.Move(moveDir*speed*Time.deltaTime);
        }*/
 /*
        {
            float targetAngle = Mathf.Atan2(moveInput.x, moveInput .z)*Mathf.Rad2Deg+ cam.localEulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.localEulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * moveInput;
            controller.Move(moveDir*speed*Time.deltaTime);
        } */

        if (Keyboard.current.spaceKey.IsPressed() && isTouchingGround)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isTouchingGround = false;
        }
    }

  /*void FixedUpdate()
    {
        // --- Rotación con A/D: gira el propio cuerpo del jugador ---
        float turnAmount = moveInput.x * turnSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, turnAmount, 0f));

        // --- Avance/retroceso con W/S: siempre según hacia dónde mira el jugador ---
        Vector3 move = transform.forward * moveInput.y;
        Vector3 targetVelocity = move * speed;

        // Mantiene la velocidad vertical actual (gravedad, salto) y solo controla X/Z
        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
    } */
}

