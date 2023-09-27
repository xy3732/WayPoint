using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;
using TMPro;
public class UImanager : Singleton<UImanager>
{
    [field: SerializeField] private TextMeshProUGUI weaponClipText { get; set; }

    [field: SerializeField] private Image clipBar { get; set; }
    
    [field: Space(20)]
    [field: SerializeField] private Image characterBoard { get; set; }
    [field: SerializeField] private Image character { get; set; }
    [field: SerializeField] private Image hpBar { get; set; }
    [field: SerializeField] private Image spBar { get; set; }
    
    [field: Space(20)]
    [field: SerializeField] private TextMeshProUGUI levelText { get; set; }
    [field: SerializeField] private Image expBar { get; set; }

    private Player player;
    private void Start()
    {
        WeaponUpdateUI(Player.instance.weaponData.curClip, Player.instance.weaponData.maxClip);
        expBarUI(Player.instance.playerData.exp, Player.instance.playerData.maxExp);

        player = Player.instance;
    }

    public void characterHitUI()
    {
        character.sprite = player.playerDataSO.hitStateSprite;

        character.gameObject.transform.DOShakePosition(1f,2f);
        characterBoard.gameObject.transform.DOShakePosition(1f, 2f);

        Invoke("characterNormalUI", 0.5f);
    }

    private void characterNormalUI()
    {
        character.sprite = player.playerDataSO.normalStateSprite;
    }

    public void WeaponUpdateUI(int curClip, int maxClip)
    {
        wepaonTextUI(curClip, maxClip);

        // 퍼포먼스 수정 필수 ( / 안쓰기 )
        weaponBarUI(curClip, maxClip);
    }

    public void expBarUI(float cur, float max)
    {
        float tempAmount = cur / max;
        expBar.fillAmount = tempAmount;
    }

    public void levelTextUI(float level)
    {
        levelText.text = string.Format($"Lv . {level}");
    }

    public void weaponBarUI(float cur, float max)
    {
        float tempAmount = cur / max;
        clipBar.fillAmount = tempAmount;
    }

    private void wepaonTextUI(float cur, float max)
    {
        weaponClipText.text = string.Format($"{cur} / {max}");

        var rect = weaponClipText.gameObject.GetComponent<RectTransform>();

        rect.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0f);
        rect.DOScale(new Vector3(1, 1, 1), 0.1f).SetEase(Ease.InOutCubic);
    }
}
