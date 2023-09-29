using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;
using TMPro;
public class UImanager : Singleton<UImanager>
{
    [field: SerializeField] private TextMeshProUGUI weaponClipText { get; set; }

    [field: SerializeField] public Image clipBar { get; set; }
    
    [field: Space(20)]
    [field: SerializeField] private GameObject characterBoard { get; set; }
    private Vector3 characterBoardNormalVector;
    [field: SerializeField] private Image character { get; set; }
    private Vector3 characterNormalVector;
    [field: SerializeField] public Image hpBar { get; set; }
    [field: SerializeField] public Image hpLateBar { get; set; }
    [field: SerializeField] public Image spBar { get; set; }
    
    [field: Space(20)]
    [field: SerializeField] private TextMeshProUGUI levelText { get; set; }
    [field: SerializeField] public Image expBar { get; set; }

    private Player player;
    private void Start()
    {
        WeaponUpdateUI(Player.instance.weaponData.curClip, Player.instance.weaponData.maxClip);
        barUI(expBar, Player.instance.playerData.exp, Player.instance.playerData.maxExp);

        barUI(hpBar, Player.instance.playerData.hp, Player.instance.playerData.maxHp);
        barUI(hpLateBar, Player.instance.playerData.hp, Player.instance.playerData.maxHp);

        characterBoardNormalVector = characterBoard.GetComponent<RectTransform>().transform.position;
        characterNormalVector = character.GetComponent<RectTransform>().transform.position;

        player = Player.instance;
    }

    public void characterHitUI()
    {
        character.sprite = player.playerDataSO.hitStateSprite;

        character.gameObject.transform.DOShakePosition(1f,4f);
        characterBoard.transform.DOShakePosition(1f, 4f);

        character.color = new Color32(255, 190, 190, 255);

        barUI(hpBar, Player.instance.playerData.hp, Player.instance.playerData.maxHp);
        hpBar.DOColor(new Color32(255, 90, 30, 255), 0.5f).SetEase(Ease.OutQuint).OnComplete( () => doLateAnimationUpdate());

    }

    private void doLateAnimationUpdate()
    {
        hpLateBarUI();
        characterNormalUI();

        characterBoard.GetComponent<RectTransform>().transform.position = characterBoardNormalVector;
        character.GetComponent<RectTransform>().transform.position = characterNormalVector;
    }

    private void hpLateBarUI()
    {
        barUI(hpLateBar, Player.instance.playerData.hp, Player.instance.playerData.maxHp);
        hpBar.DOColor(new Color32(155, 225, 100, 255), 0.4f);
    }

    private void characterNormalUI()
    {
        character.color = new Color32(255, 255, 255, 255);
        character.sprite = player.playerDataSO.normalStateSprite;
    }

    public void WeaponUpdateUI(int curClip, int maxClip)
    {
        wepaonTextUI(curClip, maxClip);

        // 퍼포먼스 수정 필수 ( / 안쓰기 )
        barUI(clipBar, curClip, maxClip);
    }

    public void barUI(Image image, float cur, float max)
    {
        float tempAmount = cur / max;

        image.DOFillAmount(tempAmount, 0.5f);
        //image.fillAmount = tempAmount;
    }

    public void levelTextUI(float level)
    {
        levelText.text = string.Format($"Lv . {level}");
    }


    private void wepaonTextUI(float cur, float max)
    {
        weaponClipText.text = string.Format($"{cur} / {max}");

        var rect = weaponClipText.gameObject.GetComponent<RectTransform>();

        rect.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0f);
        rect.DOScale(new Vector3(1, 1, 1), 0.1f).SetEase(Ease.InOutCubic);
    }
}
