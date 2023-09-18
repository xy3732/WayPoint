using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ����߿� State�� Failure�� ��带 �������� �ʰ� ���� ��带 �����ϴ� CompositeNode
/// </summary>
public class TrueSelectorNode : CompositeNode
{
    int current;
    protected override void OnStart()
    {
        current = 0;
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        // ���� current��°�� �ִ� �ڽ� ��带 ��������.
        // ��������� ������� ������ �ȵ� ��带 �����ͼ� �����̵ȴ�.
        var child = children[current];

        switch (child.Update())
        {
            case State.Running:
                return State.Running;

            case State.Success:
                return State.Success;

                // ���� ������ ��� �̸� �н�.
            case State.Failure:
                current++;
                break;
        }

        // ���� �������� ����� ��°�� �ڽ��� ������ ��°�� ������� ���� ��ȯ,
        // �ƴ϶�� ���������� ��ȯ.
        return current == children.Count ? State.Success : State.Running;
    }
}
