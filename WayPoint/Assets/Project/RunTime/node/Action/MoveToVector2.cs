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

    protected override State OnUpdate()
    {
        Vector3 pos = btContainer.targetPosition - container.transform.position;
        container.transform.position += pos.normalized * Time.smoothDeltaTime * speed;

        if((container.transform.position - btContainer.targetPosition).magnitude <1f)
        {
            return State.Success;
        }

        return State.Running;
    }
}
