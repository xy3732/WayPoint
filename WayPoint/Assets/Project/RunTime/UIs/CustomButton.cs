using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.UI;

using DG.Tweening;
using TMPro;
public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    private RectTransform rect;

    // Æ®À§´×
    [field: SerializeField] public Vector3 scaleUpEffect { get; set; }
    [field: Range(0, 1)] [field: SerializeField] public float effectTime { get; set; }
    // ÄÃ·¯
    [field: Space(20)]
    [field: SerializeField] public Color32 hoverColor { get; set; }
    [field: SerializeField] public Color32 clickColor { get; set; }
    [field: SerializeField] public Color32 normalColor { get; set; }

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rect.DOScale(scaleUpEffect, effectTime).SetEase(Ease.InOutBack).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rect.DOScale(new Vector3(1,1,1), effectTime).SetEase(Ease.InOutBack).SetUpdate(true);
    }
}
