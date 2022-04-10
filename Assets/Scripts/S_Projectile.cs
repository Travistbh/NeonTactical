using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    public float projectileSpeed;
    public float damage;
    public GameObject impactEffect;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * projectileSpeed * Time.deltaTime;
        
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    public void onHit()
    {
        //play splody animation
        
        Destroy(gameObject); 
        //Debug.Log("Enemy hit");
            
    }
    public void impact()
    {
        Instantiate(impactEffect, transform.position,transform.rotation);
    }
    public void setParameters(float inDamage, float speed)
    {
        damage = inDamage;
        projectileSpeed = speed;
    }

}
