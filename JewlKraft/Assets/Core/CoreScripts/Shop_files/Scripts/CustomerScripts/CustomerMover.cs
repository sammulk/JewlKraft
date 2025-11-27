using System.Collections;
using UnityEngine;

public class CustomerMover : MonoBehaviour
{
    public Transform WaypointParent;
    public float Speed = 2f;
    //public Animator animator;
    public float waitTime = 0f;
    public bool loopWaypoints = true;

    private Transform[] waypoints;
    private int currentWaypoint;
    private bool waiting;

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
        if (waiting) 
        {
            return;
        }
        MoveToWaypoint();
    }

    public void MoveToWaypoint()
    {
        Transform target = waypoints[currentWaypoint];

        transform.position = Vector2.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            StartCoroutine(WaitAtWaypoint());
        }
    }

    IEnumerator WaitAtWaypoint()
    {
        waiting = true;
        yield return new WaitForSeconds(waitTime);

        currentWaypoint = loopWaypoints ? (currentWaypoint + 1) % waypoints.Length : Mathf.Min(currentWaypoint +1, waypoints.Length-1);
           waiting = false;
    }
}
