using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeRunner : MonoBehaviour
{
    public BehaviourTree tree;

    // ������Ʈ�� ���� ����
    public Container container = null;
    private void Awake()
    {
        bindTree();
    }

    private void Update()
    {
        // tree�� ������ ���
        if (!tree) return;

        tree.Update();
    }

    private void bindTree()
    {
        // �����̳� ����
        // �⺻���� ���� ����. [rigidbody, transform ��] ����.
        container = CreateBehaviourTreeContext();

        // ������ �ִ� ��ũ��Ʈ Init
        var component = gameObject?.GetComponentInChildren<BTIRange>();
        component?.OnRunnerAwake(container);

        Debug.Log(component);

        // ����Ǹ� Ʈ�� ���� �ؼ� ���.
        tree = tree.Clone();
        tree.Bind(container);
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
