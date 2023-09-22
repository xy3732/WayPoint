using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVector3Node : ActionNode
{
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        container.transform.position = new Vector3(btContainer.targetPosition.x, btContainer.targetPosition.y, btContainer.targetPosition.z);

        return State.Success;
    }
}
