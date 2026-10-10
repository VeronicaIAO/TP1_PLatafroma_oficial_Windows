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
        if (other.CompareTag("PLayer") && GameManagger.Instance != null)
        {
            GameManagger.Instance.Victory();
            Debug.Log("Llegaste");
        }
    }
}

