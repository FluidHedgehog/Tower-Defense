using Unity.VisualScripting;
using UnityEngine;

public class EnemyInstance : MonoBehaviour, IMoveable
{
    [SerializeField] EnemyType type;

    [SerializeField] short health;
    [SerializeField] float speed;

    [SerializeField] int currentWaypointIndex;
    [SerializeField] Vector3 currentWaypoint;
    [SerializeField] Vector3 nextWaypoint;

    [SerializeField] Path path;

    void OnEnable()
    {
        health = type.health;
        speed = type.speed;


        path = FindFirstObjectByType<Path>();

        transform.position = path.waypoints[0].transform.position;
        currentWaypoint = path.waypoints[1].transform.position;
        nextWaypoint = path.waypoints[2].transform.position;
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

        if (Vector3.Distance(transform.position, currentWaypoint) < 0.1f)
        {
            if (currentWaypointIndex >= path.waypoints.Count - 1)
            {
                ((IMoveable)this).Dissappear();
                return;
            }
            currentWaypoint = nextWaypoint;
            (nextWaypoint, currentWaypointIndex) = path.GetNextWaypoint(currentWaypointIndex);
        }
        
    }

    void IMoveable.Dissappear()
    {
        Destroy(gameObject);
    }


}
