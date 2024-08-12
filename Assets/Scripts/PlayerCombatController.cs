using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [Header("References")]
    public Animator playerAnimator;
    public PlayerController playerController;

    [Header("Attack Attributes")]
    
    [Range(0.5f, 1.0f)]
    public float attackSpeed;

    private bool canAttack;
    [SerializeField] private BoxCollider damageBox;


    [Header("Effect Attributes")]
    public GameObject trailEffect;
    private void Awake()
    {
        trailEffect.SetActive(false);
        damageBox.enabled = false;

        canAttack = true;
    }

    public void SwingAttack()
    {
        if (!canAttack)
        {
            return;
        }

        int randomAttack = Random.Range(0, 2);
        playerController.canMove = false;
        StartCoroutine(AttackRest());

        if (randomAttack == 0)
        {
            playerAnimator.SetTrigger("Swing1");
        }
        else
        {
            playerAnimator.SetTrigger("Swing2");
        }
    }

    IEnumerator AttackRest()
    {
        playerController.canMove = false;
        canAttack = false;
        trailEffect.SetActive(true);
        damageBox.enabled = true;

        yield return new WaitForSeconds(attackSpeed);

        trailEffect.SetActive(false);

        damageBox.enabled = false;

        canAttack = true;
        playerController.canMove = true;
    }
}
