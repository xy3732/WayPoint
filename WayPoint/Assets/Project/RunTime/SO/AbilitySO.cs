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

    // id
    [field: SerializeField] public int id { get; set; }
    [field: SerializeField] public string character { get; set; }

    // sprite , color
    [field :Space(20)]
    [field: SerializeField] public SkillType type { get; set; }
    [field: SerializeField] public Sprite abilitySprite { get; set; }
    [field: SerializeField] public Color32 color { get; set; }

    [field: Space(20)]

    // text
    [field: SerializeField] public string mainText { get; set; }
    public string descText { get; set; }

    // Buff
    [field: Space(20)]
    [field: SerializeField] public int level { get; set; }
    [field: SerializeField] public float[] buffAmount { get; set; }


    public abstract void buff();
    public abstract void update();
    public abstract void setText();
}
