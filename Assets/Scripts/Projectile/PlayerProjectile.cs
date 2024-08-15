using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private float minDamage;
    [SerializeField] private float maxDamage;
    
    [Header("Travel Speed")]
    [SerializeField] private float speed = 15f;


    [Header("Life Time")]
    [SerializeField] private float lifeTime = 5f;

    [Header("References")]
    public GameObject smokePoofEffect;

    private void Awake()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Death();

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().currentHealth -= Random.Range(minDamage, maxDamage);
        }
        
    }
    public void Death()
    {
        
        Instantiate(smokePoofEffect, transform.position, transform.rotation);

        Destroy(this.gameObject);
    }
}
