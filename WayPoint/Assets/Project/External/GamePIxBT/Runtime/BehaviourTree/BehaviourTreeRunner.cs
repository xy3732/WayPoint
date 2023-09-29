using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeRunner : MonoBehaviour
{
    public BehaviourTree tree;

    private Pooling pool;

    public EnemySO so;

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

        container.Hp -= damage;
        damageEffect.createDamageEffect(gameObject, damage);

        isDie();
    }
    public void isDie()
    {
        if(container.Hp <= 0)
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
        GameManager data = GameManager.instance;
        // ���� Init
        container.Hp = (int)so.hp + (int)(data.timer * halfMin);
        container.stopVelocity = true;
    }

    // ���� UImanager�� �̵��� ����
    public void ScriptTrigerBtn()
    {
        container.isScriptTriger = true;
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
