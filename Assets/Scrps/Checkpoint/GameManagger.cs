using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;


public class GameManagger : MonoBehaviour
{

    public static GameManagger Instance; 

    public bool IsGameOver = false; 

    public int currentCoin = 0;

    public Vector3 currentSpawnPoint;
    public UnityEvent magment;



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


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCheckpoint(Vector3 newSpawnPosition)
    {
        currentSpawnPoint = newSpawnPosition;
    }

    public void Victory()
    {
        if (IsGameOver) return;
        IsGameOver = true;
    }
}
