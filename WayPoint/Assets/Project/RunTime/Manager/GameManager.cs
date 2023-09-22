using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// pixel perfect camera component
using UnityEngine.Experimental.Rendering.Universal;

public class GameManager : Singleton<GameManager>
{
    private Pooling pooling;

    private GameObject cameraObject;

    [HideInInspector] public Vector3 mousePos { get; set; }

    private void Awake()
    {
        // ������ Ÿ��
        Application.targetFrameRate = 122;

        cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        pooling = GetComponent<Pooling>();
    }

    private void Start()
    {
        // pixel perfect ������Ʈ�� �����ÿ��� ����
        var pixelPerfect = cameraObject?.GetComponent<PixelPerfectCamera>();
        if (pixelPerfect != null) pixelPerfect.enabled = true;
    }

    private void FixedUpdate()
    {
        // ���콺 ������ ������Ʈ
        var screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        screenPos.z = 0;
        mousePos = screenPos;
    }
}
