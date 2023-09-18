using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTIRange : MonoBehaviour
{
    [HideInInspector] public Container container;

    // BehaviourTree ���۵Ǹ� �ҷ��ͼ� ����
    // awkae, Start�� container�� �ҷ��÷��� �ϴ� OntringerEnter�� ���� ���� �ȴ�.
    // ���� ��ũ��Ʈ�� ����Ǹ� ���� �ǵ��� ����.
    public void OnRunnerAwake(Container container)
    {
        this.container = container;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (container == null) return;

        container.isStopMove = true;
        container.isInrange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (container == null) return;

        container.isStopMove = false;
        container.isInrange = false;
    }
}
