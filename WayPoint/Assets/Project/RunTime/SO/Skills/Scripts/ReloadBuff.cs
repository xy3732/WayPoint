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
        descText = string.Format($"������ �ӵ��� \n {buffAmount[level + 1]}% ���� \n ��ŵ�ϴ�. ");
    }

    public override void update()
    {
       
    }
}
