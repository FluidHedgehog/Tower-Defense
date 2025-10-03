using System.Collections.Generic;
using UnityEngine;

// public static class PathActions
// {
//     public static event System.Func<int, (Vector3, int)> OnGetWaypoint;

//     public static (Vector3, int) TriggerGetWaypoint(int waypoint)
//     {
//         return OnGetWaypoint?.Invoke(waypoint) ?? (default(Vector3), -1);
//     }

// }


public class Path : MonoBehaviour
{

    [Header("Path Waypoints")]

    public Dictionary<int, GameObject> waypoints;
    [SerializeField] public List<GameObject> avaibleWaypoints;

    [Header("Customize Waypoint Gizmos")]

    [Range(0, 5)]
    [SerializeField] float gizmoSize;

    [ColorUsage(true)]
    [SerializeField] Color waypointColor;

    [Header("Customize Path Gizmos")]

    [ColorUsage(true)]
    [SerializeField] Color pathColor;

    void Awake()
    {
        waypoints = new Dictionary<int, GameObject>();
        for (byte i = 0; i < avaibleWaypoints.Count; i++)
        {
            waypoints[i] = avaibleWaypoints[i];
        }
    }

    void OnEnable(){
    

     }

    void OnDisable(){
    

     }

    void OnDrawGizmos()
    {
        foreach (var waypoint in avaibleWaypoints)
        {
            Gizmos.color = waypointColor;
            Gizmos.DrawWireSphere(waypoint.transform.position, gizmoSize);
        }

        for (byte i = 0; i < avaibleWaypoints.Count - 1; i++)
        {
            Gizmos.color = pathColor;
            Gizmos.DrawLine(avaibleWaypoints[i].transform.position, avaibleWaypoints[i + 1].transform.position);
        }
    }

    public (Vector3, int) GetNextWaypoint(int waypoint)
    {
        Vector3 next = waypoints[waypoint + 2].transform.position;
        return (next, waypoint + 1);
    }

}
