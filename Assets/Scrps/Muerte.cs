using UnityEngine;

/// <summary>
/// Colocar en el collider de muerte (el "Is Trigger" debe estar activado).
/// Al entrar el jugador, lo reaparece en la última posición de checkpoint guardada.
/// </summary>
[RequireComponent(typeof(Collider))]
public class Muerte : MonoBehaviour
{
    public FolllowCamera3D cameraFolllow;
    public Transform player;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Vector3 respawnPos = CheckpointManager.Instance.CurrentRespawnPosition;
        death = GetComponent<BoxCollider>();

        if (death != null)
        {
            // Se resetea la velocidad para que no "arrastre" el impulso que tenía al caer
            player.linearVelocity = Vector3.zero;
            player.angularVelocity = Vector3.zero;
            player.position = respawnPos;
        }
        else
        {
            other.transform.position = respawnPos;
        }

    }
}

