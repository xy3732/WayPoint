using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.UI;

using DG.Tweening;
public class abilitySelectorButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [field: SerializeField] private Sprite normalImage { get; set; }
    [field: SerializeField] private Sprite selectedImage { get; set; }

    [field: Space()]
    [field: SerializeField] private Color32 hoverColor { get; set; }
    [field: SerializeField] private Color32 clickCOlor { get; set; }
    [field: SerializeField] private Color32 normalColor { get; set; }

    private Image image;
    private GameObject mainSkillSprite;
    private RectTransform rect;

    private void Awake()
    {
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        mainSkillSprite = transform.GetChild(0).gameObject;
    }

    private void OnEnable()
    {
        image.color = normalColor;
        image.sprite = normalImage; 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        image.color = clickCOlor;
        image.sprite = selectedImage;

        abilitySelector.instance.deleteButton();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = normalColor;
        image.sprite = selectedImage;

        rect.DOScale(new Vector3(1.05f, 1.05f, 1.05f),0.15f).SetEase(Ease.OutBack).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = normalColor;
        image.sprite = normalImage;

        rect.DOScale(new Vector3(1,1,1), 0.15f).SetEase(Ease.OutBack).SetUpdate(true);
    }
}
