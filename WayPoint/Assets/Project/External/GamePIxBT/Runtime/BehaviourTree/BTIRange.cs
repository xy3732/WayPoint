using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTIRange : MonoBehaviour
{
    [HideInInspector] public Container container;
    [HideInInspector] private BehaviourTreeRunner thisObject;

    // BehaviourTree 시작되면 불러와서 실행
    // awkae, Start로 container를 불러올려고 하니 OntringerEnter가 먼저 실행 된다.
    // 상위 스크립트가 실행되면 실행 되도록 설정.
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
