using UnityEngine;

public class Colectionable : MonoBehaviour
{
    public GameObject pickupEffect; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.tag.Equals("Player"))
        {
            if (pickupEffect != null)
            {
                Instantiate(pickupEffect, transform.position, transform.rotation); 

            }
            Destroy(gameObject); 
        }
    }

}
