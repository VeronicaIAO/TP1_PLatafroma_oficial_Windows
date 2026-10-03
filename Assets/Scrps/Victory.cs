using UnityEngine;

public class Victory : MonoBehaviour
{

    public GameObject victoryScreen; // panel con el texto "¡Ganaste!" ya armado en el Canvas
    public bool pauseGame = true;
    public bool unlockCursor = true;
 
    bool triggered;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider other)
    {
        if (triggered || !other.CompareTag("Player")) return;
        triggered = true;
 
        if (victoryScreen != null)
            victoryScreen.SetActive(true);
 
        if (pauseGame)
            Time.timeScale = 0f;
 
        if (unlockCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

}
