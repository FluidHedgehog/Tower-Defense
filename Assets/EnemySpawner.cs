using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Path path;
    [SerializeField] GameObject prefab1;

    void OnEnable()
    {

    }

    void OnDisable()
    {
        
    }


    public void CreateEnemy()
    {
        Instantiate(prefab1, path.waypoints[0].transform.position, Quaternion.identity);
    }

}
