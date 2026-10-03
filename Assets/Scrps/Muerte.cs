using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class Muerte : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;
 
        PLayer player = other.GetComponent<PLayer>();
        if (player != null)
        {
            player.Die();
        }
           
    }
}

