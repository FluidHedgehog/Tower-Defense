using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class EnemyInstance : MonoBehaviour, IMoveable
{

    // References variables
    // ---------------------------------------------------------------------------
    // ---------------------------------------------------------------------------
    [Header("Assign Scriptable Object 'EnemyType' here.")]
    [Space(5)]
    [SerializeField] EnemyType type;
    [HideInInspector] private Path path;

    // Enemy variables
    // ---------------------------------------------------------------------------
    // ---------------------------------------------------------------------------
    [Header("Enemy variables - DO NOT TOUCH! FOR REFERENCE ONLY!")]
    [Space(5)]
    [SerializeField] short health;
    [SerializeField] float speed;
    [SerializeField] public bool isAlive = true;
    [SerializeField] public bool isSlowed;

    // Distance variables
    // ---------------------------------------------------------------------------
    // ---------------------------------------------------------------------------
    private float distanceTreshold = 0.1f;
    private float distanceMoved;
    [HideInInspector] public float totalDistance;
    private Vector3 lastPosition;

    // PathHelper variables
    // ---------------------------------------------------------------------------
    // ---------------------------------------------------------------------------
    private int nextWaypointIndex = 2;
    private Vector3 currentWaypoint;
    private Vector3 nextWaypoint;
    private Vector3 finalWaypoint;

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
        distanceMoved = Vector3.Distance(transform.position, lastPosition);

        if (distanceMoved >= distanceTreshold)
        {
            totalDistance += distanceMoved;
            lastPosition = transform.position;
        }

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

    public void ApplyDamage(short damage)
    {
        health -= damage;
        ValidateHealth();
    }

    private void ValidateHealth()
    {
        if (health <= 0)
        {
            isAlive = false;
            StartCoroutine(Die());
        }
    }

    public IEnumerator ApplySlow(short slowValue, float howLong)
    {
        isSlowed = true;
        speed -= slowValue * 0.1f;
        if (speed <= 0)
        {
            speed = 0.1f;
        }
        yield return new WaitForSeconds(howLong);

        isSlowed = false;
        speed += slowValue * 0.1f;
    }

    void IMoveable.Dissappear()
    {
        Destroy(gameObject);
    }

    IEnumerator Die()
    {
        speed = 0;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}