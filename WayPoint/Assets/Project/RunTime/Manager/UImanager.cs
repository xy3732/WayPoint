using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;
using TMPro;
public class UIManager : Singleton<UIManager>
{

    // Scripts
    [field: SerializeField] public GameObject scriptsUiObject { get; set; }
    [field: SerializeField] private TextMeshProUGUI schoolClubText { get; set; }
    [field: SerializeField] private TextMeshProUGUI characterNameText { get; set; }
    [field: SerializeField] private TextMeshProUGUI scriptText { get; set; }
    private string message { get; set; }
    private int index { get; set;}
    [field: SerializeField] [field: Range(0,30)]private int CPS { get; set; }
    [field: SerializeField] private GameObject buttonPrefab { get; set; }
    [field: SerializeField] private GameObject buttonSelectGroupObject { get; set; }
    [field: SerializeField] public GameObject UIsObject{get; set;}
    [field: SerializeField] public GameObject ScriptsObject { get; set; }
    [field: SerializeField] public GameObject scriptButtonObject { get; set; }
    [field: SerializeField] public RectTransform BoardUpObject { get; set; }
    [field: SerializeField] public RectTransform BoardDownObject { get; set; }
    [field: SerializeField] public CharacterImageEffect[] CharacterSprites { get; set; }
    [HideInInspector] public List<Button> selectButtonList = new List<Button>();
    // weapon Text
    [field: Space(20)]
    [field: SerializeField] private TextMeshProUGUI weaponClipText { get; set; }

    [field: SerializeField] public Image clipBar { get; set; }

    [field: Space(20)]

    public Queue<GameObject> speechPool = new Queue<GameObject>();
    public Queue<GameObject> popUpPool = new Queue<GameObject>();
    [field: SerializeField] public GameObject speechBublePrefab { get; set; }
    [field: SerializeField] public GameObject popUpPrefab { get; set; }

    [field: Space(20)]
    [field: SerializeField] private GameObject characterBoard { get; set; }
    private Vector3 characterBoardNormalVector;
    [field: SerializeField] private Image character { get; set; }
    private Vector3 characterNormalVector;
    [field: SerializeField] public Image hpBar { get; set; }
    [field: SerializeField] public Image hpLateBar { get; set; }
    [field: SerializeField] public Image spBar { get; set; }

    [field: Space(20)]
    [field: SerializeField] private TextMeshProUGUI levelText { get; set; }
    [field: SerializeField] public Image expBar { get; set; }

    private Player player { get; set; }
    private void Start()
    {
        
        WeaponUpdateUI(Player.instance.weaponData.curClip, Player.instance.weaponData.maxClip);
        barUI(expBar, Player.instance.playerData.exp, Player.instance.playerData.maxExp);

        barUI(hpBar, Player.instance.playerData.hp, Player.instance.playerData.maxHp);
        barUI(hpLateBar, Player.instance.playerData.hp, Player.instance.playerData.maxHp);

        characterBoardNormalVector = characterBoard.GetComponent<RectTransform>().transform.position;
        characterNormalVector = character.GetComponent<RectTransform>().transform.position;

        hpBar.DOColor(new Color32(155, 225, 100, 255), 0.4f);
        player = Player.instance;
    }

    public GameObject speechUI()
    {
        GameObject speechObject = Pooling.instance.getSpeechObject(ref speechPool, Player.instance.gameObject, speechBublePrefab);
        speechObject.GetComponent<SpeechBuble>().onSpeech(SpeechBuble.SpeechTypes.Reload, Player.instance.gameObject);

        return speechObject;
    }

    public GameObject popupUI()
    {
        GameObject popupObject = Pooling.instance.PopUpObject(ref popUpPool,GameManager.instance.popCharacters[0], popUpPrefab);

        return popupObject;
    }


    public void characterHitUI()
    {
        character.sprite = player.playerDataSO.hitStateSprite;

        character.gameObject.transform.DOKill();
        character.gameObject.transform.DOShakePosition(1f,4f);

        characterBoard.transform.DOKill();
        characterBoard.transform.DOShakePosition(1f, 4f);

        character.color = new Color32(255, 190, 190, 255);

        Debug.Log("1");
        barUI(hpBar, Player.instance.playerData.hp, Player.instance.playerData.maxHp);

        hpBar.DOKill();
        hpBar.DOColor(new Color32(255, 90, 30, 255), 0.3f).
           SetEase(Ease.OutQuint).
            OnComplete(() => doLateAnimationUpdate());
    }

