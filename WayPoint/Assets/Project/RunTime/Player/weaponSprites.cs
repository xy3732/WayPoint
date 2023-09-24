using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponSprites : Singleton<weaponSprites>
{
    [HideInInspector] public SpriteRenderer weapon { get; set; }
    [HideInInspector] public SpriteRenderer leftHand { get; set; }
    [HideInInspector] public SpriteRenderer rightHand { get; set; }

    private void Awake()
    {
        weapon = GetComponent<SpriteRenderer>();
        leftHand = transform.GetChild(0).GetComponent<SpriteRenderer>();
        rightHand = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    public void set(PlayerSO data)
    {
        weapon.sprite = data.weapon;
        leftHand.sprite = data.leftHand;
        rightHand.sprite = data.rightHand;
    }
}
