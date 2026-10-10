using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public int order = 0;

    public GameObject activeIndicator;


    public static Checkpoint activeCheckpoint;

    void Start()
    {
        if (activeIndicator != null)
            activeIndicator.SetActive(false);
    }

    void OnTriggerStay(Collider other)
    {

        if (!other.CompareTag("PLayer")) return;

        PLayer player = other.GetComponentInParent<PLayer>();
        if (player == null) return;

        player.SetSpawnPoint(transform.position);
        CheckpointOn();
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