using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShirokoMain", menuName = "SO/Main/ShirokoMain")]
public class ShirokoMainAbility : AbilitySO
{
    public override void buff()
    {
        curDelay = 0;
    }

    public override void setText()
    {
        descText = string.Format($"'촬영용'드론을 소환합니다. ");
    }

    public override void update()
    {
        curDelay += Time.deltaTime;
        if (curDelay < delay) return;

        curDelay = 0;
        Debug.Log("skill on");
    }
}
