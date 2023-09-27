using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShotDelayBuff", menuName = "SO/Skills/ShotDelay")]
public class ShotDelayBuff : AbilitySO
{
    public override void buff()
    {
        Player.instance.buff.shotDelay += buffAmount[level];
    }

    public override void setText()
    {
        descText = string.Format($"�߻�ӵ��� \n {buffAmount[level + 1]}% ���� \n ��ŵ�ϴ�.");
    }

    public override void update()
    {
       
    }
}
