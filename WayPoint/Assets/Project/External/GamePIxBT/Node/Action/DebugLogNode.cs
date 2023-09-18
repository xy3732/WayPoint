using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  [ DEBUG ] �ش� ��尡 ����Ǵ��� Ȯ���ϴ¿�.
/// </summary>
public class DebugLogNode : ActionNode
{
    [TextArea(4,2)]public string message;

    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        Debug.Log($"{message}");

        return State.Success;
    }

}
