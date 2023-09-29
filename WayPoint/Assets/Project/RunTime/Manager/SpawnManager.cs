using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager :Singleton<SpawnManager>
{
    private Transform[] spawnPoint;

    [field: SerializeField] public SpawnSlots[] spawnSlot { get; set; }

    private float[] spawnTimer = new float[10];

    private void Awake()
    {
        for (int i = 0; i < spawnTimer.Length; i++)
        {
            spawnTimer[i] = 0;
        }

        spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {

        for (int i = 0; i < spawnSlot[GameManager.instance.spawnLevel].enemySO.Length; i++)
        {
            spawnTimer[i] += Time.deltaTime;

            if (spawnTimer[i] >= spawnSlot[GameManager.instance.spawnLevel].enemySO[i].spawnDelay)
            {
                spawnTimer[i] = 0;
                Spawn(i);
            }

        }
    }

    private void Spawn(int i)
    {
        GameObject enemy = spawnSlot[0].enemySO[0].prefab;

        var data = enemy.GetComponent<BehaviourTreeRunner>();
        data.so = spawnSlot[GameManager.instance.spawnLevel].enemySO[i];

        GameObject spawnObject = Pooling.instance.getObject
            (ref Pooling.instance.enemyPool,
            spawnPoint[Random.Range(1, spawnPoint.Length)],
            enemy);
    }
}

[System.Serializable]
public class SpawnSlots
{
    public EnemySO[] enemySO;
}

