using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GamePix.bulletType;

[CreateAssetMenu(fileName = "BulletData", menuName = "SO/bullet")]
public class BulletSO : ScriptableObject
{

    [field: SerializeField] public BulletType type { get; set; }

    [field: SerializeField] public float lifeTime { get; set; }

    [field: SerializeField] public float damage { get; set; }

    // �̻��� ��
    [field: SerializeField] public RuntimeAnimatorController MissileAnimator { get; set; }

    // �Ѿ� ��
    [field: SerializeField] public Sprite BulletSprite { get; set; }
}
