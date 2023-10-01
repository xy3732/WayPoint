using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using GamePix.CustomVector;
public class ShirokoDrone : MonoBehaviour
{
    [field: SerializeField] public float lifeTime { get; set; }
    [field: SerializeField] public float shotDelay { get; set; }
    private float curTime { get; set; }
    private float curShotDelay { get; set; }

    [field: Space(20)]
    [field : SerializeField] public GameObject bullet { get; set; }
    [field: SerializeField] public GameObject shotPos { get; set; }
    [field: SerializeField] public GameObject target { get; set; }
    [field: SerializeField] private GameObject droneObject { get; set; }
    private SpriteRenderer droneSprite { get; set; }
    [field: SerializeField] private GameObject shadowObject { get; set; }
    private SpriteRenderer shadowSprite { get; set; }
    [field: SerializeField] private GameObject inRangeObject { get; set; }
    private ShirokoDroneInRange inRange { get; set; }

    [field: SerializeField] private BulletSO bulletType { get; set; }

    private Vector3 shadowAnchor = new Vector3(0,-1f,0);
    private bool doAnchor = true;
    private void OnEnable()
    {
        curTime = 0;
        curShotDelay = 0;

        doAnchor = true;

        if (inRange == null) inRange = inRangeObject.GetComponent<ShirokoDroneInRange>();
        if (droneSprite == null) droneSprite = droneObject.GetComponent<SpriteRenderer>();
        if (shadowSprite == null) shadowSprite = shadowObject.GetComponent<SpriteRenderer>();

        droneObject.transform.DOScale(new Vector3(0.5f, 1.5f, 1f), 0f);
        droneObject.transform.DOMove(Player.instance.transform.position, 0f);

        droneSprite.DOColor(new Color32(255,255,255,70),0f);
        shadowSprite.DOColor(new Color32(255,255,255,50),0f);

        OnStart();
    }

    private void Update()
    {
        if(doAnchor) shadowObject.transform.DOMove(droneObject.transform.position + shadowAnchor, 0f);

        if (target == null)
        {
            inRange.isFireReady = false;
            setTarget(inRange.shortDistanceObject());
        }

        if (target != null)
        {
            flipSprite();
            doShot();
        }
        

        curTime += Time.deltaTime;
        curShotDelay += Time.deltaTime;

        if (curTime >= lifeTime) OnStop();
    }

    public void setTarget(GameObject target)
    {
        this.target = target;
    }

    private void flipSprite()
    {
        if(target.transform.position.x >= droneObject.transform.position.x)
        {
            droneObject.transform.localScale = FlipVector3.Left;
        }
        else
        {
            droneObject.transform.localScale = FlipVector3.Right;
        }
    }

    private void doShot()
    {
        if (curShotDelay < shotDelay) return;
        curShotDelay = 0;

        Pooling pool = Pooling.instance;
        var missile = pool.bulletGetObject(ref pool.bulletPool, transform, bullet, bulletType);
        missile.transform.position = shotPos.transform.position;

        if(target.transform.position.x >= droneObject.transform.position.x)
        {
            missile.transform.eulerAngles = new Vector3(0, 0,-90);
        }
        else
        {
            missile.transform.eulerAngles = new Vector3(0, 0, 90);
        }

        missile.GetComponent<Bullet>().target = target; 
    }

    private void OnStart()
    {
        droneObject.transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f).SetEase(Ease.OutBack);

        droneSprite.DOColor(new Color32(255,255,255,255),0.5f).SetEase(Ease.OutExpo);
        shadowSprite.DOColor(new Color32(255,255,255,170), 0.5f).SetEase(Ease.OutExpo);
    }

    private void OnStop()
    {
        doAnchor = false;

        droneObject.transform.DOMove(droneObject.transform.position + new Vector3(0,1,0), 0.4f).SetEase(Ease.OutBack);

        droneSprite.DOColor(new Color32(255, 255, 255, 0), 0.5f).SetEase(Ease.OutExpo);
        shadowSprite.DOColor(new Color32(255, 255, 255, 0), 0.5f).SetEase(Ease.OutExpo).OnComplete(() => setPool());
    }

    private void setPool()
    {
        Pooling.instance.setObject(ref Player.instance.abilityPool, this.gameObject);
    }
}

