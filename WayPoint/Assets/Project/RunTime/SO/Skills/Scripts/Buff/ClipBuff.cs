using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClipBuff", menuName = "SO/Buff/Clip")]
public class ClipBuff : AbilitySO
{
    public override void buff()
    {
        Player.instance.buff.clip += buffAmount[level];
        WeaponData.instance.setClip();
    }

    public override void setText()
    {
        descText = string.Format($"≈∫√¢¿ª \n {buffAmount[level + 1]} ¡ı∞° \n Ω√≈µ¥œ¥Ÿ.");
    }

    public override void update()
    {
       
    }
}
