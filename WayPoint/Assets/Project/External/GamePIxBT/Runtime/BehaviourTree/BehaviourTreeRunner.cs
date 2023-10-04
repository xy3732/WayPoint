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

    // ������Ʈ�� ���� ����
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
        // tree�� ������ ���
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
        // �����̳� ����
        // �⺻���� ���� ����. [rigidbody, transform ��] ����.
        container = CreateBehaviourTreeContext();

        // ������ �ִ� ��ũ��Ʈ Init
        var component = gameObject?.GetComponentInChildren<BTIRange>();
        component?.OnRunnerAwake(container);

        Init();

        // ����Ǹ� Ʈ�� ���� �ؼ� ���.
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
                // ���� Init
                container.Hp = (int)so.hp + (int)((data.timer * halfMin) * 5);
                container.stopVelocity = true;

                break;

            case TreeType.script:
                break;
        }
    }

    // ���� UImanager�� �̵��� ����
    public void ScriptTrigerBtn()
    {
        if(gameObject.activeSelf) container.isScriptTriger = true;
    }

    // �����̳� ����
    Container CreateBehaviourTreeContext()
    {
        // �̹� �����̳ʸ� ������ ������ ����
        if (container != null) return container;

        // ������ ����
        return Container.CreateFromGameObject(gameObject);
    }
}
