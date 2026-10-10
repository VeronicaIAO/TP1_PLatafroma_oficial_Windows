using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Victory : MonoBehaviour
{
    public GameManagger gamemanagger;

    void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManagger.Instance != null)
        {
            GameManagger.Instance.Victory();
            gamemanagger.IsGameOver = true;
            if (gamemanagger.IsGameOver == true) 
                Debug.Log("Llegaste");
        }
    }
}

