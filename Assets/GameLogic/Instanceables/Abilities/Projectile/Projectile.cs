using UnityEngine;

public class Projectile : MonoBehaviour
{
    EnemyInstance target;
    int damage;
    [SerializeField] float speed;

    public void Initialize(EnemyInstance instance, int dmg)
    {
        target = instance;
        damage = dmg;
    }

    void Update()
    {
        if (target == null || !target.isAlive)
        {
            var newEnemy = transform.parent.gameObject.GetComponent<TargetAbilityInstance>().currentTarget;
            if (newEnemy != null)
            {
                target = newEnemy;
            }
            else
            {
                DestroyProjectile();
                return;
            }
        }

        if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            ApplyDamage();
            DestroyProjectile();
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        
    }

    void ApplyDamage()
    {
        if (target == null || !target.isAlive) return;
        target.ApplyDamage(damage);
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
