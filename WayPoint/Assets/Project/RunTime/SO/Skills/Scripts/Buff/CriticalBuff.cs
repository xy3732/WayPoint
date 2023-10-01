using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CriticalBuff", menuName = "SO/Buff/Critical")]
public class CriticalBuff : AbilitySO
{
    public override void buff()
    {
        Player.instance.buff.critical += buffAmount[level];
    }

    public override void setText()
    {
        descText = string.Format($"ġ��Ÿ Ȯ���� \n {buffAmount[level + 1]} ���� \n ��ŵ�ϴ�.");
    }

    public override void update()
    {
       
    }
}
