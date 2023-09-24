using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GamePix.CustomVector;

[RequireComponent(typeof(PlayerInput))]
public class Player : Singleton<Player>
{
    private Animator animator;
    private Rigidbody2D rigid;
    private PlayerInput playerInput;
    
    private GameObject weaponObject;

    [HideInInspector] public WeaponData weaponData;

    [HideInInspector] public Vector3 Direction = FlipVector3.Default;

    [HideInInspector] public float moveSpeed;

    public PlayerSO playerDataSO;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        weaponObject = GameObject.FindGameObjectWithTag("Weapon");
        weaponData = weaponObject.GetComponent<WeaponData>();
    }

    private void Start()
    {
        init();

        playerInput.move = MoveTo;
        playerInput.idle = AnimationSetIdle;
        playerInput.shot = Shot;
        playerInput.reload = Reload;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            hit();
        }
    }

    public void init()
    {
        animator.runtimeAnimatorController = playerDataSO.animator;
        moveSpeed = playerDataSO.speed;
        weaponSprites.instance.set(playerDataSO);
    }

    public void FlipSprite(Vector3 flip)
    {
        transform.localScale = flip;
    }

    private void Shot()
    {
        weaponData.Shot();
    }

    private void Reload()
    {
        weaponData.doReload();
    }

    // 캐릭터 이동
    private void MoveTo(Vector3 input)
    {
        Vector3 moveVelocity = move(input);

        rigid.MovePosition(rigid.position + (Vector2)moveVelocity * moveSpeed * Time.smoothDeltaTime);

    }

    private void hit()
    {
        rigid.velocity = Vector2.zero;
    }

    // 에니메이션 IDLE 로 설정
    private void AnimationSetIdle()
    {
        rigid.velocity = Vector2.zero;
        animator.SetBool("isMove", false);
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
