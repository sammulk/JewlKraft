using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    public Transform WaypointParent;
    public float Speed;
    
    private Transform[] waypoints;
    private int currentWaypoint;

    
    void Start()
    {
        waypoints = new Transform[WaypointParent.childCount];

        for (int i = 0; i < WaypointParent.childCount; i++)
        {
            waypoints[i] = WaypointParent.GetChild(i);
        }
        
    }

    
    void Update()
    {
        
    }
}
