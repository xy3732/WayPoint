using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Experimental.GraphView;
#endif

// Node ���� Ŭ�� �ϸ� ���� ��� ���� ���õǰ� ����
public class DoubleClickSelection : MouseManipulator    
{
    double time;
    double doubleClickDuration = 0.3f;

    public DoubleClickSelection()
    {
        time = EditorApplication.timeSinceStartup;
    }

    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<MouseDownEvent>(OnMouseDown);
    }

    protected override void UnregisterCallbacksFromTarget()
    {

        target.UnregisterCallback<MouseDownEvent>(OnMouseDown);
    }


    // ���콺 Ŭ����
    private void OnMouseDown(MouseDownEvent evt)
    {
        var graphview = target as BehaviourTreeView;

        // �׷����䰡 ������ ��ȯ
        if(graphview == null) return;

        double duration = EditorApplication.timeSinceStartup - time;
        // ����Ŭ�� ���� �ð����� ª���� ����Ŭ�� �ڵ� ���� 
        if(duration < doubleClickDuration)
        {
            SelectChildren(evt);
        }

        time = EditorApplication.timeSinceStartup;
    }

    // ����� ���� ������ ����
    void SelectChildren(MouseDownEvent evt)
    {
        var graphView = target as BehaviourTreeView;

        // �׷����䰡 ������ ��ȯ
        if (graphView == null) return;

        if (!CanStopManipulation(evt)) return;

        NodeView clickedElement = evt.target as NodeView;

        if(clickedElement == null)
        {
            var ve = evt.target as VisualElement;
            clickedElement = ve.GetFirstAncestorOfType<NodeView>();

            if (clickedElement == null) return;
        }

        // ���� ��� ���� �����ϴ� �޼ҵ�
        BehaviourTree.Traverse(clickedElement.node, (n) => 
        {
            var view = graphView.FindNodeView(n);
            graphView.AddToSelection(view);
        });
    }
}
