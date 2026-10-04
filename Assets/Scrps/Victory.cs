using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Victory : MonoBehaviour
{

    void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManagger.Instance != null)
        {
            GameManagger.Instance.Victory();
        }
    }
}

