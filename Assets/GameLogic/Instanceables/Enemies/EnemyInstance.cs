using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyInstance : MonoBehaviour, IMoveable
{

    // References variables
    // ---------------------------------------------------------------------------
    // ---------------------------------------------------------------------------
    [Header("Assign Scriptable Object 'EnemyType' here.")]
    [Space(5)]
    [SerializeField] EnemyType type;
    [SerializeField] GameObject bloodPrefab;
    [SerializeField] Slider sliderUI;
    [HideInInspector] public Path path;
    [SerializeField] Animator animator;

    // Enemy variables
    // ---------------------------------------------------------------------------
    // ---------------------------------------------------------------------------
    [Header("Enemy variables - DO NOT TOUCH! FOR REFERENCE ONLY!")]
    [Space(5)]
    [HideInInspector] int health;
    [HideInInspector] float speed;
    [HideInInspector] public bool isAlive = true;
    [HideInInspector] public bool isSlowed;
    [HideInInspector] public bool isBloodBoosted;
    [HideInInspector] public bool isStunned;
    [HideInInspector] int boostedBlood;
    [HideInInspector] public bool isDamageBoosted;
    [HideInInspector] int boostedDamage;
    [HideInInspector] public int poisonCycles;
    [HideInInspector] public int enhanced;

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
    private GameObject currentWaypoint;
    private GameObject nextWaypoint;
    private GameObject finalWaypoint;

    // Initialization of enemy
    // ---------------------------------------------------------------------------
    // ---------------------------------------------------------------------------

    void OnEnable()
    {
        health = type.health;
        speed = type.speed;
        sliderUI.maxValue = type.health;
        sliderUI.value = health;

        //path = FindFirstObjectByType<Path>();


    }

    public void Initialize(Path path)
    {
        this.path = path;

        transform.position = (Vector2)path.waypoints[0].transform.position;

        currentWaypoint = path.waypoints[1];
        nextWaypoint = path.waypoints[2];
        finalWaypoint = path.GetFinalWaypoint();

        // transform.position = (Vector2)path.waypoints[0].transform.position;
        // currentWaypoint = (Vector2)path.waypoints[1].transform.position;
        // nextWaypoint = (Vector2)path.waypoints[2].transform.position;
        // finalWaypoint = (Vector2)path.GetFinalWaypoint();
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
        transform.position = Vector2.MoveTowards(transform.position, currentWaypoint.transform.position, Time.deltaTime * speed);

        if (Vector2.Distance(transform.position, finalWaypoint.transform.position) < 0.1f)
        {
            ((IMoveable)this).Dissappear();
            return;
        }

        if (Vector2.Distance(transform.position, currentWaypoint.transform.position) < 0.1f)
        {
            animator.SetTrigger(currentWaypoint.GetComponent<Waypoint>().SetAnimation());
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
        UpdateSlider();
        ValidateHealth();
    }

    public IEnumerator ApplyPoison(int cycles, int damage, float cooldown )
    {
        poisonCycles = cycles;

        while (poisonCycles > 0)
        {
            if (isDamageBoosted)
            {
                damage += boostedDamage;
            }
            
            health -= damage;
            UpdateSlider();
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

    public IEnumerator ApplyStun(float howLong)
    {
        isStunned = true;
        var currentSpeed = speed;

        speed = 0;

        yield return new WaitForSeconds(howLong);

        isStunned = false;
        speed = currentSpeed;
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

    public void Enhance(int enh)
    {
        enhanced += enh;

        speed += 0.10f;

    }

    void UpdateSlider()
    {
        sliderUI.value = health;
    }

    void IMoveable.Dissappear()
    {
        HealthSystemEvents.TriggerHealthRemoved(type.damage);
        Destroy(gameObject);
    }

    IEnumerator Die()
    {
        speed = 0;
        yield return new WaitForSeconds(1f);
        BloodSystemEvents.TriggerBloodAdded(type.blood);
        Destroy(gameObject);
    }
}