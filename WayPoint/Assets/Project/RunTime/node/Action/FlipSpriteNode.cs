using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GamePix.CustomVector;

public class FlipSpriteNode : ActionNode
{
    protected override void OnStart()
    {
     
    }

    protected override void OnStop()
    {
       
    }

    protected override State OnUpdate()
    {
        if(btContainer.targetPosition.x <= container.transform.position.x)
        {
            container.transform.localScale = FlipVector3.Left;
        }
        else
        {
            container.transform.localScale = FlipVector3.Right;
        }

        return State.Success;
    }
}
