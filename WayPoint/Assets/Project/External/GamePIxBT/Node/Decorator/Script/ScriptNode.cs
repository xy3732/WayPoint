using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptNode : DecoratorNode
{
    [field: SerializeField]public string characterName { get; set; }
    [field: SerializeField]public string schoolClubName { get; set; }

    [field: SerializeField] [field: TextArea(4, 2)] public string script { get; set; }
    public bool alreadyRead = false;

    protected override void OnStart()
    {
        container.isScriptTriger = false;

        // text ¼³Á¤
        container.textScript = script;
        UiManager.instance.setScriptText(characterName,schoolClubName,script);

        if (!UiManager.instance.scriptsUiObject.activeSelf) UiManager.instance.scriptsUiObject.SetActive(true);

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

        if(child.state == State.Success) return State.Success;
        else return State.Running;
    }
}
