using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "SO/Enemy")]
public class EnemySO : ScriptableObject
{
    [field: SerializeField] public GameObject prefab { get; set; }

    [field: Space(20)]
    [field: SerializeField] public float spawnDelay { get; set; }

    [field: Space(20)]
    // �������ͽ�
    [field: SerializeField] public float hp { get; set; }
    [field: SerializeField] public float damage { get; set; }
    [field: SerializeField] public int exp { get; set; }
}