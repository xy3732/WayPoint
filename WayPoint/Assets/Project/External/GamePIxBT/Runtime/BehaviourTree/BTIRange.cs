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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (container == null) return;

        if(other.CompareTag("Bullet"))
        {
            container.Hp -= 1;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (container == null) return;

        container.stopVelocity = true;
    }
}
