using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������� ���𰡰� ������ ����Ǵ� ActionNode
/// </summary>
public class InRangeNode : ConditionNode
{
    protected override void OnStart()
    {
            
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        if(container.isInrange == false) return State.Failure;
        else return State.Success;
    }
}
