using UnityEngine;

/// <summary>
/// Colocar en el collider de muerte (el "Is Trigger" debe estar activado).
/// Al entrar el jugador, lo reaparece en la última posición de checkpoint guardada.
/// </summary>
[RequireComponent(typeof(Collider))]
public class Muerte : MonoBehaviour
{
    public string playerTag = "Player";

    [Tooltip("Arrastrá acá el objeto de la cámara (con el script FollowCamera3D) para que se reubique detrás del jugador al respawnear")]
    public FolllowCamera3D cameraFolllow;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        Rigidbody rb = other.attachedRigidbody;
        Vector3 respawnPos = CheckpointManager.Instance.CurrentRespawnPosition;

        if (rb != null)
        {
            // Se resetea la velocidad para que no "arrastre" el impulso que tenía al caer
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.position = respawnPos;
        }
        else
        {
            other.transform.position = respawnPos;
        }

        if (cameraFolllow != null)
        {
            cameraFolllow.ResetBehindPlayer();
        }

        // Acá podrías restar una vida, reproducir un sonido de muerte, etc.
    }
}

