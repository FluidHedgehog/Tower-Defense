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
    [SerializeField] GameObject bloodPrefab;
    [HideInInspector] private Path path;

    // Enemy variables
    // ---------------------------------------------------------------------------
    // ---------------------------------------------------------------------------
    [Header("Enemy variables - DO NOT TOUCH! FOR REFERENCE ONLY!")]
    [Space(5)]
    [SerializeField] int health;
    [SerializeField] float speed;
    [SerializeField] public bool isAlive = true;
    [SerializeField] public bool isSlowed;
    [SerializeField] public bool isBloodBoosted;
    [SerializeField] int boostedBlood;
    [SerializeField] public bool isDamageBoosted;
    [SerializeField] int boostedDamage;
    [SerializeField] public int poisonCycles;

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

    public void ApplyDamage(int damage)
    {
        if (isDamageBoosted)
        {
            damage += boostedDamage;
        }
        health -= damage;
        ValidateHealth();
    }

    public IEnumerator ApplyPoison(int cycles, int damage, float cooldown )
    {
        poisonCycles = cycles;

        while (poisonCycles > 0)
        {
            health -= damage;
            yield return new WaitForSeconds(cooldown);
        }
    }

    public IEnumerator ApplyBoostDamage(int damage, float howLong)
    {
        isDamageBoosted = true;
        boostedDamage = damage * 3 / 10;

        yield return new WaitForSeconds(howLong);

        isDamageBoosted = false;
        boostedDamage = 0;
    }
    
    public IEnumerator ApplyBoostBlood(int blood, float howLong)
    {
        isBloodBoosted = true;
        boostedBlood = blood;
        yield return new WaitForSeconds(howLong);

        isBloodBoosted = false;
        boostedBlood = 0;
    }

    public IEnumerator ApplySlow(int slowValue, float howLong)
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

    void CreateBlood(int blood)
    {
        var blood1 = Instantiate(bloodPrefab, lastPosition, Quaternion.identity);
        blood1.GetComponent<Blood>().Initialize(blood + boostedBlood);
    }

    private void ValidateHealth()
    {
        if (health <= 0)
        {
            isAlive = false;
            //CreateBlood(type.blood);
            StartCoroutine(Die());
        }
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