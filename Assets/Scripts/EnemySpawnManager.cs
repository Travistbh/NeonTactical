using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawnManager : MonoBehaviour
{
    //public GameObject[] EnemyPool;

    public GameObject[] EnemyPrefabs;

    public Vector2 cornerL, cornerR, middle, pos;
    public float offset;

    private WaitForSeconds wait1 = new WaitForSeconds(1);

    private void Start()
    {
        Init();
    }

    private void Init()
    {

        pos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        cornerL = new Vector2(-pos.x, pos.y);
        cornerR = new Vector2(pos.x, pos.y);
        middle = new Vector2(0, pos.y + offset);

        StartCoroutine(Sequence1());
    }

    private void CreateEnemy(GameObject enemyPrefab, EnemyBehaviour.SpawnType spawnType = EnemyBehaviour.SpawnType.None)
    {
        GameObject enemy = Instantiate(enemyPrefab);
        
        S_Enemy sEnemy = enemy.GetComponent<S_Enemy>();
        //sEnemy.scoreBoard = GameObject.Find("Text_Score");

        EnemyBehaviour behaviour = enemy.GetComponent<EnemyBehaviour>();

        if (spawnType != EnemyBehaviour.SpawnType.None)
        {
            behaviour.spawnType = spawnType;
        }

        switch (behaviour.spawnType)
        {
            case EnemyBehaviour.SpawnType.Middle:
                enemy.transform.position = middle;
                break;
            case EnemyBehaviour.SpawnType.Random:
                enemy.transform.position = new Vector2(UnityEngine.Random.Range(cornerL.x + offset, cornerR.x - offset), pos.y + offset);
                break;
            default:
                throw new NotImplementedException("this SpawnType is not implemented yet");
        }

        enemy.transform.parent = transform;
        behaviour.canMove = true;
    }

    public IEnumerator Sequence1()
    {
        CreateEnemy(EnemyPrefabs[0], EnemyBehaviour.SpawnType.Random);

        yield return wait1;

        StartCoroutine(Sequence1());
    }
}
