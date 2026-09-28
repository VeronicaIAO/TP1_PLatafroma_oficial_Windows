using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool isActivated = false;
    [SerializeField] ResetTrigger resetTrigger;
    
    /*
    public PlayerController player;

    void Start()
    {
        player = FindObjectByType<PlayerController>();
    }
    
    public void CheckpointOn()
    {
        Checkpoint[] checkpoints = FindObjectsByType<Checkpoint>();
        foreach (Checkpoint cp in checkpoints)
        {
            cp.CheckpointOff();
        }
    } 

    public void CheckpointOff()
    {

    }*/

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isActivated)
            {
                resetTrigger.SetSpawnPoint(transform.position);
                isActivated = true;
            }
            
            //CheckpointOn();
        }    
    }
}