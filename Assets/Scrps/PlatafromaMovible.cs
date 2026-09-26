using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Plataforma que se mueve entre waypoints y "arrastra" consigo cualquier Rigidbody
/// que esté parado encima (jugador, cajas, etc.), sin usar parenting (evita jitter).
/// El collider debe ser sólido (Is Trigger = false).
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class MovingPlatform : MonoBehaviour
{
    [Tooltip("Puntos por los que se moverá la plataforma, en orden")]
    public Transform[] waypoints;
    public float speed = 3f;
    [Tooltip("true: va y vuelve entre los extremos. false: recorre los puntos en loop cerrado (1→2→3→1...)")]
    public bool pingPong = true;

    Rigidbody rb;
    int currentIndex = 0;
    int direction = 1;
    Vector3 lastPosition;

    readonly HashSet<Rigidbody> passengers = new HashSet<Rigidbody>();

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // se mueve por código, no por fuerzas físicas
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        lastPosition = rb.position;
    }

    void FixedUpdate()
    {
        MoveAlongWaypoints();
        CarryPassengers();
    }

    void MoveAlongWaypoints()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        Vector3 targetPos = waypoints[currentIndex].position;
        Vector3 newPos = Vector3.MoveTowards(rb.position, targetPos, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (Vector3.Distance(newPos, targetPos) < 0.05f)
        {
            AdvanceWaypoint();
        }
    }

    void AdvanceWaypoint()
    {
        if (pingPong)
        {
            if (currentIndex == waypoints.Length - 1) direction = -1;
            else if (currentIndex == 0) direction = 1;
            currentIndex += direction;
        }
        else
        {
            currentIndex = (currentIndex + 1) % waypoints.Length;
        }
    }

    void CarryPassengers()
    {
        Vector3 delta = rb.position - lastPosition;

        if (delta != Vector3.zero)
        {
            foreach (Rigidbody passenger in passengers)
            {
                if (passenger != null)
                {
                    passenger.MovePosition(passenger.position + delta);
                }
            }
        }

        lastPosition = rb.position;
    }

    // --- Detección de quién está parado encima ---
    void OnCollisionEnter(Collision collision) => TryAddPassenger(collision);
    void OnCollisionStay(Collision collision) => TryAddPassenger(collision);

    void OnCollisionExit(Collision collision)
    {
        if (collision.rigidbody != null) passengers.Remove(collision.rigidbody);
    }

    void TryAddPassenger(Collision collision)
    {
        if (collision.rigidbody == null) return;

        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f) // el contacto empuja hacia arriba: está parado encima
            {
                passengers.Add(collision.rigidbody);
                return;
            }
        }
    }
}