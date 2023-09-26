using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using DG.Tweening;
public class abilitySelector : Singleton<abilitySelector>
{
    [field: SerializeField] public GameObject blackAlphaObject;
    [field: SerializeField] public GameObject abilitySelectorButton;

    private bool isActive = false;

    [HideInInspector] private List<GameObject> buttonList = new List<GameObject>();
    [HideInInspector] public Queue<GameObject> pool = new Queue<GameObject>();

    public void createButton()
    {
        if (isActive) return;

        Time.timeScale = 0;
        isActive = true;

        blackAlphaObject.GetComponent<Image>().DOColor(new Color32(0, 0, 0, 150), 0.5f).SetEase(Ease.OutQuint).SetUpdate(true);

        for (int i = 0; i < Player.instance.playerData.abilitySelectAble; i++)
        {
            var buttonObject = Pooling.instance.getObject(ref pool, gameObject, abilitySelectorButton);
            buttonObject.GetComponent<RectTransform>().DOScale(new Vector3(0, 0, 0), 0).SetUpdate(true);
            buttonList.Add(buttonObject);
        }

        doAnimation();
    }

    private void doAnimation()
    {
        for (int i = 0; i < buttonList.Count; i++)
        {
            var button = buttonList[i].gameObject;

            button.GetComponent<RectTransform>().DOScale(new Vector3(1,1,1), 0.075f).SetDelay(i * 0.05f).SetEase(Ease.OutBack).SetUpdate(true);
        }
    }

    public void deleteButton()
    {
        if (!isActive) return;

        Time.timeScale = 1;
        isActive = false;

        blackAlphaObject.GetComponent<Image>().DOColor(new Color32(0, 0, 0, 10), 0.5f).SetEase(Ease.OutQuad).SetUpdate(true);

        foreach (var item in buttonList)
        {
            Pooling.instance.setObject(ref pool, item);
        }

        buttonList.Clear();
    }
}
