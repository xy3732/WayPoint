using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GamePix.bulletType;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [field: SerializeField] public BulletType type { get; set; }
    [field: SerializeField] public float lifeTime { get; set; }
    [field : SerializeField] public float damage { get; set; }
    



    public GameObject target { get; set; }

    private float limt = 0;
    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        limt = 0;
        lifeTime = 2f;

        damage += Player.instance.buff.damage;
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case BulletType.bullet:
                rigid.velocity = transform.up * 25f;
                break;

            case BulletType.missile:    
                Vector3 targetPos = target != null ? target.transform.position : new Vector3(1,1,1); 

                Vector2 dir = (Vector2)targetPos - rigid.position;
                dir.Normalize();

                float rotateAmount;

                if (target == null) rotateAmount = 0;
                else rotateAmount = Vector3.Cross(dir, transform.up).z;

                rigid.angularVelocity = -rotateAmount * 100f;
                rigid.velocity = transform.up * 10f;

                break;
        }

        limt += Time.deltaTime;
        if (limt > lifeTime) Pooling.instance.setObject(ref Pooling.instance.bulletPool,gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("enemy"))
        {
            Pooling.instance.setObject(ref Pooling.instance.bulletPool, gameObject);
        }
    }
}
