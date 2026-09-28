using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    //public Transform lastCheckpoint;
    public Vector3 currentSpawnPoint;
    public UnityEvent magment;

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
}
