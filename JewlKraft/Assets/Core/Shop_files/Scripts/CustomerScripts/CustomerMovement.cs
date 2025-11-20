using UnityEditor.Tilemaps;
using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    public Transform WaypointParent;
    public float Speed = 2f;
    public Animator animator;
    
    private Transform[] waypoints;
    private int currentWaypoint;
    private bool moving = false;
    private bool waiting = false;

    private Vector3 target;

    
    void Start()
    {
        waypoints = new Transform[WaypointParent.childCount];

        for (int i = 0; i < WaypointParent.childCount; i++)
        {
            waypoints[i] = WaypointParent.GetChild(i);
        }

        currentWaypoint = 1;
        MoveToNext();
        
    }

    
    void Update()
    {
        if (moving)
            MoveTowardsTarget();
    }

    public void MoveToNext()
    {
        //at checkout
        if (currentWaypoint == 2 && !waiting)
        {
            moving = false;
            SetIdle();
            waiting = true;
            return;
        }

        //back in spawn
        if (waiting && currentWaypoint == 0)
        {
            Destroy(gameObject);
            return;
        }

        //moving to next
        target = waypoints[currentWaypoint].position;
        moving = true;
        SetAnimationDirection(target - transform.position);
    }

    public void MoveTowardsTarget()
    {
        Vector3 diff = target - transform.position;
        if (diff.magnitude < 0.05f)
        {
            transform.position = target;
            moving = false;
            //SetIdle();

            currentWaypoint++;

            MoveToNext();
            return;
        }

        transform.position += diff.normalized * Speed * Time.deltaTime;

    }
    private void SetIdle()
    {
        animator.SetInteger("MoveX", 0);
        animator.SetInteger("MoveY", 0);
    }

    public void SetAnimationDirection(Vector3 dir)
    {
        dir.Normalize();
        int x = 0;
        int y = 0;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            x = dir.x > 0 ? 1 : -1;
        else
            y = dir.y > 0 ? 1 : -1;

        animator.SetInteger("MoveX", x);
        animator.SetInteger("MoveY", y);
    }

    public void AcceptOrder()
    {
        if (!waiting) return;

        waiting = false;
        currentWaypoint = 1; //target 
        MoveToNext();
    }
}
