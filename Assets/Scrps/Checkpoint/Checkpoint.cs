using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }
    
    public void CheckpointOn()
    {
        Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();
        foreach (Checkpoint cp in checkpoints)
        {
            cp.CheckpointOff();
        }
    }

    public void CheckpointOff()
    {

    }
    private void OnTriggerEnter (Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            player.AddPlayerForConnection(transform.position);
            CheckpointOn();
        }    
    }
}