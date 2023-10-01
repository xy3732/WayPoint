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
        descText = string.Format($"źâ�� \n {buffAmount[level + 1]} ���� \n ��ŵ�ϴ�.");
    }

    public override void update()
    {
       
    }
}
