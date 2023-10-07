using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// pixel perfect camera component
using UnityEngine.Experimental.Rendering.Universal;
using System.Linq;

using DG.Tweening;

public class GameManager : Singleton<GameManager>
{
    private Pooling pooling;

    private GameObject cameraObject;
    private SpawnManager spawnManager;
    private PlayerInput playerInput;
    [HideInInspector] public Vector3 mousePos { get; set; }
    [HideInInspector] public float timer { get; set; }
    [HideInInspector] public int spawnLevel { get; set; }
    [HideInInspector] public bool isPause { get; set; }

    [field: SerializeField] public AbilitySO[] abilitys { get; set; }

    [field: SerializeField] public GameObject debugTestObject { get; set; }
    private void Awake()
    {
        // ������ Ÿ��
        Application.targetFrameRate = 122;

        cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        pooling = GetComponent<Pooling>();

        isPause = false;

        timer = 0;
        spawnLevel = 0;
    }

    private void Start()
    {
        playerInput = Player.instance.playerInput;
        spawnManager = SpawnManager.instance;

        // pixel perfect ������Ʈ�� �����ÿ��� ����
        var pixelPerfect = cameraObject?.GetComponent<PixelPerfectCamera>();
        if (pixelPerfect != null) pixelPerfect.enabled = true;

        playerInput.Escape = DoEsacpe;
    }

    private void DoEsacpe()
    {
        isPause = !isPause;
        Time.timeScale = isPause ? 0 : 1;
    }

    public static float min = 1.0f / 999f;
    private void Update()
    {
        timer += Time.deltaTime;

        // ���� ����
        spawnLevel = (int)(timer * min);
        // ���� ������ ����slot���� ũ�� �ִ�ġ�� ������
        if (spawnLevel >= spawnManager.spawnSlot.Length)
        {
            spawnLevel = spawnManager.spawnSlot.Length - 1;
        }

        if (Input.GetKeyDown(KeyCode.F2)) debugTestObject.SetActive(true);
        if (Input.GetKeyDown(KeyCode.F1)) abilitySelector.instance.createButton();
    }

    private void FixedUpdate()
    {
        // ���콺 ������ ������Ʈ
        var screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        screenPos.z = 0;
        mousePos = screenPos;
    }

    public void abilityDelete(int ID)
    {
        for(int i=0; i< abilitys.Length; i++)
        {
            if(abilitys[i].id == ID)
            {
                abilitys = abilitys.Where(n => n.id != ID).ToArray();
                break;
            }
        }
    }
}
