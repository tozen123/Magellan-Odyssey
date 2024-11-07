using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierGuardChaser : MonoBehaviour
{
    public Transform target;
    public bool startChasing;
    public float chasingSpeed;
    public float rotationSpeed = 5f;
    private Animator animator;
    private PlayerHealthManager playerHealthManager;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerHealthManager = target.GetComponent<PlayerHealthManager>();
    }

    void Update()
    {
        if (startChasing && target != null)
        {
            animator.SetBool("isRunning", true);

            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0; 
            transform.position += direction * chasingSpeed * Time.deltaTime;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target.position) <= 2f)
            {
                playerHealthManager.currentHealth = -1;
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }
}
