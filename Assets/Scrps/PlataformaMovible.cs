using UnityEngine;

public class PlataformaMovible : MonoBehaviour
{
    public float speed = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, speed, 0);
        new WaitForSeconds(1);
        transform.position = new Vector3(0, -speed, 0);
    }
}
