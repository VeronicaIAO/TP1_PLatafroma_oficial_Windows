using UnityEngine;

public class GameManagger : MonoBehaviour
{

    public static GameManagger Instance { get; private set; }

    public bool IsGameOver { get; private set; }

    public int currentCoin;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCoin(int coinToAdd)
    {
        currentCoin += coinToAdd;
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Victory()
    {
        if (IsGameOver) return;
        IsGameOver = true;
        Debug.Log("Victoria: llegaste a la meta.");
    }
}
