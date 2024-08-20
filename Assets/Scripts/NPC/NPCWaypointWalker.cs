using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWaypointWalker : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float waypointThreshold = 0.2f;
    [SerializeField] private float minIdleTime = 1.0f;
    [SerializeField] private float maxIdleTime = 5.0f;

    private int _currentWaypointIndex = 0;
    private Animator _animator;
    private bool _isIdle = true; // Start in idle state

    private void Start()
    {
        _animator = GetComponent<Animator>();
        StartCoroutine(IdleAtWaypoint()); // Start with idle state
    }

    void Update()
    {
        if (waypoints.Length == 0 || _isIdle) return;

        MoveTowardsWaypoint();
    }

    private void MoveTowardsWaypoint()
    {
        Vector3 targetPosition = waypoints[_currentWaypointIndex].position;

        float distanceToWaypoint = Vector3.Distance(transform.position, targetPosition);

        if (distanceToWaypoint < waypointThreshold)
        {
            _animator.SetBool("isWalking", false);
            StartCoroutine(IdleAtWaypoint());
            return;
        }

        _animator.SetBool("isWalking", true);

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        Vector3 direction = (targetPosition - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * moveSpeed);
        }
    }

    private IEnumerator IdleAtWaypoint()
    {
        _isIdle = true;

    

        float idleDuration = Random.Range(minIdleTime, maxIdleTime);
        _animator.SetBool("isIdle", true);
        yield return new WaitForSeconds(idleDuration);

        _animator.SetBool("isIdle", false);
        _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
        _isIdle = false; 
    }

    private void OnDrawGizmos()
    {
        if (waypoints == null) return;

        Gizmos.color = Color.red;
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[i] != null)
            {
                Gizmos.DrawSphere(waypoints[i].position, 0.3f);
            }
        }
    }
}
