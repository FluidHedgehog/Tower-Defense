using Unity.VisualScripting;
using UnityEngine;

public class EnemyInstance : MonoBehaviour, IMoveable
{

    // References variables
    // ---------------------------------------------------------------------------
    // ---------------------------------------------------------------------------
    [SerializeField] EnemyType type;
    [SerializeField] Path path;

    // Enemy variables
    // ---------------------------------------------------------------------------
    // ---------------------------------------------------------------------------
    [SerializeField] short health;
    [SerializeField] float speed;

    // PathHelper variables
    // ---------------------------------------------------------------------------
    // ---------------------------------------------------------------------------
    [SerializeField] int nextWaypointIndex = 2;
    [SerializeField] Vector3 currentWaypoint;
    [SerializeField] Vector3 nextWaypoint;
    [SerializeField] Vector3 finalWaypoint;

    // Initialization of enemy
    // ---------------------------------------------------------------------------
    // ---------------------------------------------------------------------------

    void OnEnable()
    {
        health = type.health;
        speed = type.speed;

        path = FindFirstObjectByType<Path>();

        transform.position = path.waypoints[0].transform.position;
        currentWaypoint = path.waypoints[1].transform.position;
        nextWaypoint = path.waypoints[2].transform.position;
        finalWaypoint = path.GetFinalWaypoint();
    }

    void Update()
    {
        ((IMoveable)this).GoToNextWaypoint();
    }

    void IMoveable.GoToNextWaypoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, Time.deltaTime * speed);

        if (Vector3.Distance(transform.position, finalWaypoint) < 0.1f)
        {
            ((IMoveable)this).Dissappear();
            return;
        }

        if (Vector3.Distance(transform.position, currentWaypoint) < 0.1f)
        {
            currentWaypoint = nextWaypoint;
            nextWaypoint = path.GetNextWaypoint(nextWaypointIndex);
            nextWaypointIndex++;
        }
    }

    void ApplyDamage()
    {
        
    }

    void IMoveable.Dissappear()
    {
        Destroy(gameObject);
    }
    
}