    private void doLateAnimationUpdate()
    {
        Debug.Log("2");

        hpLateBarUI();
        characterNormalUI();

        characterBoard.GetComponent<RectTransform>().transform.position = characterBoardNormalVector;
        character.GetComponent<RectTransform>().transform.position = characterNormalVector;
    }

    private void hpLateBarUI()
    {
        barUI(hpLateBar, Player.instance.playerData.hp, Player.instance.playerData.maxHp);

        hpBar.DOKill();
        hpBar.DOColor(new Color32(155,255,100,255 ),0.4f);
    }

    private void characterNormalUI()
    {
        character.color = new Color32(255, 255, 255, 255);
        character.sprite = player.playerDataSO.normalStateSprite;
    }

    public void WeaponUpdateUI(int curClip, int maxClip)
    {
        wepaonTextUI(curClip, maxClip);

        // 퍼포먼스 수정 필수 ( / 안쓰기 )
        barUI(clipBar, curClip, maxClip);
    }

    public void barUI(Image image, float cur, float max)
    {
        float tempAmount = cur / max;

        image.DOKill();
        image.DOFillAmount(tempAmount, 0.5f);
    }

    public void levelTextUI(float level)
    {
        levelText.text = string.Format($"Lv . {level}");
    }

    #region scriptTextOnly
    public void setScriptText(string characterName, string schoolClubName, string text)
    {
        characterNameText.text = characterName;
        schoolClubText.text = schoolClubName;

        message = text;
        TypeEffect();
    }

    private void TypeEffect()
    {
        scriptText.text = "";
        index = 0;
        Invoke("Effecting", 1 * (CPS * 0.01f));
    }

    private void Effecting()
    {
        if(scriptText.text == message || message == null) return;

        scriptText.text += message[index];
        index += 1;

        Invoke("Effecting", 1 * (CPS * 0.01f));
    }

    public void InitButtonSelection(int num, List<Node> children, Container container)
    {
        // 버튼 생성전에 리스트를 한번 초기화 시킨다.
        clearButtons();

        // 해당 노드의 자식의 수만큼 반복한다.
        for(int i=0; i<num; i++)
        {
            // 버튼 생성 ( 풀링으로 만들기)
            var button = Instantiate(buttonPrefab);

            // 버튼에 해당 자식번호 저장
            button.AddComponent<selectionButtonNumber>();
            var selectNumber = button.GetComponent<selectionButtonNumber>().selectNumber;

            // 버튼에 이름 지정
            button.name = children[i].description;

            // 버튼 내부에 있는 text 변경;
            var text = button.GetComponentInChildren<TextMeshProUGUI>();
            text.text = children[i].description;

            // 버튼의 부모 설정
            button.transform.SetParent(buttonSelectGroupObject.transform);

            // 버튼에 클릭 이벤트 추가
            selectNumber = i;
            button.GetComponent<Button>().onClick.AddListener(() => 
            {
                clickSelectBtn(selectNumber, container);
                clearButtons();
            });

            selectButtonList.Add(button.GetComponent<Button>());
        }
    }

    public void clickSelectBtn(int i, Container container)
    {
        container.buttonSelectNumber = i;
    }

    public void clearButtons()
    {
        if(selectButtonList != null)
        {
            for(int i=0; i< selectButtonList.Count; i++)
            {
                Destroy(selectButtonList[i].gameObject);
            }

            selectButtonList.Clear();
        }
    }
    #endregion;

    Tweener rectTween { get; set; }
    private void wepaonTextUI(float cur, float max)
    {
        weaponClipText.text = string.Format($"{cur} / {max}");

        var rect = weaponClipText.gameObject.GetComponent<RectTransform>();

        rectTween = rect.DOScale(new Vector3(1, 1, 1), 0f).SetAutoKill(false);

        rectTween.ChangeEndValue(new Vector3(1.1f, 1.1f, 1.1f), 0f, false).Restart();
        rectTween.ChangeEndValue(new Vector3(1,1,1),0.1f,true).SetEase(Ease.InOutCubic).Restart();
    }
}
