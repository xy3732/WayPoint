using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilitySO : ScriptableObject
{
    public enum SkillType
    {
        Main,
        buff
    }

    // Sprite
    [field: SerializeField] public SkillType type { get; set; }
    [field: SerializeField] public Sprite abilitySprite { get; set; }
    [field: SerializeField] public Color32 color { get; set; }

    // Buff
    [field: Space(20)]
    [field: SerializeField] public float buffAmount { get; set; }

    //
    protected abstract void buff();
    protected abstract void update();
}
