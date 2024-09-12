using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Attributes")]

    [SerializeField] public float currentHealth;
    [SerializeField] public float maxHealth;

    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private float knockbackDuration = 0.1f;

    [SerializeField] private bool knockBackAllowed = true;

    private Rigidbody rb;

    public bool isDummyFromCh1L2R = false;
    private ChapterOneLevelTwoHandler_RevisedVersion handler;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (isDummyFromCh1L2R)
        {
            handler = GameObject.FindObjectOfType<ChapterOneLevelTwoHandler_RevisedVersion>();

        }

    }

    void Update()
    {
        if(currentHealth <= 0)
        {
            if (isDummyFromCh1L2R)
            {
                DestroyDummy();
            }
            Destroy(this.gameObject);
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("PlayerProjectile"))
        {
            collision.gameObject.GetComponent<PlayerProjectile>().Death();


            CameraShaker.Instance.Shake();

            // Calculate knockback direction
            Vector3 knockbackDirection = transform.position - collision.contacts[0].point;
            knockbackDirection.y = 0; // Optional: Keep the knockback on the horizontal plane
            knockbackDirection.Normalize();

            // Apply knockback force
            if (rb != null)
            {
                if (knockBackAllowed)
                {
                    StartCoroutine(ApplyKnockback(knockbackDirection));

                }
            }

        }
    }

    private IEnumerator ApplyKnockback(Vector3 direction)
    {
        // Apply knockback
        rb.AddForce(direction * knockbackForce, ForceMode.Impulse);

        // Wait for the knockback duration
        yield return new WaitForSeconds(knockbackDuration);

        // Stop the enemy's movement
        rb.velocity = Vector3.zero;
    }

    public void DestroyDummy()
    {
        handler.OnDummyDestroyed(gameObject);
        Destroy(gameObject);
    }
}
