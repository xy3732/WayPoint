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
        descText = string.Format($"������� \n {buffAmount[level + 1]} ���� \n ��ŵ�ϴ�.");
    }

    public override void update()
    {
       
    }
}
