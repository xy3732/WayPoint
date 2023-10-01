using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageBuff", menuName = "SO/Skills/Damage")]
public class DamageBuff : AbilitySO
{
    public override void buff()
    {
        Player.instance.buff.damage += buffAmount[level];
    }

    public override void setText()
    {
        descText = string.Format($"대미지를 \n {buffAmount[level + 1]} 증가 \n 시킵니다.");
    }

    public override void update()
    {
       
    }
}
