using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �̿ϼ� - ���� Script Merge Node�� ��밡 �ɰ�
/// ��ũ��Ʈ �շ�����
/// </summary>
public abstract class ConfluenceNode : Node
{
    public List<Node> parents = new List<Node>();
    [HideInInspector] public Node child;

    // �ش� ��带 �����ؼ� ����.
    public override Node Clone(Node input)
    {
        ConfluenceNode node = null;

        node = Instantiate(this);
        node.child = child.Clone();

        return node;
    }
}
