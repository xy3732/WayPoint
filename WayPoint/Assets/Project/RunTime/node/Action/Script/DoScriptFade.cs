using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DG.Tweening;

public class DoScriptFade : ActionNode
{
    [field: SerializeField] private int arrayNumber { get; set; }
    private Image image { get; set; }
    public enum DoFadeType
    {
        FadeIn,
        FadeOut
    }
    public DoFadeType types;


    protected override void OnStart()
    {
        image = UiManager.instance.CharacterSprites[arrayNumber].gameObject.GetComponent<Image>();

        if (image == null)
        {
            Debug.LogWarning($"{this.name} - CharacterImageEffect is null");
        }

        switch (types)
        {
            case DoFadeType.FadeIn:
                image.DOKill();
                image.DOColor(new Color32(40, 40, 40, 255), 0f);
                image.DOColor(new Color32(255, 255, 255, 255), 0.3f);
                break;

            case DoFadeType.FadeOut:
                image.DOKill();
                image.DOColor(new Color32(255, 255, 255, 255), 0f);
                image.DOColor(new Color32(40, 40, 40, 255), 0.3f).OnComplete(() => endSet());
                break;
        }
    }

    protected override void OnStop()
    {
     
    }

    protected override State OnUpdate()
    {
        if (image != null) return State.Success;
        else return State.Failure;
    }

    private void endSet()
    {
        // 캐릭터, 헤일러 오브젝트 off
        image.gameObject.SetActive(false);
        image.gameObject.GetComponent<CharacterImageEffect>().haloObject?.SetActive(false);

        UiManager.instance.scriptsUiObject.SetActive(false);
    }
}
