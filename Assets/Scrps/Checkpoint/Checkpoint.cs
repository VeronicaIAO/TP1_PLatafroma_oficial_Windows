using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool isActivated = false;
    [SerializeField] ResetTrigger resetTrigger;
    public PlayerController player;
    [SerializeField] GameManager GameManager; 


    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isActivated && player.isFalling==true)
            {
                resetTrigger.SetSpawnPoint(transform.position);
                isActivated = true;
                GameManager.Instance.SetCheckpoint(transform.position);
                Debug.Log("Checkpoint guardado en: " + transform.position);
            }
            
        }    
    }
}