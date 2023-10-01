using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using DG.Tweening;
public class abilitySelector : Singleton<abilitySelector>
{
    [field: SerializeField] public GameObject blackAlphaObject;
    [field: SerializeField] public GameObject abilitySelectorButton;

    // 현재 작동중인지 확인
    private bool isActive = false;

    [SerializeField] private List<AbilitySO> abilitys = new List<AbilitySO>();
    // 버튼 리스트
    [HideInInspector] private List<GameObject> buttonList = new List<GameObject>();
    // 풀링 이스트
    [HideInInspector] public Queue<GameObject> pool = new Queue<GameObject>();

    Tweener blackAlpha;

    // 버튼 생성
    public void createButton()
    {
        // 만약 ability 선택 버튼이 있다면 리턴
        if (isActive) return;

        // 타임 스케일로 오브젝트 멈추기
        Time.timeScale = 0;
        isActive = true;

        selectAbilitys();

        // 해당 버튼 오브젝트들 뒤에 있는 검은색 오브젝트
        blackAlpha = blackAlphaObject.GetComponent<Image>().DOColor(new Color32(0, 0, 0, 150), 0.5f).SetEase(Ease.OutQuint).SetUpdate(true).SetAutoKill(false);
        for (int i = 0; i < checkAbilityAmount(); i++)
        {
            // 풀링으로 오브젝트 생성
            var buttonObject = Pooling.instance.abilityGetObject(ref pool, gameObject, abilitySelectorButton);

            // 트위닝 에니메이션을 위해 사이즈를 0으로 설정
            buttonObject.GetComponent<RectTransform>().DOScale(new Vector3(0, 0, 0), 0).SetUpdate(true).SetAutoKill(true);

            // 해당 오브젝트에서 ablitiy를 추출
            var select = buttonObject.GetComponent<abilitySelectorButton>();

            // 해당 오브젝트 버튼에 ability 값 저장
            select.setAbility(abilitys[i]);

            // 생성된 버튼 리스트 저장
            buttonList.Add(buttonObject);
        }
        doAnimation();
    }

    private void selectAbilitys()
    {
        for(int i=0; i< checkAbilityAmount(); i++)
        {
            int random = Random.Range(0, GameManager.instance.abilitys.Length);
            var select = GameManager.instance.abilitys[random];
            
            if(abilitys.Contains(select)) i--;
            else abilitys.Add(select);
        }
    }

    private int checkAbilityAmount()
    {
        int count;

        if (Player.instance.playerData.abilitySelectAble <= GameManager.instance.abilitys.Length)
        {
            count = Player.instance.playerData.abilitySelectAble;
        }
        else
        {
            count = GameManager.instance.abilitys.Length;
        }

        return count;
    }

    // 버튼 에니메이션
    Tweener tweenAnimation;
    private void doAnimation()
    {
        for (int i = 0; i < buttonList.Count; i++)
        {
            var button = buttonList[i].gameObject;

            tweenAnimation = button.GetComponent<RectTransform>().DOScale(new Vector3(1, 1, 1), 0.075f).SetDelay(i * 0.05f).SetEase(Ease.OutBack).SetUpdate(true);
        }
    }

    public void deleteButton()
    {
        // 만약 ability 선택 버튼이 안뜬다면 리턴
        if (!isActive) return;


        // 타임 스케일 풀기
        Time.timeScale = 1;
        isActive = false;

        blackAlpha.ChangeEndValue(new Color32(0, 0, 0, 10), 0.5f, true).SetEase(Ease.OutQuad).Restart();
        // 해당 버튼 오브젝트들 뒤에 있는 검은색 오브젝트
        //blackAlphaObject.GetComponent<Image>().DOColor(new Color32(0, 0, 0, 10), 0.5f).SetEase(Ease.OutQuad).SetUpdate(true).SetAutoKill(true);

        // 리스트에 있는 버튼 오브젝트를 풀링으로 지우기
        foreach (var item in buttonList)
        {
            Pooling.instance.setObject(ref pool, item);
        }

        // 리스트 초기화
        buttonList.Clear();
        abilitys.Clear();
    }
}
