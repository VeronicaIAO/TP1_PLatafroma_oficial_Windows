using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Feedback visual (opcional)")]
    public GameObject activeIndicator;
 
    public PLayer player;
 
    static Checkpoint activeCheckpoint;
 
    void Start()
    {
        player = FindFirstObjectByType<PLayer>();
 
        if (activeIndicator != null)
        {
            activeIndicator.SetActive(false);
        }
            
    }
 
    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        } 
 
        player.SetSpawnPoint(transform.position);
        CheckpointOn();
    }
 
    public void CheckpointOn()
    {
        if (activeCheckpoint != null && activeCheckpoint != this)
        {
            activeCheckpoint.CheckpointOff();
        }
            
 
        activeCheckpoint = this;
 
        if (activeIndicator != null)
        {
            activeIndicator.SetActive(true);
        }
            
    }
 
    public void CheckpointOff()
    {
        if (activeIndicator != null)
        {
            activeIndicator.SetActive(false);
        }
            
    }

}
 