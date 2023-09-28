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

    // 무기 스프라이트
    [field: SerializeField] public Sprite weapon { get; set; }
    // 왼손은 스프라이트
    [field: SerializeField] public Sprite leftHand { get; set; }
    // 오른손 스프라이트
    [field: SerializeField] public Sprite rightHand { get; set; }

    [field: Space(20)]

    // 어빌리티 선택 폭
    [field: SerializeField] public int abilitySelectAble { get; set; }
    // 최대로 가질수 있는 어빌리티 갯수
    [field: SerializeField] public int maxAbilityCount { get; set; }

    // UI
    [field: Space(20)]
    [field: SerializeField] public Sprite normalStateSprite { get; set; }
    [field: SerializeField] public Sprite hitStateSprite { get; set; }
}
