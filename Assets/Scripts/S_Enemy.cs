using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using Random=UnityEngine.Random;
using UnityEngine;

using Debug = UnityEngine.Debug;
public class S_Enemy : MonoBehaviour
{
    public float health;
    public float armor;
    public float fireRate;
    public GameObject projectile;
    public GameObject deathEffect;
    public Transform gunC;
    public Transform gunL;
    public Transform gunR;
    [SerializeField]
    GameObject PowerUp_prefab;
    S_PowerUP PowerType;
    public int scoreValue = 10;
    public int pointValue;
    public float moveSpeed;
    public int enemyLevel;
    public float collisionDamage = 25f;
    
    public GameObject GM;
    
    S_GameManager GMScript;
    

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager");
        GMScript = GM.GetComponent<S_GameManager>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            
            //GMScript.currentScore = scoreValue + GMScript.currentScore;
            GMScript.UpdateKills();
            GMScript.UpdateScore(scoreValue);
            Instantiate(deathEffect, transform.position, transform.rotation);
            RandomDrop();
            Destroy(gameObject);
        }
    }
/*     void OnCollisionEnter2D(Collision2D hitObject)
    {
        if (hitObject.gameObject.tag == "Projectile" || hitObject.gameObject.tag == "Player")
        {
            float damageTaken = hitObject.gameObject.GetComponent<S_Projectile>().damage;
            health = health - damageTaken;

            Debug.Log("Hit"); 
            Debug.Log(health); 
            Debug.Log(damageTaken);
            
            
        } */

    void OnTriggerEnter2D(Collider2D hitObject)
    {
        if (hitObject.gameObject.tag == "Projectile")
        {
            float damageTaken = hitObject.gameObject.GetComponent<S_Projectile>().damage;
            hitObject.gameObject.GetComponent<S_Projectile>().onHit();
            health = health - damageTaken;
            
            Debug.Log("Hit by a " + hitObject.gameObject.tag); 
            if (health > 0)
            {
                hitObject.gameObject.GetComponent<S_Projectile>().impact();
            }
        } 
        
        if  (hitObject.gameObject.tag == "Character")
        {
            hitObject.gameObject.GetComponent<S_Player>().takeDamage(collisionDamage);
            health = 0;

            Debug.Log("Hit by a " + hitObject.gameObject.tag);
        }
    }

    //Percent chance that an enemy will drop a PowerUp
    void RandomDrop()
    {
        int num = Random.Range(0,100);

        if (num >= 1 && num <=5)
        {
            
            GameObject PowerUP = Instantiate(PowerUp_prefab, transform.position, transform.rotation);
            PowerUP.GetComponent<S_PowerUP>().FireRate = true;
        }
        else if (num >= 20 && num <= 25)
        {
            GameObject PowerUP = Instantiate(PowerUp_prefab, transform.position, transform.rotation);
            PowerUP.GetComponent<S_PowerUP>().Armor = true;
        }
        else if (num >= 40 && num <= 45)
        {
            GameObject PowerUP = Instantiate(PowerUp_prefab, transform.position, transform.rotation);
            PowerUP.GetComponent<S_PowerUP>().Health = true;
        }
        else if (num >= 70 && num <= 75)
        {
            GameObject PowerUP = Instantiate(PowerUp_prefab, transform.position, transform.rotation);
            PowerUP.GetComponent<S_PowerUP>().Damage = true;
        }
        else if (num >= 95 && num <= 100)
        {
            GameObject PowerUP = Instantiate(PowerUp_prefab, transform.position, transform.rotation);
            PowerUP.GetComponent<S_PowerUP>().Speed = true;
        }
    }

}

