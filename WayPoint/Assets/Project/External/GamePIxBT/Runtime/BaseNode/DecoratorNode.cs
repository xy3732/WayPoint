using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Decorator�� �⺻������ �ڽ��� �ϳ��� ������ �ִ�.
public abstract class DecoratorNode : Node
{
    public Node child;

    // �ش� ��带 �����ؼ� ����.
    public override Node Clone()
    {
        DecoratorNode node = Instantiate(this);

        node.child = child.Clone();

        return node;
    }
}
