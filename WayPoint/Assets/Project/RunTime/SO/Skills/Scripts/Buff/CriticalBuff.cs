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
        descText = string.Format($"치명타 확률을 \n {buffAmount[level + 1]} 증가 \n 시킵니다.");
    }

    public override void update()
    {
       
    }
}
