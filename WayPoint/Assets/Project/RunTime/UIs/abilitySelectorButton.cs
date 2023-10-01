using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.UI;

using DG.Tweening;
using TMPro;
public class abilitySelectorButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [field:SerializeField] private AbilitySO ability { get; set; }

    [field : Space(20)]
    [field: SerializeField] private Sprite normalImage { get; set; }
    [field: SerializeField] private Sprite selectedImage { get; set; }

    [field: Space(20)]
    [field: SerializeField] private Color32 hoverColor { get; set; }
    [field: SerializeField] private Color32 clickCOlor { get; set; }
    [field: SerializeField] private Color32 normalColor { get; set; }

    private Image image;

    private GameObject skillSpriteObject;
    private GameObject MainSkillSpriteObject;
    private GameObject abilitySpriteObject;

    private TextMeshProUGUI mainText;
    private TextMeshProUGUI descriptionText;
    private TextMeshProUGUI characterNameText;
    private TextMeshProUGUI abilityTypeText;

    private RectTransform rect;

    private void Awake()
    {
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();

        skillSpriteObject = transform.GetChild(0).gameObject;
        MainSkillSpriteObject = skillSpriteObject.transform.GetChild(0).gameObject;
        abilitySpriteObject = skillSpriteObject.transform.GetChild(1).gameObject;

        mainText = transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        descriptionText = transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        characterNameText = transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();
        abilityTypeText = transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        image.color = normalColor;
        image.sprite = normalImage; 
    }

    public void setAbility(AbilitySO ability)
    {
        bool isSameAbilityHave = false;

        // 플레이어가 어빌리티를 가지고 있지 않으면 패스
        if (Player.instance.buffAbilitys.Count != 0)
        {
            var abilitys = Player.instance.buffAbilitys;

            // 이미 가지고 있는 어벌리티 있으면 그 데이터를 버튼에 추가
          for(int i= 0; i< abilitys.Count; i++)
            {
                if (ability.id == abilitys[i].id)
                {
                    this.ability = abilitys[i];
                    isSameAbilityHave = true;

                    break;
                }
            }
        }

        // 가지고 있지 않으면 새로 생성
        if(!isSameAbilityHave) this.ability = Instantiate(ability);

        // 이미지 설정
        setImage();
    }

    private void setImage()
    {
        if (ability == null) return;

        switch (ability.type)
        {
            case AbilitySO.SkillType.Main:
                MainSkillSpriteObject.SetActive(true);
                break;

            case AbilitySO.SkillType.buff:
                MainSkillSpriteObject.SetActive(false);
                break;
        }

        skillSpriteObject.GetComponent<Image>().color = ability.color;
        abilitySpriteObject.GetComponent<Image>().sprite = ability.abilitySprite;

        ability.setText();
        descriptionText.text = ability.descText;
        mainText.text = string.Format($"{ability.mainText} LV.{ability.level+1}");
        characterNameText.text = ability.character;

        switch (ability.type)
        {
            case AbilitySO.SkillType.Main:
                abilityTypeText.text = "EX 스킬";
                break;

            case AbilitySO.SkillType.buff:
                abilityTypeText.text = "강화 스킬";
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        image.color = clickCOlor;
        image.sprite = selectedImage;

        abilitySelector.instance.deleteButton();
        Player.instance.addAbility(ability);
        ability.level += 1;

        // 해당 어빌리티가 buff 형태라면 buff player data 업데이트
        if (ability.type == AbilitySO.SkillType.buff) ability.buff();

        // 해당 어빌리티가 레벨에 도달하면 선택 풀에서 제외
        if(ability.level >= ability.buffAmount.Length -1)
        {
            GameManager.instance.abilityDelete(ability.id);
        }
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
