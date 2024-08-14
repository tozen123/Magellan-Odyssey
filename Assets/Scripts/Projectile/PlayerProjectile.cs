using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [Header("Travel Speed")]
    [SerializeField] private float speed = 15f;


    [Header("Life Time")]
    [SerializeField] private float lifeTime = 5f;

    [Header("References")]
    public GameObject smokePoofEffect;
  

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Death();

        }
    }

    public void Death()
    {
        
        Instantiate(smokePoofEffect, transform.position, transform.rotation);

        Destroy(this.gameObject);
    }
}
