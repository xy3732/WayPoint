using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : Singleton<Pooling>
{
    public GameObject bulletPrefab;

    [HideInInspector] public Queue<GameObject> bulletPool = new Queue<GameObject>();
    [HideInInspector] public Queue<GameObject> enemyPool = new Queue<GameObject>();

    private GameObject CreateObjects(Queue<GameObject> pool, GameObject insObject)
    {
        GameObject poolObject = Instantiate(insObject);
        poolObject.SetActive(false);

        return poolObject;
    }

    public GameObject getObject(ref Queue<GameObject> pool, Transform transform, GameObject gameObject)
    {
        GameObject getObject;

        if (pool.Count > 0) getObject = pool.Dequeue();
        else getObject = CreateObjects(enemyPool, gameObject);

        getObject.SetActive(true);
        getObject.transform.position = transform.position;

        return getObject;
    }

    //Instantiate() ����ؼ� ���
    public GameObject getObject(ref Queue<GameObject> pool,Transform transform)
    {
        GameObject getObject;

        // ���� ������Ʈ�� ������ �� ������Ʈ�� �� �Ѵ�.
        // ���� ť�� �� �ִ� ������Ʈ�� �����´�.
        if (pool.Count > 0) getObject = pool.Dequeue();
        // ������Ʈ�� ������ ����.
        else getObject = CreateObjects(bulletPool, bulletPrefab);

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
