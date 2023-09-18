using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    // ������ ��ư ����Ʈ
    [HideInInspector] public List<Button> selectButtonList = new List<Button>();

    // ��ȭ �ؽ�Ʈ ���
    [field: SerializeField] public TextMeshProUGUI textObject { get; set; }

    // UI ��ȭ ��ư ����â
    [field: Space(20)]
    [field: SerializeField] public GameObject buttonFrefab { get; set; }
    [field: SerializeField] public GameObject buttonSelectGroupObject { get; set; }
    public void Awake()
    {
        instance = this;

        textObject.text = "";
    }

    public void setText(string text)
    {
        textObject.text = text;
    }

    public void InitButtonSelection(int num, List<Node> children, Container container)
    {
        // ��ư �������� ����Ʈ�� �ѹ� �ʱ�ȭ ��Ų��.
        clearButtons();

        // �ش� ����� �ڽ��� ����ŭ �ݺ��Ѵ�.
        for (int i = 0; i < num; i++)
        {
            // ��ư ����
            var button = Instantiate(buttonFrefab);

            // ��ư�� �ش� �ڽĹ�ȣ ����
            button.AddComponent<selectionButtonNumber>();
            var selectNumber = button.GetComponent<selectionButtonNumber>().selectNumber;

            // ��ư �̸� ����
            button.name = children[i].description;

            // ��ư ���ο� �ִ� text ����
            var text = button.GetComponentInChildren<TextMeshProUGUI>();
            text.text = children[i].description;

            // ��ư �θ� ����
            button.transform.SetParent(buttonSelectGroupObject.transform);

            // ��ư Ŭ�� �̺�Ʈ �߰�
            selectNumber = i;
            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                // ��ư �̺�Ʈ
                clickSelectBtn(selectNumber, container);
                clearButtons();
            });

            //����Ʈ�� �߰�
            selectButtonList.Add(button.GetComponent<Button>());
        }
    }

    // ����Ʈ �ʱ�ȭ
    public void clearButtons()
    {
        // ��ư ����Ʈ�� null�� �ƴҽ� ��ư ����Ʈ �ʱ�ȭ �� ����.
        if (selectButtonList != null)
        {
            for (int i = 0; i < selectButtonList.Count; i++)
            {
                Destroy(selectButtonList[i].gameObject);
            }

           selectButtonList.Clear();
        }
    }

    // ��ư �̺�Ʈ
    public void clickSelectBtn(int i, Container container)
    {
        container.buttonSelectNumber = i;
    }

}
