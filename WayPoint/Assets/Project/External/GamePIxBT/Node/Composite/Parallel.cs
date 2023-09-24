using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 병행 실행
public class Parallel : CompositeNode
{
    List<State> childrenLeftToExcute = new List<State>();

    protected override void OnStart()
    {
        childrenLeftToExcute.Clear();

        children.ForEach((n) => 
        {
            childrenLeftToExcute.Add(State.Running);
        });
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        bool stillRunning = false;

        for(int i = 0; i < childrenLeftToExcute.Count(); i++)
        {
            if(childrenLeftToExcute[i] == State.Running)
            {
                var status = children[i].Update();

                if(status == State.Failure)
                {
                    AbortRunningChildren();
                    return State.Failure;
                }

                if(status == State.Running)
                {
                    stillRunning = true;
                }

                childrenLeftToExcute[i] = status;
            }
        }

        return stillRunning ? State.Running : State.Success;
    }

    void AbortRunningChildren()
    {
        for(int i=0; i<childrenLeftToExcute.Count(); i++)
        {
            if(childrenLeftToExcute[i]==State.Running)
            {
                children[i].Abort();
            }
        }
    }
}
