using System.Collections;
using UnityEngine;

public class CustomerMover : MonoBehaviour
{
    //This script is for a customer who moves using waypoints

    public Transform WaypointParent;
    public float Speed = 2f;
    //public Animator animator;
    public float waitTime = 0f;
    public bool loopWaypoints = true;

    private Transform[] waypoints;
    private int currentWaypoint;
    private bool waiting;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
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
            animator.SetBool("isWalking", false);
            return;
        }
        MoveToWaypoint();
    }

    public void MoveToWaypoint()
    {
        Transform target = waypoints[currentWaypoint];
        Vector2 direction = (target.position - transform.position).normalized;

        transform.position = Vector2.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);
        animator.SetFloat("InputX", direction.x);
        animator.SetFloat("InputY", direction.y);
        animator.SetBool("isWalking", direction.magnitude > 0.1f);


        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            StartCoroutine(WaitAtWaypoint());
        }
    }

    IEnumerator WaitAtWaypoint()
    {
        waiting = true;
        animator.SetBool("isWalking", false);
        yield return new WaitForSeconds(waitTime);

        currentWaypoint = loopWaypoints ? (currentWaypoint + 1) % waypoints.Length : Mathf.Min(currentWaypoint +1, waypoints.Length-1);
           waiting = false;
    }
}
