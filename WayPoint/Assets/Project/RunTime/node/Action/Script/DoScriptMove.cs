using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class DoScriptMove : ActionNode
{
    [field: SerializeField] private int arrayNumber { get; set; }
    private RectTransform image { get; set; }
    private Vector3 oriPos { get; set; }
    public enum type
    {
        ShakeUpDown,
    }
    public type types;

    private float nodeTimer = 0;

    protected override void OnStart()
    {
        image = UIManager.instance.CharacterSprites[arrayNumber].gameObject?.GetComponent<RectTransform>();
        if(image == null)
        {
            Debug.LogWarning($"{this.name} - RectTransform is null");
        }

        oriPos = image.anchoredPosition;
    }

    protected override void OnStop()
    {
        image.DOAnchorPos(oriPos,0.2f).OnComplete(() => image.DOKill());
    }

    protected override State OnUpdate()
    {
        nodeTimer += Time.deltaTime;

        switch (types)
        {
            case type.ShakeUpDown:
                float debug = Mathf.Abs(10f * Mathf.Sin(GameManager.instance.timer * 8.95f));
                image.anchoredPosition = new Vector3( oriPos.x,oriPos.y + debug, oriPos.z);
                break;
        }

        if (nodeTimer > 0.7f) return State.Success;
        return State.Running;
    }
}
