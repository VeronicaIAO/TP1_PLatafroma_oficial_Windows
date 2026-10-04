using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public int order = 0;

    public GameObject activeIndicator;

    public PLayer player;

    public Checkpoint activeCheckpoint;

    void Start()
    {
        player = FindFirstObjectByType<PLayer>();

        Debug.Log($"[Checkpoint] '{name}' Start() ejecutado. Player encontrado: {player != null}");

        if (activeIndicator != null)
            activeIndicator.SetActive(false);
    }

    void OnTriggerSta(Collider other)
    {
        Debug.Log($"[Checkpoint] '{name}' detectó entrada de '{other.name}' (tag: '{other.tag}')");

        if (!other.CompareTag("Player")) return;

        // Si ya hay un checkpoint activo más avanzado (o el mismo), no retrocedemos el spawn.
        if (activeCheckpoint != null && order < activeCheckpoint.order)
        {
            Debug.Log($"[Checkpoint] '{name}' (order {order}) IGNORADO: ya hay uno más avanzado activo ('{activeCheckpoint.name}', order {activeCheckpoint.order}).");
            return;
        }

        player.SetSpawnPoint(transform.position);
        CheckpointOn();
        Debug.Log($"[Checkpoint] '{name}' (order {order}) ACTIVADO. Nuevo respawn: {transform.position}");
    }

    public void CheckpointOn()
    {
        if (activeCheckpoint != null && activeCheckpoint != this)
            activeCheckpoint.CheckpointOff();

        activeCheckpoint = this;

        if (activeIndicator != null)
            activeIndicator.SetActive(true);
    }

    public void CheckpointOff()
    {
        if (activeIndicator != null)
            activeIndicator.SetActive(false);
    }
}