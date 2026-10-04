using UnityEngine;


[RequireComponent(typeof(Collider))]
public class Muerte : MonoBehaviour
{
    void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PLayer")) return;
 
        PLayer player = other.GetComponent<PLayer>();
        if (player != null)
        {
            player.Die();
        }
           
    }
}

