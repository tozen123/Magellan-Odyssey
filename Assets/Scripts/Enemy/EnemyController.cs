using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Attributes")]

    public float currentHealth;
    public float maxHealth;

    public float knockbackForce = 5f;
    public float knockbackDuration = 0.1f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

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
                StartCoroutine(ApplyKnockback(knockbackDirection));
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
}
