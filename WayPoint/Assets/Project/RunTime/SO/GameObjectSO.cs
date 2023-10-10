using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "SO/gameObject")]
public class GameObjectSO : ScriptableObject
{
    [field: SerializeField] public GameObject prefab { get; set; }

    [field: Space(20)]
    [field: SerializeField] public float spawnDelay { get; set; }

    [field: Space(20)]
    // 스테이터스
    [field: SerializeField] public float hp { get; set; }
    [field: SerializeField] public float damage { get; set; }
    [field: SerializeField] public int exp { get; set; }
}
