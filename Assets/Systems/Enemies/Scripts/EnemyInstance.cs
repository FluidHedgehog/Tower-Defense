using Unity.VisualScripting;
using UnityEngine;

public class EnemyInstance : MonoBehaviour, IMoveable
{
    [SerializeField] EnemyType type;

    [SerializeField] short health;
    [SerializeField] float speed;

    [SerializeField] int nextWaypointIndex = 2;
    [SerializeField] Vector3 currentWaypoint;
    [SerializeField] Vector3 nextWaypoint;
    [SerializeField] Vector3 finalWaypoint;

    [SerializeField] Path path;

    void OnEnable()
    {
        health = type.health;
        speed = type.speed;

        path = FindFirstObjectByType<Path>();

        transform.position = path.waypoints[0].transform.position;
        currentWaypoint = path.waypoints[1].transform.position;
        nextWaypoint = path.waypoints[2].transform.position;
        finalWaypoint = path.GetFinalWaypoint();
        ((IMoveable)this).GetNextWaypoint();
    }

    void Update()
    {
        ((IMoveable)this).GoToNextWaypoint();
    }

    void IMoveable.GetNextWaypoint()
    {

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

    void IMoveable.Dissappear()
    {
        Destroy(gameObject);
    }
    
}