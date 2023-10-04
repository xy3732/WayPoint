using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class DoScriptNode : ActionNode
{
    public enum DoScriptType
    {
        start,
        end
    }
    public DoScriptType types;

    protected override void OnStart()
    {
        var uiManager = UiManager.instance;

        switch (types)
        {
            case DoScriptType.start:
                uiManager.UIsObject.SetActive(false);
                uiManager.ScriptsObject.SetActive(true);
                uiManager.scriptButtonObject.SetActive(true);
                break;

            case DoScriptType.end:
                uiManager.UIsObject.SetActive(true);
                uiManager.ScriptsObject.SetActive(false);
                uiManager.scriptButtonObject.SetActive(false);
                break;
        }
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        return State.Success;
    }
}