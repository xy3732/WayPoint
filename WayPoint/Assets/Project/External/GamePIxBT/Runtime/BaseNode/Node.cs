using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node : ScriptableObject
{
    public enum State
    {
        Running,
        Failure,
        Success
    }

    // �⺻ State�� Running���� ����
    [HideInInspector] public State state = State.Running;
    [HideInInspector] public bool started = false;
    public string guid { get; set; }
    [HideInInspector] public Vector2 position;
    [HideInInspector] public BTContainer btContainer;
    [HideInInspector] public Container container;

    // ���� ����� ���� �ۼ� ����
    [TextArea(2,4)] public string description;
    public bool alreadySelected = false;
    // Script Only
    [HideInInspector] public int selectNode = 0;
    public State Update()
    {
        // �ش� ��尡 ó�� ���� �ɋ� Onstart�� ����
        if(!started)
        {
            OnStart();
            started = true;
        }

        // �ش� ��带 ���������� ������Ʈ
        state = OnUpdate();

        // �ش� ��尡 ����, ���� �ϸ� OnStop�� ����
        if(state == State.Failure || state == State.Success)
        {
            OnStop();
            started = false;
        }

        // ������ state�� ��ȯ�Ѵ�.
        return state;
    }


    // ���� �ߴ�
    public void Abort()
    {
        BehaviourTree.Traverse(this, (n) =>
        {
            n.started = false;
            n.state = State.Running;
            n.OnStop();
        });
    }

    // ��� Ŭ�� ���� 
    public virtual Node Clone()
    {
        return Instantiate(this);
    }

    // ��� Ŭ�� ���� 
    public virtual Node Clone(Node node)
    {
        return Instantiate(this);
    }

    protected abstract void OnStart();

    protected abstract State OnUpdate();
    protected abstract void OnStop();

}
