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

                foreach (var item in uiManager.CharacterSprites)
                {
                    item.gameObject?.SetActive(true);
                }

                uiManager.UIsObject.SetActive(false);
                uiManager.ScriptsObject.SetActive(true);
                uiManager.scriptButtonObject.SetActive(true);

                uiManager.BoardDownObject.DOKill();
                uiManager.BoardUpObject.DOKill();
                uiManager.BoardUpObject.DOAnchorPos(new Vector3(0,-220,0), 0.5f).SetEase(Ease.OutExpo);
                uiManager.BoardDownObject.DOAnchorPos(new Vector3(0, 20, 0), .5f).SetEase(Ease.OutExpo);
                break;

            case DoScriptType.end:
                uiManager.BoardDownObject.DOKill();
                uiManager.BoardUpObject.DOKill();
                uiManager.BoardUpObject.DOAnchorPos(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.OutExpo);
                uiManager.BoardDownObject.DOAnchorPos(new Vector3(0,-220,0),0.5f).SetEase(Ease.OutExpo).OnComplete( () => endSet(uiManager));
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

    private void endSet(UiManager uiManager)
    {
        // script(대사) 오브젝트들 비활성화
        uiManager.scriptsUiObject.SetActive(false);
        uiManager.ScriptsObject.SetActive(false);
        uiManager.scriptButtonObject.SetActive(false);
        // ui 오브젝트 활성화
        uiManager.UIsObject.SetActive(true);

    }
}