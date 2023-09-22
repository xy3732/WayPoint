using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private Transform[] spawnPoint;
    [ SerializeField] private SpawnSlots[] spawnSlot;


    private float timer;

    private void Awake()
    {
        timer = 0;

        spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= 0.5f)
        {
            timer = 0f;
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject enemy = Pooling.instance.getObject
            (ref Pooling.instance.enemyPool,
            spawnPoint[Random.Range(1,spawnPoint.Length)], 
            spawnSlot[0].spawnData);
    }
}

[System.Serializable]
public class SpawnSlots
{
    public GameObject spawnData;
}

