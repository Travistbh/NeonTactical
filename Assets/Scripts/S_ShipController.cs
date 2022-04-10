using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script for controlling the movement of the Ship and fireing of projectiles */
public class S_ShipController : MonoBehaviour
{
    public GameObject projectile;
    public float bulletSpeed = 25f;

    // Reduce FireRate to increase Rate of fire ( time between shots /sec)
    public float fireRate = 0.5f;
    public float bulletDamage = 1;
    public float speed;

    private float nextShot = 0.5f;
    private GameObject newBullet;
    private float fireTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
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

            nextShot = nextShot - fireTime;
            fireTime = 0.0f;
        }
    }
    void CheckProjectile()
    {
        projectile.GetComponent<S_Projectile>().setParameters(GetComponentInParent<S_Player>().damage, bulletSpeed); 
    }

}
