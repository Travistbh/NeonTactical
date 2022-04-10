using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class S_Boundries : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector2 screenBounds;
    private float objWidth;
    private float objHeight;
    void Start()
    {
        
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Math.Clamp(viewPos.x, screenBounds.x + objWidth, screenBounds.x * -1 - objWidth);
        viewPos.y = Math.Clamp(viewPos.y, screenBounds.y + objHeight, screenBounds.y * -1 - objHeight);
        transform.position = viewPos;

    }
}
