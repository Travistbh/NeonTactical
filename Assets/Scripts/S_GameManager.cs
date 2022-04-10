using System.Net.Mime;
using System.ComponentModel.Design;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/* This Game Manager is the Main Prefab for all scenes.
All assets should be able to reference the game manager to control any attribute.
-Contains all game elements and their interactions
-Contain all UI elements
-Spawn the player
-Provide persistent stats for player
All stats for the player are set in the GameManager Inspector
*/
public class S_GameManager : MonoBehaviour
{
    [Header("Player Health")]
    [Space(3)]
    public float playerHealth;
    public float playerMaxHealth;

    [Header("Ship Attributes")]
    public float shipDamage;
    public float shipSpeed;
    public float shipArmor;
    public float fireRate;
    public float bulletSpeed;
    public float upgradePoints;

    [Header("PowerUP Options")]
    [Space(3)]
    public float Power_Duration;
    public float Power_FireRate;
    public float Power_Armor;
    public float Power_Speed;
    public float Power_Damage;
    public float Power_Health;

    [Header("Game Stats")]
    [Space(3)]
    public int currentScore;
    public int highScore;
    public int kills;
    public int wave = 1;

    [Header("Game Options")]
    [Space(3)]
    public GameObject startLocation;
    public GameObject playerObject;
    S_Player player;
    
    
    [Header("UI References")]
    [Space(3)]
    //References to all UI elements for external access.
    public Slider healthSlider;
    public GameObject uiScore;
    public GameObject uiHighScore;
    public GameObject uiKills;
    public GameObject uiWave;
    public GameObject uiShipDamage;
    public GameObject uiShipSpeed;
    public GameObject uiShipArmor;

    //These attributes are for storing current ship info, 
    
    GameObject EnemyManager;
    EnemySpawnManager EM;
    public IEnumerator power;
    

    // Start is called before the first frame update
    void Start()
    {
        uiScore = GameObject.Find("Text_Score");
        uiHighScore = GameObject.Find("Text_HighScore");
        uiKills = GameObject.Find("Text_Kills");
        uiWave = GameObject.Find("Text_Wave");
        uiShipArmor = GameObject.Find("Text_Armor");
        uiShipDamage = GameObject.Find("Text_Damage");
        uiShipSpeed = GameObject.Find("Text_Speed");
        
        //Use the GameObjects location as the start position,  allows Spawn location to be set in scene.
        Vector3 start = startLocation.transform.position;

        playerHealth = playerMaxHealth;

        //SpawnPlayer and assign it to player variable to set the reference
        playerObject = Instantiate(playerObject, start, transform.rotation);
        player = playerObject.GetComponent<S_Player>();
        

        player.currentHealth = playerHealth;
        player.maxHealth = playerMaxHealth;

        
        player.damage = shipDamage;
        player.speed = shipSpeed;
        player.armor = shipArmor;
        player.fireRate = fireRate;
        player.bulletSpeed = bulletSpeed;

        UpdateArmor();
        UpdateDamage();
        UpdatePlayerHealth();
        UpdateSpeed();
        
        InvokeRepeating("cleanup", 10.0f, 10.0f);
    
    }

    // Update is called once per frame
    void Update()
    {

        
        //If we have a new HighScore make sure we set the current score to it
        if (currentScore >= highScore)
        {
            highScore = currentScore;
            uiHighScore.GetComponent<Text>().text = highScore.ToString();
        }
        player.damage = shipDamage;
        player.speed = shipSpeed;
        player.armor = shipArmor;
        player.fireRate = fireRate;
        player.bulletSpeed = bulletSpeed;
        
    }
    
    public void UpdatePlayerHealth()
    {
        healthSlider.value = player.currentHealth / player.maxHealth ;
    }
    public void UpdateScore(int score)
    {
        currentScore += score;
        uiScore.GetComponent<Text>().text=currentScore.ToString();
        //Debug.Log("Added Points");
    }
    public void UpdateKills()
    {
        kills = kills + 1;
        uiKills.GetComponent<Text>().text = kills.ToString();
        //Debug.Log("Added a Kill");
    }
    void UpdateDamage()
    {
        uiShipDamage.GetComponent<Text>().text = shipDamage.ToString();
    }
    void UpdateArmor()
    {
        uiShipArmor.GetComponent<Text>().text = shipArmor.ToString();
    }
    void UpdateSpeed()
    {
        uiShipSpeed.GetComponent<Text>().text = shipSpeed.ToString();
    }

    void cleanup()
    {
        GameObject[] effects = GameObject.FindGameObjectsWithTag("vfx");

        if (effects != null)
        {
            foreach (GameObject effect in effects)
            {
                Destroy(effect);
            }
        }
    }


    public void GivePower(string pickup)
    {
        
        power = PowerUP(pickup);
    }


    public IEnumerator PowerUP(string power)
    {
        if (power == "FireRate")
        {
            
            fireRate -= Power_FireRate;
            //Debug.Log("Improved FireRate");
            yield return new WaitForSeconds(Power_Duration);
            fireRate += Power_FireRate;
            Debug.Log("FireRate Normal");
            //StopCoroutine(power);
            
            
        }
        else if (power == "Armor")
        {
            
            shipArmor += Power_Armor;
            UpdateArmor();
            yield return new WaitForSeconds(Power_Duration);
            shipArmor -= Power_Armor;
            UpdateArmor();
            

        }
        else if (power == "Damage")
        {
            
            shipDamage += Power_Damage;
            UpdateDamage();
            yield return new WaitForSeconds(Power_Duration);
            shipDamage -= Power_Damage;
            UpdateDamage();
        }
        else if (power == "Health")
        {
            
            player.currentHealth = player.maxHealth;
            UpdatePlayerHealth();
            yield return new WaitForSeconds(Power_Duration);
            
            
        }
        else if (power == "Speed")
        {
            
            shipSpeed += Power_Speed;
            UpdateSpeed();
            yield return new WaitForSeconds(Power_Duration);
            shipSpeed -= Power_Speed;
            UpdateSpeed();
        }
    } 

    void gameOver()
    {

    }
}




// Need to Control the number of Enemy per Level and Increase as Waves Increase

//Need to control the UI information for Score keeping



        
