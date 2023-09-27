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

    public PlayerSO playerDataSO;
    [field: SerializeField] public List<AbilitySO> abilitys { get; set; }
    [HideInInspector] public PlayerData playerData = new PlayerData();
    [HideInInspector] public BuffData buff { get; set; }

    private void Awake()
    {
        buff = new BuffData();
        buff.init();

        init();
    }

    private void Start()
    {
        playerInput.move = MoveTo;
        playerInput.idle = AnimationSetIdle;
        playerInput.shot = Shot;
        playerInput.reload = Reload;
        playerInput.hit = hit;
    }

    private void Update()
    {
        curHit += Time.deltaTime;    
    }

    // 이니시에이터
    private void init()
    {
        abilitys = new List<AbilitySO>();

        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        weaponObject = GameObject.FindGameObjectWithTag("Weapon");
        weaponData = weaponObject.GetComponent<WeaponData>();

        playerData.set(playerDataSO);

        animator.runtimeAnimatorController = playerDataSO.animator;
        weaponSprites.instance.set(playerDataSO);
    }

    public void addAbility(AbilitySO ability)
    {
        abilitys.Add(ability);
    }

    // 스프라이트 플립
    public void FlipSprite(Vector3 flip)
    {
        transform.localScale = flip;
    }

    // 발사
    private void Shot()
    {
        weaponData.Shot(buff.shotDelay);
    }

    // 재장전
    private void Reload()
    {
        weaponData.doReload();
    }

    public void getExp(float exp)
    {
        playerData.exp += exp;

        UImanager.instance.expBarUI(playerData.exp, playerData.maxExp);

        levelUp();
    }

    public void levelUp()
    {
        if(playerData.exp >= playerData.maxExp)
        {
            playerData.level++;
            playerData.exp = 0;

            UImanager.instance.levelTextUI(playerData.level);
            UImanager.instance.expBarUI(playerData.exp, playerData.maxExp);

            abilitySelector.instance.createButton();
        }
    }

    // 캐릭터 이동
    private void MoveTo(Vector3 input)
    {
        Vector3 moveVelocity = move(input);

        rigid.MovePosition(rigid.position + (Vector2)moveVelocity * playerData.speed * Time.smoothDeltaTime);

    }

    // 피격시
    private float curHit = 0f;
    private void hit()
    {
        if (curHit < playerData.invicivMax) return;
        curHit = 0;

        UImanager.instance.characterHitUI();

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

public class PlayerData
{
    public float speed { get; set; }
    public float maxHp { get; set; }
    public float hp { get; set; }
    public float sp { get; set; }
    public float exp { get; set; }
    public float maxExp { get; set; }
    public float maxSp { get; set; }
    public float invicivMax { get; set; }

    public int level { get; set; }
    public int abilitySelectAble { get; set; }

    public void set(PlayerSO data)
    {
        speed = data.speed;

        maxHp = data.maxHp;
        hp = maxHp;

        maxSp = data.maxSp;
        sp = 0;

        abilitySelectAble = data.abilitySelectAble;

        invicivMax = 0.2f;
        level = 1;
        exp = 0;
        maxExp = 100;
    }
}

public class BuffData
{
    public float reload { get; set; }
    public float shotDelay { get; set; }
    public float speed { get; set; }
    public float damage {get; set;}

    public void init()
    {
        reload = 1;
        speed = 1;

        shotDelay = 0;
        damage = 0;
        
    }
}

