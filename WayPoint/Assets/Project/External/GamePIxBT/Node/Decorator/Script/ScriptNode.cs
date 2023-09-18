using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptNode : DecoratorNode
{
    [TextArea(4, 2)] public string script;
    public bool alreadyRead = false;

    protected override void OnStart()
    {
        container.isScriptTriger = false;

        // text ����
        container.textScript = script;
        UIManager.instance.setText(container.textScript);
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        if (container.isScriptTriger)
        {
            alreadyRead = true;

            child.Update();
        }

        return State.Running;
    }
}
