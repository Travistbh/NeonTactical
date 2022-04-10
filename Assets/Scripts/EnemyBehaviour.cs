using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public enum SpawnType { None = default, Middle, Random}
    public enum MovementType { StraightConst }

    public SpawnType spawnType;
    public MovementType movementType;

    public float StraightConstSpeed;

    public bool canMove;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("WaitForDeath", 6, 4);
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            switch (movementType)
            {
                case MovementType.StraightConst:
                    rb.velocity = transform.up * StraightConstSpeed;
                    break;
                default:
                    throw new NotImplementedException("this MovementType is not implemented yet");
                    // TODO: finish that
            }
        }
    }

    private void WaitForDeath()
    {
        if (transform.position.y <= -20)
        {
            Destroy(gameObject);
        }
    }
}