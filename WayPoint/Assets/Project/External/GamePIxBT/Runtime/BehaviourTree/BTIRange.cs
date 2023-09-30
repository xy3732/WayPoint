using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTIRange : MonoBehaviour
{
    [HideInInspector] public Container container;
    [HideInInspector] private BehaviourTreeRunner thisObject;

    // BehaviourTree ���۵Ǹ� �ҷ��ͼ� ����
    // awkae, Start�� container�� �ҷ��÷��� �ϴ� OntringerEnter�� ���� ���� �ȴ�.
    // ���� ��ũ��Ʈ�� ����Ǹ� ���� �ǵ��� ����.
    public void OnRunnerAwake(Container container)
    {
        this.container = container;

        thisObject = transform.GetComponentInParent<BehaviourTreeRunner>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (container == null) return;

        if (other.CompareTag("map"))
        {
            container.stopVelocity = true;
        }

        if (other.CompareTag("Bullet"))
        {
            var bullet = other.GetComponent<Bullet>();
            thisObject.hit(bullet.damage);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (container == null) return;

        container.stopVelocity = true;
    }
}
