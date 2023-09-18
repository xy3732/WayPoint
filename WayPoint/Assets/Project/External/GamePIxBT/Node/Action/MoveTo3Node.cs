using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// [ Unity 3D ] Blackboard�� movePosition�� ��ǥ���� �̵��ϴ� ActionNode.
/// </summary>
public class MoveTo3Node : ActionNode
{
    // [todo] container ���� ������ �� �ޱ�.
    public float speed;
    public bool isStopAble = false;

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (isStopAble && container.isStopMove) return State.Success;

        Vector3 dir;
        Quaternion lookTarget;

        dir = btContainer.moveToPosition - container.transform.position;
        lookTarget = Quaternion.LookRotation(dir);

        container.transform.position += dir.normalized * Time.deltaTime * speed;
        container.transform.rotation = Quaternion.Lerp(container.transform.rotation, lookTarget, 0.25f);

        if((container.transform.position - btContainer.moveToPosition).magnitude < 1f)
        {
            return State.Success;
        }

        return State.Running;
    }
}
