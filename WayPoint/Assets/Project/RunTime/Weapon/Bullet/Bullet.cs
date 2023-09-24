using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    Rigidbody2D rigid;

    float limt = 0;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        limt = 0;
    }

    private void LateUpdate()
    {
        rigid.velocity = transform.up * 25f;

        limt += Time.deltaTime;
        if (limt > 2f) Pooling.instance.setObject(ref Pooling.instance.bulletPool,gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("enemy"))
        {
            Pooling.instance.setObject(ref Pooling.instance.bulletPool, gameObject);
        }
    }
}
