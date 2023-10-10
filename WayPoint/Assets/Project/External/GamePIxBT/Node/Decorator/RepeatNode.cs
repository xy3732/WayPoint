using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  반복적으로 실행되게 만들어주는 DecoratorNode
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
        
        // 반복 실행이라 Suceess 반환으로 하면 안된다.
        // 지속적으로 Running 돌려줄것.
        return State.Running;
    }
}
