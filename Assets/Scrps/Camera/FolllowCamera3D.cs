using UnityEngine;
using UnityEngine.InputSystem;

public class FolllowCamera3D : MonoBehaviour
{
    public Transform player; // arrastrá acá el Transform del jugador (el GameObject en sí, no el script)
    public float distance = 3f;

    [Header("Sensibilidad del mouse")]
    public float sensitivity = 20f;
    public float minPitch = -30f;
    public float maxPitch = 60f;

    [Header("Suavizado de posición")]
    public float smoothTime = 0.1f;
    Vector3 currentVelocity;

    [Header("Rotación del jugador")]
    [Tooltip("Desactivado: la cámara orbita totalmente libre e independiente del giro del jugador (modo tanque elegido).")]
    public bool followPlayerRotation = false;

    [Header("Posición inicial / de respawn")]
    [Tooltip("Ángulo de inclinación (pitch) con el que aparece la cámara detrás del jugador")]
    public float defaultPitch = 15f;

    float mouseYawOffset; // lo que el mouse suma/resta por encima de la rotación del jugador
    float pitch;           // rotación vertical (arriba/abajo), siempre libre por mouse

    void Start()
    {
        // Bloquea el cursor en el centro de la pantalla y lo oculta
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        transform.position = player.transform.position *(-defaultPitch);
        float baseYaw = player.eulerAngles.y;
        float yaw = baseYaw;
        Quaternion rotation = Quaternion.Euler(defaultPitch, yaw, 0f);
        ResetBehindPlayer();


    }

     /// Reubica la cámara detrás del jugador en la posición por defecto, sin suavizado (snap instantáneo).
    /// Llamar al arrancar el juego y cada vez que el jugador respawnea.
    /// </summary>
    public void ResetBehindPlayer()
    {
        pitch = defaultPitch;
        // Si la cámara sigue la rotación del jugador, el offset del mouse arranca en 0.
        // Si es independiente, el offset absoluto se iguala al yaw del jugador para que quede detrás de él.
        mouseYawOffset = followPlayerRotation ? 0f : player.eulerAngles.y;

       // SnapToTargetPosition();
    }

    void SnapToTargetPosition()
    {
        float baseYaw = followPlayerRotation ? player.eulerAngles.y : 0f;
        float yaw = baseYaw + mouseYawOffset;

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 targetPosition = player.position - (rotation * Vector3.forward * (-distance));

        transform.position = targetPosition; // sin SmoothDamp: aparece ahí directamente
        currentVelocity = Vector3.zero;
        transform.LookAt(player);
    }

    void Update()
    {
        // --- Lectura del mouse: se acumula como offset, independiente de la rotación del jugador ---
        if (Mouse.current == null) return; // evita NullReferenceException si no hay mouse detectado

        Vector2 delta = Mouse.current.delta.ReadValue();

        mouseYawOffset += delta.x * sensitivity * Time.deltaTime;
        pitch -= delta.y * sensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
    }

    /*void OnApplicationFocus(bool hasFocus)
    {
        // Al volver a la ventana (por ej. tras hacer clic en la Game View), re-bloquea el cursor
        if (hasFocus)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }*/

    void LateUpdate()
    {
        // El yaw final combina la rotación actual del jugador (si followPlayerRotation está activo)
        // con lo que el mouse haya agregado encima
        float baseYaw = followPlayerRotation ? player.eulerAngles.y : 0f;
        float yaw = baseYaw + mouseYawOffset;

        Quaternion rotation = Quaternion.Euler(pitch, -yaw, 0f);
        Vector3 targetPosition = player.position - (rotation * Vector3.forward * (-distance));

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
        transform.LookAt(player);
    }
}

