using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager :Singleton<SpawnManager>
{
    private Transform[] spawnPoint;

    [field: SerializeField] public SpawnSlots[] spawnSlot { get; set; }

    private float spawnTimer;

    private void Awake()
    {
        spawnTimer = 0;

        spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnSlot[GameManager.instance.spawnLevel].enemySO.spawnDelay)
        {
            spawnTimer = 0f;
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject enemy = spawnSlot[0].enemySO.prefab;

        var data = enemy.GetComponent<BehaviourTreeRunner>();
        data.so = spawnSlot[0].enemySO;

        GameObject spawnObject = Pooling.instance.getObject
            (ref Pooling.instance.enemyPool,
            spawnPoint[Random.Range(1, spawnPoint.Length)],
            enemy);
    }
}

[System.Serializable]
public class SpawnSlots
{
    public EnemySO enemySO;
}

