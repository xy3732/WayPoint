using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : Singleton<WeaponData>
{
    [HideInInspector] public int maxClip { get; set; }
    [HideInInspector] public int curClip { get; set; }

    private float shotCurDelay = 0;
    [HideInInspector] public float shotMaxDelay { get; set; }

    [HideInInspector] public bool isReload { get; set; }
    private float curReload = 0;
    [HideInInspector] public float maxReload { get; set; }

    private UImanager uImanager;

    private void Awake()
    {
        isReload = false;
        maxReload = 4f;

        maxClip = 31;
        curClip = maxClip;

        shotMaxDelay = 0.1f;
    }

    private void Start()
    {
        uImanager = UImanager.instance;
    }

    private void Update()
    {
        if (isReload) Reload();
    }

    private void FixedUpdate()
    {
        shotCurDelay += Time.deltaTime;
        if(isReload) curReload += Time.deltaTime;
    }

    public void Shot()
    {
        if (curClip <= 0 && !isReload) doReload();

        if (shotCurDelay < shotMaxDelay || curClip <= 0) return;

        // weapon ������Ʈ
        shotCurDelay = 0;
        curClip -= 1;

        // UI ������Ʈ
        uImanager.WeaponUpdateUI(curClip, maxClip);

        // Ǯ��
        Pooling.instance.getObject(ref Pooling.instance.bulletPool,transform);
    }

    public void doReload()
    {
        if (isReload) return;

        isReload = true;

        curClip = 0;
        curReload = 0;

        uImanager.WeaponUpdateUI(curClip, maxClip);
    }

    private void Reload()
    {
        uImanager.weaponBarUI(curReload, maxReload);

        if (curReload < maxReload) return;

        curClip = maxClip;
        isReload = false;

        uImanager.WeaponUpdateUI(curClip, maxClip);
    }
}
