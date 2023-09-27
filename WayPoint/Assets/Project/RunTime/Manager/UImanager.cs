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
    [field: SerializeField] private TextMeshProUGUI levelText { get; set; }
    [field: SerializeField] private Image expBar { get; set; }
    private void Start()
    {
        WeaponUpdateUI(Player.instance.weaponData.curClip, Player.instance.weaponData.maxClip);
        expBarUI(Player.instance.playerData.exp, Player.instance.playerData.maxExp);
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
