using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayableCharacterData", menuName = "SO/character")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField] public RuntimeAnimatorController animator { get; set; }
    [field: SerializeField] public float speed { get; set; }
    [field: SerializeField] public float maxHp { get; set; }
    [field: SerializeField] public float maxSp { get; set; }

    [field: Space(20)]

    [field: SerializeField] public Sprite weapon { get; set; }

    [field: SerializeField] public Sprite leftHand { get; set; }

    [field: SerializeField] public Sprite rightHand { get; set; }

    [field: Space(20)]

    [field: SerializeField] public int abilitySelectAble { get; set; }

    [field: Space(20)]

    [field: SerializeField] public Sprite normalStateSprite { get; set; }
    [field: SerializeField] public Sprite hitStateSprite { get; set; }
}
