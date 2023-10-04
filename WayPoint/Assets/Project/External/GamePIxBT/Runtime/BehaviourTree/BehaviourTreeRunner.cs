using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeRunner : MonoBehaviour
{
    public enum TreeType
    {
        gameObject,
        script
    }
    [field: SerializeField]public TreeType type { get; set; }

    public BehaviourTree tree;

    private Pooling pool;

    public GameObjectSO so;

    // 오브젝트의 값을 저장
    public Container container = null;
    private void Awake()
    {
        bindTree();
    }

    private void Start()
    {
        pool = Pooling.instance;
    }

    private void OnEnable()
    {
        Init();
    }

    private void Update()
    {
        // tree가 없으면 취소
        if (!tree) return;

        tree.Update();
    }

    public void hit(float damage)
    {
        DamageFontContainer damageEffect = DamageFontContainer.instance;

        float hitDamage;
        float random = Random.Range(0, 100f);
        if (Player.instance.playerData.critical >= random)
        {
            hitDamage = damage * 3;
            damageEffect.createDamageEffect(gameObject, hitDamage, new Color32(255, 120, 60, 255));
        }
        else
        {
            hitDamage = damage;
            damageEffect.createDamageEffect(gameObject, hitDamage, new Color32(255, 255, 255, 255));
        }
        container.Hp -= hitDamage;

        isDie();
    }
    public void isDie()
    {
        if (container.Hp <= 0)
        {
            Player.instance.getExp(so.exp);
            pool.setObject(ref pool.enemyPool, gameObject);
        }
    }

    private void bindTree()
    {
        // 컨테이너 생성
        // 기본적인 값을 설정. [rigidbody, transform 등] 저장.
        container = CreateBehaviourTreeContext();

        // 하위에 있는 스크립트 Init
        var component = gameObject?.GetComponentInChildren<BTIRange>();
        component?.OnRunnerAwake(container);

        Init();

        // 실행되면 트리 복사 해서 사용.
        tree = tree.Clone();
        tree.Bind(container);
    }
    static float halfMin = 1.0f / 30.0f;
    private void Init()
    {
        switch (type)
        {
            case TreeType.gameObject:

                GameManager data = GameManager.instance;
                // 정보 Init
                container.Hp = (int)so.hp + (int)((data.timer * halfMin) * 5);
                container.stopVelocity = true;

                break;

            case TreeType.script:
                break;
        }
    }

    // 추후 UImanager로 이동할 예정
    public void ScriptTrigerBtn()
    {
        if(gameObject.activeSelf) container.isScriptTriger = true;
    }

    // 컨테이너 생성
    Container CreateBehaviourTreeContext()
    {
        // 이미 컨테이너를 가지고 있으면 리턴
        if (container != null) return container;

        // 없으면 생성
        return Container.CreateFromGameObject(gameObject);
    }
}
