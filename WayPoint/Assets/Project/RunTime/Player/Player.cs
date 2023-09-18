using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GamePix.CustomVector;
public class Player : Singleton<Player>
{
    [HideInInspector] private Animator animator;
    [HideInInspector] private Rigidbody2D rigid;

    [HideInInspector] public Vector3 Direction = FlipVector3.Default;

    public float moveSpeed;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        characterFlip();

        FixedKeyInput();
    }

    // 키 INPUT [ FIXED 전용]
    void FixedKeyInput()
    {
        Vector2 moveVelocity = Vector2.zero;

        if (Input.GetKey(KeyCode.A)) moveVelocity = move(Vector3.left);
        else if (Input.GetKey(KeyCode.D)) moveVelocity = move(Vector3.right);
        else if (Input.GetKey(KeyCode.W)) moveVelocity = move(Vector3.up);
        else if (Input.GetKey(KeyCode.S)) moveVelocity = move(Vector3.down);
        else
        {
            animator.SetBool("isMove",false);
        }

        rigid.MovePosition(rigid.position + moveVelocity * moveSpeed * Time.smoothDeltaTime);
    }

    // 캐릭터 뒤집기
    private void characterFlip()
    {
        transform.localScale = Direction;
    }

    // 캐릭터 이동
    private Vector3 move(Vector3 velocity)
    {
        animator.SetBool("isMove", true);

        if (Input.GetKey(KeyCode.S)) velocity = (velocity + Vector3.down).normalized;
        if (Input.GetKey(KeyCode.W)) velocity = (velocity + Vector3.up).normalized;

        return velocity;
    }
}
