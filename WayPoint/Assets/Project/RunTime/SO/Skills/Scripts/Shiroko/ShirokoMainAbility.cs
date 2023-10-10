using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShirokoMain", menuName = "SO/Main/ShirokoMain")]
public class ShirokoMainAbility : AbilitySO
{

    public override void buff()
    {

    }

    public override void setText()
    {
        descText = string.Format($"'�Կ���'����� ��ȯ�մϴ�. ");

        curDelay = delay * 0.5f;
    }

    public override void update()
    {
        curDelay += Time.deltaTime;

        if (curDelay < delay) return;

        curDelay = 0;

        Pooling.instance.getObject(ref Player.instance.abilityPool, Player.instance.transform, abilityObject);
    }
}
