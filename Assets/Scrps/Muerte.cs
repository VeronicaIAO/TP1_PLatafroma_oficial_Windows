using UnityEngine;

/// <summary>
/// Colocar en el collider de muerte (el "Is Trigger" debe estar activado).
/// Al entrar el jugador, lo reaparece en la última posición de checkpoint guardada.
/// </summary>
[RequireComponent(typeof(Collider))]
public class Muerte : MonoBehaviour
{
    public Transform player;
    public string playerTag = "Capsule";

    void OnTriggerEnter(Collider Muerte)
    {
        if (!Muerte.CompareTag(playerTag)) return;

        Rigidbody rb = Muerte.attachedRigidbody;
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
            Muerte.transform.position = respawnPos;
        }

    }
}