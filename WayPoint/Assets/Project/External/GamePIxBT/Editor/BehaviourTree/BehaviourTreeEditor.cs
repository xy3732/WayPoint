using System;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using UnityEditor.Callbacks;
#endif

public class BehaviourTreeEditor : EditorWindow
{
    BehaviourTreeView treeView;
    InspectorView inspectorView;
    IMGUIContainer behaviourTreeContainerView;
    Label treeViewLabel, behaviourTreeContainerLabel;
    ToolbarMenu toolbarMenu;
    ToolbarButton savetoolButton;
    VisualElement overlay;

    // Behaviour tree Container
    SerializedObject treeObject;
    SerializedProperty behaviourProperty;

    [MenuItem("GamePix's BT/Editor")]
    public static void OpenWindow()
    {
        BehaviourTreeEditor wnd = GetWindow<BehaviourTreeEditor>();
        wnd.titleContent = new GUIContent("GamePix's Behaviour Editor");
    }


    // ���� Ŭ������ BehaviourTree ����
    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        if(Selection.activeObject is BehaviourTree)
        {
            OpenWindow();
            return true;
        }

        return false;
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Project/External/GamePIxBT/USS/BehaviourTreeEditor.uxml");
        visualTree.CloneTree(root);

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Project/External/GamePIxBT/USS/BehaviourTreeEditor.uss");
        root.styleSheets.Add(styleSheet);

        // Init ����
        Setter(root);

        // Tree Editor�� Save�� ������ �ش� Ʈ�� ����.
        savetoolButton.clicked += () => SaveTree();

        // behaviour Tree Container ����
        BehaviourTreeContainerSet(root);

        // ���� �޴� ["Asset"]��ư
        ToolBarMenuSet(root);

        treeView.OnNodeSelected = OnNodeSelectionChanged;

