using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using DG.Tweening;
public class abilitySelector : Singleton<abilitySelector>
{
    [field: SerializeField] public GameObject blackAlphaObject;
    [field: SerializeField] public GameObject abilitySelectorButton;

    // ���� �۵������� Ȯ��
    private bool isActive = false;

    [SerializeField] private List<AbilitySO> abilitys = new List<AbilitySO>();
    // ��ư ����Ʈ
    [HideInInspector] private List<GameObject> buttonList = new List<GameObject>();
    // Ǯ�� �̽�Ʈ
    [HideInInspector] public Queue<GameObject> pool = new Queue<GameObject>();

    Tweener blackAlpha;

    // ��ư ����
    public void createButton()
    {
        // ���� ability ���� ��ư�� �ִٸ� ����
        if (isActive) return;

        // Ÿ�� �����Ϸ� ������Ʈ ���߱�
        Time.timeScale = 0;
        isActive = true;

        selectAbilitys();

        // �ش� ��ư ������Ʈ�� �ڿ� �ִ� ������ ������Ʈ
        blackAlpha = blackAlphaObject.GetComponent<Image>().DOColor(new Color32(0, 0, 0, 150), 0.5f).SetEase(Ease.OutQuint).SetUpdate(true).SetAutoKill(false);
        for (int i = 0; i < checkAbilityAmount(); i++)
        {
            // Ǯ������ ������Ʈ ����
            var buttonObject = Pooling.instance.abilityGetObject(ref pool, gameObject, abilitySelectorButton);

            // Ʈ���� ���ϸ��̼��� ���� ����� 0���� ����
            buttonObject.GetComponent<RectTransform>().DOScale(new Vector3(0, 0, 0), 0).SetUpdate(true).SetAutoKill(true);

            // �ش� ������Ʈ���� ablitiy�� ����
            var select = buttonObject.GetComponent<abilitySelectorButton>();

            // �ش� ������Ʈ ��ư�� ability �� ����
            select.setAbility(abilitys[i]);

            // ������ ��ư ����Ʈ ����
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

    // ��ư ���ϸ��̼�
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
        // ���� ability ���� ��ư�� �ȶ�ٸ� ����
        if (!isActive) return;


        // Ÿ�� ������ Ǯ��
        Time.timeScale = 1;
        isActive = false;

        blackAlpha.ChangeEndValue(new Color32(0, 0, 0, 10), 0.5f, true).SetEase(Ease.OutQuad).Restart();
        // �ش� ��ư ������Ʈ�� �ڿ� �ִ� ������ ������Ʈ
        //blackAlphaObject.GetComponent<Image>().DOColor(new Color32(0, 0, 0, 10), 0.5f).SetEase(Ease.OutQuad).SetUpdate(true).SetAutoKill(true);

        // ����Ʈ�� �ִ� ��ư ������Ʈ�� Ǯ������ �����
        foreach (var item in buttonList)
        {
            Pooling.instance.setObject(ref pool, item);
        }

        // ����Ʈ �ʱ�ȭ
        buttonList.Clear();
        abilitys.Clear();
    }
}
