using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptSequenceNode : CompositeNode
{

    public int selectNumber = -1;
    protected override void OnStart()
    {
        container.scriptSequence = this;

        // �����̳ʿ� �ִ� ��ư���ð��� -1�� �ʱ�ȭ
        container.buttonSelectNumber = -1;

        // �ڽ� ��ư�鿡�� ������� �ּҰ� ����
        for(int i= 0; i< children.Count; i++) children[i].selectNode = i;

        // ��ư ����
       UIManager.instance.InitButtonSelection(children.Count, children,container);
    }

    protected override void OnStop()
    {
   
    }

    protected override State OnUpdate()
    {
        if (alreadySelected)
        {
            children[selectNumber].Update();
        }
        else
        {
            // ��ư���ð��� ������ ���� �ȵǰ� ����
            if (container.buttonSelectNumber != -1 && !alreadySelected)
            {
                //��ư���ð��� -1�� �ƴ϶��
                for (int i = 0; i < children.Count; i++)
                {
                    // �ش� ��ư �ּҰ� �̶� �����̳ʿ� �ִ� ��ư���ð��� ������
                    if (children[i].selectNode == container.buttonSelectNumber)
                    {
                        selectNumber = container.buttonSelectNumber;
                        alreadySelected = true; 
                        // �ش� ��带 ������Ʈ �Ѵ�
                        children[i].Update();
                    }
                }
            }
        }

        return State.Running;
    }
}