        OnSelectionChange();
    }
    private void BehaviourTreeContainerSet(VisualElement root)
    {
        behaviourTreeContainerView.onGUIHandler = () =>
        {
            // Ʈ��������Ʈ �� ������ BehaviourTreeEditorâ�� ���� ���װ� �ɸ���.
            // �ش� ������Ʈ�� ������ ���� �ȵǰ� �ٲ۴�.
            // Ÿ�� ������Ʈ�� ������ ���� ���.
            if (treeObject != null && treeObject.targetObject != null)
            {
                // ���õ� Ʈ���� ������ overlay ����
                overlay.style.visibility = Visibility.Hidden;

                treeObject.Update();
                EditorGUILayout.PropertyField(behaviourProperty);
                treeObject.ApplyModifiedProperties();
            }
        };
    }

    private void Setter(VisualElement root)
    {
        treeView = root.Q<BehaviourTreeView>();
        inspectorView = root.Q<InspectorView>();
        savetoolButton = root.Q<ToolbarButton>("SaveToolbar");
        overlay = root.Q<VisualElement>("Overlay");
        treeViewLabel = root.Q<Label>("treeView-Label");
        behaviourTreeContainerLabel = root.Q<Label>("BehaviourTreeContainer-Label");
        behaviourTreeContainerView = root.Q<IMGUIContainer>();
    }

    private void OnEnable()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }


    private void OnDisable()
    {
        if (Application.isPlaying) return;

        SaveTree();
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
    }

    private void OnPlayModeStateChanged(PlayModeStateChange obj)
    {
        // �÷��� ��� �߿����� Ŭ���� ������Ʈ�� Ʈ���並 Behaviour tree editor�� ���� �ִ�.
        switch (obj)
        {
            case PlayModeStateChange.EnteredEditMode:
                OnSelectionChange();
                break;

            case PlayModeStateChange.ExitingEditMode:
                break;

            case PlayModeStateChange.EnteredPlayMode:
                OnSelectionChange();
                break;

            case PlayModeStateChange.ExitingPlayMode:
                break;
        }
    }

    private void OnSelectionChange()
    {
        EditorApplication.delayCall += () =>
        {
            BehaviourTree tree = Selection.activeObject as BehaviourTree;

            if (!tree)
            {
                // ���� ���õ� ������Ʈ�� ������
                if (Selection.activeObject)
                {
                    // ��ũ��Ʈ�� �����ϸ� Error�� ���. 
                    try
                    {
                        // ���� �������� ���ӿ�����Ʈ���� ������Ʈ�� �����´�
                        BehaviourTreeRunner runner = Selection.activeGameObject.GetComponent<BehaviourTreeRunner>();

                        // ������Ʈ�� �����ϸ�
                        if (runner)
                        {
                            //Ʈ���� �ش� ������Ʈ�� Ʈ���� �ٲ۴�.
                            tree = runner.tree;
                        }
                    }
                    catch
                    {

                    }
                }
            }

            // ����Ƽ���� ������ �������̶��
            if (Application.isPlaying)
            {
                // Behaviour tree editor â�� ����.
                if (tree)
                {
                    treeView.PopulateView(tree);
                }
            }
            else
            {
                if (tree && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
                {
                    // treeView â ����
                    treeView.PopulateView(tree);
                }
            }

            // TreeView �����ִ� ���� �̸��� �ٲ۴�.
            LabelNameChanger(tree);

            // Ʈ���� null�� �ƴҽø� behaviour container�� �� ����
            if (tree != null)
            {
                treeObject = new SerializedObject(tree);
                behaviourProperty = treeObject.FindProperty("btContainer");
            }
        };
    }
    private void ToolBarMenuSet(VisualElement root)
    {
        // ���� �޴� ["Asset"]��ư
        toolbarMenu = root.Q<ToolbarMenu>();
        var behaviourTrees = LoadAssets<BehaviourTree>();
        behaviourTrees.ForEach((t) =>
        {
            toolbarMenu.menu.AppendAction($"BehaviourTreeTypes/{t.behaviourTreeType}/{t.name}", (a) =>
            {
                // ���� Asset�� �ִ°� Ŭ���� activeobject�� ����
                Selection.activeObject = t;
            });
        });
        toolbarMenu.menu.AppendSeparator();
    }

    // TreeView �����ִ� ���� �̸��� �ٲ۴�.
    private void LabelNameChanger(BehaviourTree tree)
    {
        if (tree == null) return;
        // TreeView �����ִ� ���� �̸��� �ٲ۴�.
        treeViewLabel.text = tree.name + " Tree View";
        behaviourTreeContainerLabel.text = tree.name + "'s container";
    }

    private List<T> LoadAssets<T>() where T : UnityEngine.Object
    {
        // Ÿ�� : t: , "t:type" Ư�� type �˻�, ��� ������ t:Object ���� �˻�
        string[] assetIDs = AssetDatabase.FindAssets($"t:{typeof(T).Name}");
        // ã�� ��� ������ �ӽ÷� ���� �ϴ� ����Ʈ
        List<T> assets = new List<T>();

        foreach (var assetID in assetIDs)
        {
            //���� �˻� �� �ε�
            string path = AssetDatabase.GUIDToAssetPath(assetID);
            T asset = AssetDatabase.LoadAssetAtPath<T>(path);

            assets.Add(asset);
        }

        return assets;
    }

    // ������ ��尡 �ٲ�� �ν����ͺ並 ������Ʈ
    void OnNodeSelectionChanged(NodeView node)
    {
        inspectorView.UpdateSelection(node);
    }

    // ���������� ������Ʈ
    private void OnInspectorUpdate()
    {
        treeView?.UpdateNodeStates();
    }

    // ���õ� Ʈ���� �ְ� ����Ƽ�� �÷��� ��尡 �ƴҽ� ���� ����.
    private void SaveTree()
    {
        if (treeObject == null && !Application.isPlaying) return;

        AssetDatabase.SaveAssets();
    }
}