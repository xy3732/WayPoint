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

    private UiManager uImanager;

    private void Awake()
    {
        isReload = false;
        maxReload = 4f;

        maxClip = 30;
        curClip = maxClip;

        shotMaxDelay = 0.2f;
    }

    private void Start()
    {
        uImanager = UiManager.instance;
    }

    private void Update()
    {
        if (isReload) Reload();
    }

    private void FixedUpdate()
    {
        shotCurDelay += Time.deltaTime; 

        if (isReload)  curReload += Time.deltaTime;

    }

    public void setClip()
    {
        maxClip = maxClip + (int)Player.instance.buff.clip;

        uImanager.WeaponUpdateUI(curClip, maxClip);
    }

    public void SetReloadSpeed()
    {
        float reloadBuff = 0.01f * (100 - Player.instance.buff.reload);
        maxReload = maxReload * reloadBuff;
    }

    public void Shot(BuffData buff, BulletSO bullet)
    {
        if (curClip <= 0 && !isReload) doReload();

        float buffDelay = 0.01f * (100 - buff.shotDelay);

        if (shotCurDelay < shotMaxDelay * buffDelay || curClip <= 0) return;

        // weapon 업데이트
        shotCurDelay = 0;
        curClip -= 1;

        // UI 업데이트
        uImanager.WeaponUpdateUI(curClip, maxClip);

        // 풀링
        Pooling pool = Pooling.instance;
        pool.bulletGetObject(ref pool.bulletPool,transform, pool.bulletPrefab, bullet);
    }

    private GameObject speechObject;

    // 재장전 시작
    public void doReload()
    {
        if (isReload) return;

        // 재장전 말풍선 생성
        speechObject = uImanager.speechUI();
        speechObject.GetComponent<SpeechBuble>().stopAnimation = false;
        speechObject.GetComponent<SpeechBuble>().reloadAnimation();

        isReload = true;

        curClip = 0;
        curReload = 0;

        uImanager.WeaponUpdateUI(curClip, maxClip);
    }


    // 재장전 중
    private void Reload()
    {
        uImanager.barUI(uImanager.clipBar, curReload, maxReload);

        if (curReload < maxReload) return;

        curClip = maxClip;
        isReload = false;

        // 재장전 말풍선 제거
        speechObject.GetComponent<SpeechBuble>().stopAnimation = true; 
        Pooling.instance.setObject(ref uImanager.speechPool, speechObject);
        
        // 남은 탄창 업데이트
        uImanager.WeaponUpdateUI(curClip, maxClip);
    }
}
