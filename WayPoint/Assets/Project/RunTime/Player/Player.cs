using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GamePix.CustomVector;

[RequireComponent(typeof(PlayerInput))]
public class Player : Singleton<Player>
{
    private Animator animator;
    private Rigidbody2D rigid;
    [HideInInspector]public PlayerInput playerInput;

    private GameObject weaponObject;

    [HideInInspector] public GameObject player;

    [HideInInspector] public WeaponData weaponData;

    [HideInInspector] public Vector3 Direction = FlipVector3.Default;

    public PlayerSO playerDataSO;

    // 플레이 도중에 얻는 어빌리티 임시 저장.
    [field: SerializeField] public List<AbilitySO> buffAbilitys { get; set; }
    [field: SerializeField] public List<AbilitySO> mainAbilitys { get; set; }
    [HideInInspector] public PlayerData playerData = new PlayerData();
    [HideInInspector] public BuffData buff { get; set; }

    private void Awake()
    {
        player = this.gameObject;

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
        foreach (var item in mainAbilitys)
        {
            item.update();
        }
    }

    // 이니시에이터
    private void init()
    {
        buffAbilitys = new List<AbilitySO>();

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
        if (ability.type == AbilitySO.SkillType.buff) buffAbilitys.Add(ability);
        else if (ability.type == AbilitySO.SkillType.Main) mainAbilitys.Add(ability);
    }

    // 스프라이트 플립
    public void FlipSprite(Vector3 flip)
    {
        transform.localScale = flip;
    }

    // 발사
    private void Shot()
    {
        weaponData.Shot(buff);
    }

    // 재장전
    private void Reload()
    {
        weaponData.doReload();
    }

    public void getExp(float exp)
    {
        playerData.exp += exp;

        UImanager.instance.barUI(UImanager.instance.expBar, playerData.exp, playerData.maxExp);

        levelUp();
    }

    public void levelUp()
    {
        if(playerData.exp >= playerData.maxExp)
        {
            playerData.level++;
            playerData.exp = 0;

            UImanager.instance.levelTextUI(playerData.level);
            UImanager.instance.barUI(UImanager.instance.expBar, playerData.exp, playerData.maxExp);

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
    private void hit(float damage)
    {
        if (curHit < playerData.invicivMax) return;
        curHit = 0;
        playerData.hp -= damage;

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
    public float critical { get; set; }

    public int level { get; set; }
    public int abilitySelectAble { get; set; }

    public void set(PlayerSO data)
    {
        speed = data.speed;

        maxHp = data.maxHp;
        hp = maxHp;

        maxSp = data.maxSp;
        sp = 0;

        critical = data.critical;

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
    public float damage { get; set; }
    public float critical { get; set; }

    public float clip { get; set; }

    public void init()
    {
        reload = 1;
        speed = 1;

        shotDelay = 0;
        damage = 0;
        critical = 0;
        clip = 0;
        
    }
}

