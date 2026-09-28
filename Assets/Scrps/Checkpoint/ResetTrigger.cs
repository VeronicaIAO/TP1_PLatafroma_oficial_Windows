using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResetTrigger : MonoBehaviour
{
    public Transform player;
   // public PlayerController player;
    private Vector3 spwanPoint;

 /*   public Checkpoint[] checkpoints; 
    foreach (Checkpoint cp in checkpoints)
    {
        cp.isActivated= false;
    } */

    
    public UnityEvent respawn;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spwanPoint =  player.position;
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spwanPoint = newSpawnPoint;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.root.position = spwanPoint;
            respawn.Invoke();

           /* Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            if (rb = null)
            {
                player.verticalVelocity = Vector3.zero;
            } */
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
