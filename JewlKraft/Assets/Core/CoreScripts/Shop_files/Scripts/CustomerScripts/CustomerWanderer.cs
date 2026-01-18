using System;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

public class CustomerWanderer : MonoBehaviour
{
    public float Speed = 2f;
    public Vector2 waitTimeRange = new Vector2(2f, 5f);

    public Vector2 minBounds;
    public Vector2 maxBounds;

    public LayerMask obstacleLayer;

    private Vector2 target;
    private bool waiting = false;
    private bool leaving = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (target == Vector2.zero) FindNewTarget();
    }

    void Update()
    {
        if (waiting)
        {
            animator.SetBool("isWalking", false);
            return;
        }

        MoveToTarget();
    }

    public void SetTarget(Vector2 targetPosition)
    {
        target = targetPosition;
        waiting = false;
    }
    
    private void MoveToTarget()
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;

        transform.position = Vector2.MoveTowards(
            transform.position,
            target,
            Speed * Time.deltaTime
        );

        
        animator.SetFloat("InputX", direction.x);
        animator.SetFloat("InputY", direction.y);
        animator.SetBool("isWalking", direction.magnitude > 0.1f);

        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            StartCoroutine(WaitAndFindNew());
        }
    }

    void FindNewTarget()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector2 randomPoint = new Vector2(
                Random.Range(minBounds.x, maxBounds.x),
                Random.Range(minBounds.y, maxBounds.y)
            );

            Vector2 direction = randomPoint - (Vector2)transform.position;
            float distance = direction.magnitude;

            RaycastHit2D hit = Physics2D.Raycast(
                transform.position,
                direction.normalized,
                distance,
                obstacleLayer
            );

            if (!hit)
            {
                target = randomPoint;
                return;
            }
        }
        
        target = transform.position;
    }

    IEnumerator WaitAndFindNew()
    {
        waiting = true;
        animator.SetBool("isWalking", false);

        float randomWait = Random.Range(waitTimeRange.x, waitTimeRange.y);
        yield return new WaitForSeconds(randomWait);

        waiting = false;
        FindNewTarget();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, target);
    }

    public void FinalVoyage(Transform spawnTransform)
    {
        StopAllCoroutines();
        SetTarget(spawnTransform.position);
        leaving = true;
    }

    private void OnBecameInvisible()
    {
        if (leaving) Destroy(gameObject);
    }
}
