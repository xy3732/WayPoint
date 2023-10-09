using System.Collections;
using System.Collections.Generic;


#if UNITY_EDITOR
using UnityEngine.Profiling;
#endif

public class SequenceNode : CompositeNode
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
        UnityEngine.Profiling.Profiler.BeginSample("SequenceNode");

        var child = children[current];

        switch (child.Update())
        {
            case State.Running:
                return State.Running;
            case State.Failure:
                return State.Failure;

            case State.Success:
                current++;
                break;
        }

        UnityEngine.Profiling.Profiler.EndSample();

        return current == children.Count ? State.Success : State.Running;
    }
}
