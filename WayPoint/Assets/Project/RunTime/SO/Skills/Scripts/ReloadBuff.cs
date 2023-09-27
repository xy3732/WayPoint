using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReloadBuff", menuName = "SO/Skills/Reload")]
public class ReloadBuff : AbilitySO
{
    public override void buff()
    {
        Player.instance.buff.reload = buffAmount[level];
        WeaponData.instance.SetReloadSpeed();
    }

    public override void setText()
    {
        descText = string.Format($"재장전 속도를 \n {buffAmount[level + 1]}% 증가 \n 시킵니다. ");
    }

    public override void update()
    {
       
    }
}
