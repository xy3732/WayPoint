using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToVector2 : ActionNode
{
    public float speed;

    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    float delay = 0;
    protected override State OnUpdate()
    {
        delay += Time.deltaTime;

        if (delay > 0.02f)
        {
            delay = 0;

            if (container.stopVelocity)
            {
                container.rigid.velocity = Vector2.zero;
                container.stopVelocity = false;

                return State.Success;
            }

            Vector3 pos = btContainer.targetPosition - container.transform.position;
            container.transform.position += pos.normalized * Time.smoothDeltaTime * speed;

            if ((container.transform.position - btContainer.targetPosition).magnitude < 1f)
            {
                return State.Success;
            }
        }

        return State.Running;
    }
}
