using UnityEngine;

/// <summary>
/// Colocar en cada checkpoint. El collider debe tener "Is Trigger" activado.
/// Al entrar el jugador, calcula el centro del collider y lo guarda como punto de reaparición.
/// </summary>
[RequireComponent(typeof(Collider))]
public class Checkpoint : MonoBehaviour
{
    [Tooltip("Tag que debe tener el jugador (por defecto 'Player')")]
    public string playerTag = "Player";

    [Tooltip("Se activa visualmente/sonoramente una sola vez, opcional")]
    public bool activated = false;

    Collider col;
    void Awake()
    {
        col = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        // Posición central del collider del checkpoint (funciona con Box, Sphere, Capsule, etc.)
        Vector3 centerPosition = col.bounds.center;

        CheckpointManager.Instance.SetRespawnPosition(centerPosition);
        activated = true;

        // Acá podrías disparar un sonido, un efecto visual, etc.
    }
}