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
        descText = string.Format($"발사속도를 \n {buffAmount[level + 1]}% 증가 \n 시킵니다.");
    }

    public override void update()
    {
       
    }
}
