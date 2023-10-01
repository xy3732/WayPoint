using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

using GamePix.bulletType;
public class Pooling : Singleton<Pooling>
{
    public GameObject bulletPrefab;

    [HideInInspector] public Queue<GameObject> bulletPool = new Queue<GameObject>();
    [HideInInspector] public Queue<GameObject> enemyPool = new Queue<GameObject>();

    private GameObject CreateObjects(GameObject insObject)
    {
        GameObject poolObject = Instantiate(insObject);

        return poolObject;
    }

    public GameObject getObject(ref Queue<GameObject> pool, Transform transform, GameObject gameObject)
    {
        GameObject getObject;

        if (pool.Count > 0) getObject = pool.Dequeue();
        else getObject = CreateObjects(gameObject);

        getObject.SetActive(true);
        getObject.transform.position = transform.position;

        return getObject;
    }

    public GameObject getSpeechObject(ref Queue<GameObject> pool, GameObject parent, GameObject gameObject)
    {
        GameObject getObject;

        if (pool.Count > 0) getObject = pool.Dequeue();
        else getObject = CreateObjects(gameObject);

        getObject.SetActive(true);

        return getObject;
    }

    // DamageFont ��
    public GameObject getUiObject(ref Queue<GameObject> pool,GameObject uiParent, GameObject parent, GameObject gameObject, float damage, Color32 color)
    {
        GameObject getObject;

        if (pool.Count > 0) getObject = pool.Dequeue();
        else getObject = CreateObjects(gameObject);

        getObject.GetComponent<DamageFont>().damage = damage;
        getObject.GetComponent<TextMeshProUGUI>().color = color;

        getObject.SetActive(true);
        getObject.transform.SetParent(uiParent.transform);
        getObject.transform.position = Camera.main.WorldToScreenPoint(parent.transform.position);

        getObject.GetComponent<DamageFont>().DoAnimation();

        return getObject;
    }

    public GameObject abilityGetObject(ref Queue<GameObject> pool, GameObject parent, GameObject gameObject)
    {
        GameObject getObject;

        if (pool.Count > 0) getObject = pool.Dequeue();
        else getObject = CreateObjects(gameObject);

        getObject.SetActive(true);
        getObject.transform.SetParent(parent.transform);

        return getObject;
    }

    //Instantiate() ����ؼ� ���
    public GameObject bulletGetObject(ref Queue<GameObject> pool,Transform transform, GameObject bullet, BulletSO bulletType)
    {
        GameObject getObject;

        // ���� ������Ʈ�� ������ �� ������Ʈ�� �� �Ѵ�.
        // ���� ť�� �� �ִ� ������Ʈ�� �����´�.
        if (pool.Count > 0) getObject = pool.Dequeue();
        // ������Ʈ�� ������ ����. 
        else getObject = CreateObjects(bullet);

        var bulletSettings = getObject.GetComponent<Bullet>();
        var image = bulletSettings.gameObject.GetComponent<SpriteRenderer>();
        var anim = bulletSettings.gameObject.GetComponent<Animator>();

        bulletSettings.type = bulletType.type;
        bulletSettings.lifeTime = bulletType.lifeTime;
        bulletSettings.damage = bulletType.damage;

        if(bulletSettings.type == BulletType.bullet)
        {
            anim.runtimeAnimatorController = null;
            image.sprite = bulletType.BulletSprite;
            
        }
        else if(bulletSettings.type == BulletType.missile)
        {
            anim.runtimeAnimatorController = bulletType.MissileAnimator;
            image.sprite = null;
        }

        // �ش� ������Ʈ�� Ȱ��ȭ
        getObject.gameObject.SetActive(true);
        getObject.transform.position = transform.position;
        getObject.transform.eulerAngles = new Vector3(0,0,transform.eulerAngles.z - 90);

        return getObject;
    }

    // Destroy() ����ؼ� ���
    public void setObject(ref Queue<GameObject> pool,GameObject obj)
    {
        // �ش� ������Ʈ�� ��Ȱ��ȭ
        obj.gameObject.SetActive(false);

        // �ٽ� ť�� ����
        pool.Enqueue(obj);
    }
}
