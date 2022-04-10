using System.Runtime.InteropServices;
using System.Reflection;
using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class S_Player : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public float damage;
    public float armor;

    // Ship Controls

    public GameObject projectile;
    AudioSource audioSource;
    public AudioClip fireEffect;
    public float bulletSpeed;

    // Reduce FireRate to increase Rate of fire ( time between shots /sec)
    public float fireRate;
    public float bulletDamage;
    public float speed;

    private float nextShot = 0.5f;
    private GameObject newBullet;
    private float fireTime = 0.0f;
    GameObject GM;
    public S_GameManager Manager;
    S_PowerUP power;
    // Start is called before the first frame update
    void Start()

    {
        GM = GameObject.Find("GameManager");
        Manager = GM.GetComponent<S_GameManager>();
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    { 
        if (currentHealth <= 0)
        {
            Debug.Log("GameOver");
            //game over
        }

        if (Input.GetKey(KeyCode.D)){
            transform.position += Vector3.right * speed * Time.deltaTime;
        } 
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.right * -speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W)){
            transform.position += Vector3.up * speed * Time.deltaTime;
        } 
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.up * -speed * Time.deltaTime;
        }

        //Setup hold fire button to repeatedly shoot;
        fireTime = fireTime + Time.deltaTime;
        CheckProjectile();
        if (Input.GetButton("Fire1") && fireTime > nextShot) 
        {
            nextShot = fireTime + fireRate;
            newBullet = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
            audioSource.Play(0);

            nextShot = nextShot - fireTime;
            fireTime = 0.0f;
        }
    }

    public void takeDamage(float damageTaken)
    {
        currentHealth = currentHealth - damageTaken;
        Manager.UpdatePlayerHealth();


    }
    void CheckProjectile()
    {
        projectile.GetComponent<S_Projectile>().setParameters(damage, bulletSpeed); 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Power")
        {
            power = other.gameObject.GetComponent<S_PowerUP>();
            StartCoroutine(Manager.PowerUP(power.PowerUp));
            Debug.Log("Hit a PowerUp");
            Destroy(other.gameObject);
        }
        //Debug.Log("hit object" + other.name);
    }


}
