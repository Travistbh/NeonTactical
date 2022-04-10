using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PowerUP : MonoBehaviour
{
    public bool FireRate, Damage, Health, Speed, Armor;
    public string PowerUp;
    SpriteRenderer sr;


    void Start()
    {
        
        sr = gameObject.GetComponentInChildren<SpriteRenderer>();
        if (FireRate == true)
        {
            PowerUp = "FireRate";
            sr.color = Color.yellow;
        }
        else if (Damage == true)
        {
            PowerUp = "Damage";
            sr.color = Color.red;
        }
        else if (Health == true)
        {
            PowerUp = "Health";
            sr.color = Color.green;
        }
        else if (Speed == true)
        {
            PowerUp = "Speed";
            sr.color = Color.grey;
        }
        else if (Armor == true)
        {
            PowerUp = "Armor";
            sr.color = Color.blue;
        }
    }
}
