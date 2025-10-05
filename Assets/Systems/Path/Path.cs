using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    public Vector3 GetNextWaypoint(int waypoint)
    {
        if (waypoints.ContainsKey(waypoint + 1))
            return waypoints[waypoint + 1].transform.position;
        else
            return GetFinalWaypoint();

    }

    public Vector3 GetFinalWaypoint()
    {
        return waypoints.Values.Last().transform.position;
    }

}