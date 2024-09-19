using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatController : MonoBehaviour
{
    // Define states for the enemy
    public enum EnemyState
    {
        Idle,
        Chasing,
        Attacking
    }

    public EnemyState currentState = EnemyState.Idle; 

    public Transform player; 
    public float speed = 5f;
    public float chaseRange = 10f; 
    public float attackRange = 2f; 
    public float attackCooldown = 1f; 
    private float nextAttackTime = 0f; 

    private bool playerIsAlive = true;


    public Animator animator;

    public GameObject triggerMark;

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case EnemyState.Idle:
                if (distanceToPlayer <= chaseRange && playerIsAlive)
                {
                    currentState = EnemyState.Chasing;
                }

                triggerMark.SetActive(false);
                break;

            case EnemyState.Chasing:
                animator.SetBool("isChasing", true);
                triggerMark.SetActive(true);

                ChasePlayer();

                if (distanceToPlayer <= attackRange && playerIsAlive)
                {
                    currentState = EnemyState.Attacking;
                }
                break;

            case EnemyState.Attacking:
                animator.SetBool("isChasing", false);
                animator.SetTrigger("doAttack");
                AttackPlayer();

                if (distanceToPlayer > attackRange && distanceToPlayer <= chaseRange && playerIsAlive)
                {
                    currentState = EnemyState.Chasing;
                }
                break;
        }
    }

    private void ChasePlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();

        transform.position += direction * speed * Time.deltaTime;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
    }

    private void AttackPlayer()
    {
        if (Time.time >= nextAttackTime)
        {
            Debug.Log("Enemy is attacking the player!");

            nextAttackTime = Time.time + attackCooldown;
        }

        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
    }

    public void PlayerDied()
    {
        playerIsAlive = false;
        currentState = EnemyState.Idle; 
        Debug.Log("Player is dead, enemy stopped chasing.");
    }
}
