using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container
{
    [HideInInspector] public Rigidbody2D rigid;
    [HideInInspector] public Transform transform;
    //[HideInInspector] public Sprite 

    [HideInInspector] public bool stopVelocity = false;
    [HideInInspector] public bool isInrange = false;
    [HideInInspector] public bool isStopMove = false;

    // Script Only
    [HideInInspector] public string textScript;
    [HideInInspector] public bool isScriptTriger = false;
    [HideInInspector] public int buttonSelectNumber = -1;
    [HideInInspector] public ScriptSequenceNode scriptSequence;

    //Enemy Only
    [HideInInspector] public int Hp { get; set; }

    public static Container CreateFromGameObject(GameObject gameObject)
    {
        Container container = new Container();

        container.rigid = gameObject.GetComponent<Rigidbody2D>();
        container.transform = gameObject.GetComponent<Transform>();

        return container;
    }
}
