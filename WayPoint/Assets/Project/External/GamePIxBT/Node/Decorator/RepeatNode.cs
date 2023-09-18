using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  �ݺ������� ����ǰ� ������ִ� DecoratorNode
/// </summary>
public class RepeatNode : DecoratorNode
{
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        child.Update();
        
        // �ݺ� �����̶� Suceess ��ȯ���� �ϸ� �ȵȴ�.
        // ���������� Running �����ٰ�.
        return State.Running;
    }
}
