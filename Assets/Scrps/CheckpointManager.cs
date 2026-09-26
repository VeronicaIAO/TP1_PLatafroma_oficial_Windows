using UnityEngine;

/// <summary>
/// Guarda la posición del último checkpoint tocado por el jugador.
/// Es un singleton simple: cualquier script puede leer CheckpointManager.Instance.CurrentRespawnPosition
/// </summary>
public class CheckpointManager : MonoBehaviour
{
    public Transform player;
    public static CheckpointManager Instance { get; private set; }
    [Tooltip("Posición inicial por si el jugador muere antes de tocar un checkpoint")]
    public Vector3 CurrentRespawnPosition;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SetRespawnPosition(Vector3 position)
    {
        CurrentRespawnPosition = position;
    }

    
}