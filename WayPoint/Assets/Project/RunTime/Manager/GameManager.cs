using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// pixel perfect camera component
using UnityEngine.Experimental.Rendering.Universal;

public class GameManager : Singleton<GameManager>
{
    private Pooling pooling;

    private GameObject cameraObject;
    private SpawnManager spawnManager;

    [HideInInspector] public Vector3 mousePos { get; set; }
    [HideInInspector] public float timer { get; set; }
    [HideInInspector] public int spawnLevel { get; set; }

    private void Awake()
    {
        // 프레임 타겟
        Application.targetFrameRate = 122;

        cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        pooling = GetComponent<Pooling>();

        timer = 0;
        spawnLevel = 0;
    }

    private void Start()
    {
        spawnManager = SpawnManager.instance;

        // pixel perfect 컴포넌트가 있을시에만 실행
        var pixelPerfect = cameraObject?.GetComponent<PixelPerfectCamera>();
        if (pixelPerfect != null) pixelPerfect.enabled = true;
    }

    public static float min = 1.0f / 5.0f;
    private void Update()
    {
        timer += Time.deltaTime;

        // 스폰 레벨
        spawnLevel = (int)(timer * min);
        // 스폰 레벨이 스폰slot보다 크면 최대치로 재조정
        if (spawnLevel >= spawnManager.spawnSlot.Length)
        {
            spawnLevel = spawnManager.spawnSlot.Length - 1;
        }

    }

    private void FixedUpdate()
    {
        // 마우스 포지션 업데이트
        var screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        screenPos.z = 0;
        mousePos = screenPos;
    }
}
