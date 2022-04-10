using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_BGScroll : MonoBehaviour
{
    public float scrollSpeed;
    public float tileSizeY;

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newPostion = Mathf.Repeat (Time.time * scrollSpeed, tileSizeY);
        transform.position = startPosition + Vector3.down * newPostion;
    }
}
