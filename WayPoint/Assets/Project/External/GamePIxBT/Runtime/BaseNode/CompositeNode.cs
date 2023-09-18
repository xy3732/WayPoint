using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Composite�� �������� �ڽ��� ������ �ִ�.
public abstract class CompositeNode : Node
{
    // �б��� ���
    [HideInInspector] public List<Node> children = new List<Node>();

    // �ش� ��� �����ؼ� ����
    public override Node Clone()
    {
        CompositeNode node = Instantiate(this);
        node.children = children.ConvertAll((c) => c.Clone());

        return node;
    }
}
