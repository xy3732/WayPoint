using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerPosNode : ActionNode
{
    protected override void OnStart()
    {
       
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        btContainer.targetPosition = Player.instance.transform.position;

        return State.Success;
    }
}
