using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// pixel perfect camera component
using UnityEngine.Experimental.Rendering.Universal;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector] GameObject cameraObject;

    [HideInInspector] public Vector3 mousePos { get; set; }

    private void Awake()
    {
        // 프레임 타겟
        Application.targetFrameRate = 122;

        if (cameraObject == null) cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Start()
    {
        // pixel perfect 컴포넌트가 있을시에만 실행
        var pixelPerfect = cameraObject?.GetComponent<PixelPerfectCamera>();
        if (pixelPerfect != null) pixelPerfect.enabled = true;
    }

    private void FixedUpdate()
    {
        var screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        screenPos.z = 0;

        mousePos = screenPos;
    }
}
