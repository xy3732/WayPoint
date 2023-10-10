using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class DamageFont : MonoBehaviour
{
    private RectTransform rect;
    private TextMeshProUGUI text;

    private float duration = 0.5f;
    [HideInInspector] public float damage { get; set; }

    private void OnEnable()
    {
        if(rect == null) rect = GetComponent<RectTransform>();
        if (text == null) text = GetComponent<TextMeshProUGUI>();

        rect.DOScale(new Vector3(2f, 2f, 2f), 0);
    }

    public void DoAnimation()
    {
        text.text = string.Format($"{damage}");

        rect.DOScale(new Vector3(1, 1, 1), duration).SetEase(Ease.OutExpo);
        rect.transform.DOMove(rect.transform.position + new Vector3(0, 30, 0), duration).SetEase(Ease.OutQuint).OnComplete(() => fade());
    }

    private void fade()
    {
        text.DOColor(new Color32(255, 255, 255, 0), duration * 2)
            .SetEase(Ease.OutQuart)
            .OnComplete(() => DamageFontContainer.instance.endEffect(gameObject));
    }
}
