using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;
using TMPro;
public class UImanager : Singleton<UImanager>
{
    [SerializeField] private TextMeshProUGUI weaponClipText;

    [SerializeField] private Image clipBar;
    private void Start()
    {
        WeaponUpdateUI(Player.instance.weaponData.curClip, Player.instance.weaponData.maxClip);
    }

    public void WeaponUpdateUI(int curClip, int maxClip)
    {
        wepaonTextUI(curClip, maxClip);

        // 퍼포먼스 수정 필수 ( / 안쓰기 )
        weaponBarUI(curClip, maxClip);
    }



    public void weaponBarUI(float cur, float max)
    {
        float tempAmount = (float)cur / (float)max;
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
