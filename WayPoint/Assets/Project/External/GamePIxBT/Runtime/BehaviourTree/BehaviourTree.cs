using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu()]
public class BehaviourTree : ScriptableObject
{
    // BehaviourTree�� Ÿ��
    public BehaviourTreeType behaviourTreeType;
    
    [Space()]
    public Node rootNode;
    public Node.State treeState = Node.State.Running;

    public List<Node> nodes = new List<Node>();
    public BTContainer btContainer = new BTContainer();
    public Node.State Update()
    {
        if(rootNode.state == Node.State.Running)
        {
            treeState = rootNode.Update();
        }
        return treeState;
    }
    #region UnityEditor
#if UNITY_EDITOR
    // Node ����
    public Node CreateNode(System.Type type)
    {
        
        // Node ���� ������ 
        Node node = ScriptableObject.CreateInstance(type) as Node;
        node.name = type.Name;
        node.guid = GUID.Generate().ToString();

        //ctrl+z, ctrl+y
        Undo.RecordObject(this, "Behaviour Tree (CreateNode)");
        // ����Ʈ�� ����
        nodes.Add(node);

        if(Application.isPlaying) AssetDatabase.AddObjectToAsset(node, this);

        // ������ ����
        AssetDatabase.AddObjectToAsset(node, this);
        Undo.RegisterCreatedObjectUndo(node, "Behaviour Tree (CreateNode)");
        AssetDatabase.SaveAssets();

        return node;
    }

    // Node ������ List������ ����
    public void DeleteNode(Node node)
    {
        //ctrl+z, ctrl+y
        Undo.RecordObject(this, "Behaviour Tree (DeleteNode)");

        nodes.Remove(node);

        //AssetDatabase.RemoveObjectFromAsset(node);
        Undo.DestroyObjectImmediate(node);
        AssetDatabase.SaveAssets();
    }

    // ��� ���� �θ��ڽ� ��� ���踦 �����
    public void AddChild(Node parent, Node child)
    {
        RootNode rootNode = parent as RootNode;
        if (rootNode)
        {
            Undo.RecordObject(rootNode, "Behaviour Tree (AddChild)");
            rootNode.child = child;
            EditorUtility.SetDirty(rootNode);
        }

        DecoratorNode decorator = parent as DecoratorNode;
        if(decorator)
        {
            Undo.RecordObject(decorator, "Behaviour Tree (AddChild)");
            //if (child.GetType() == typeof(MergeNode)) AddParent(decorator, child);
            decorator.child = child;
            EditorUtility.SetDirty(decorator);
        }

        CompositeNode composite = parent as CompositeNode;
        if (composite)
        {
            Undo.RecordObject(composite, "Behaviour Tree (AddChild)");
            composite.children.Add(child);
            EditorUtility.SetDirty(composite);
        }

        ConfluenceNode confluence = parent as ConfluenceNode;
        if(confluence)
        {
            Undo.RecordObject(confluence, "Behaviour Tree (AddChild)");
            confluence.child = child;
            EditorUtility.SetDirty(confluence);
        }
    }

    // parent ����Ʈ�� parent �߰�
    public void AddParent(Node parent, Node nowNode)
    {
        var confluence = nowNode as ConfluenceNode;
        if(confluence)
        {
            Undo.RecordObject(confluence, "Behaviour Tree (AddParent)");
            confluence.parents.Add(parent);
            EditorUtility.SetDirty(confluence);
        }
    }

    // parent ����Ʈ���� parent ����
    public void RemoveParent(Node parent, Node nowNode)
    {
        var confluence = nowNode as ConfluenceNode;
        if(confluence)
        {
            Undo.RecordObject(confluence, "Behaviour Tree (RemoveParent)");
            confluence.parents.Remove(parent);
            EditorUtility.SetDirty(confluence);
        }
    }

    // ��� ���� ������� �θ��ڽ� ��� ���踦 �����Ѵ�.
    public void RemoveChild(Node parent, Node child)
    {
        RootNode rootNode = parent as RootNode;
        if (rootNode)
        {
            Undo.RecordObject(rootNode, "Behaviour Tree (RemoveChild)");
            rootNode.child = null;
            EditorUtility.SetDirty(rootNode);
        }

        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator)
        {
            Undo.RecordObject(decorator, "Behaviour Tree (RemoveChild)");
            //if (decorator.child.GetType() == typeof(MergeNode)) RemoveParent(parent, child);
            decorator.child = null;
            EditorUtility.SetDirty(decorator);
        }

        ConfluenceNode confluence = parent as ConfluenceNode;
        if (confluence)
        {
            Undo.RecordObject(confluence, "Behaviour Tree (RemoveChild)");
            confluence.child = null;
            EditorUtility.SetDirty(confluence);
        }

        CompositeNode composite = parent as CompositeNode;
        if (composite)
        {
            Undo.RecordObject(composite, "Behaviour Tree (RemoveChild)");
            composite.children.Remove(child);
            EditorUtility.SetDirty(composite);
        }
    }
#endif
    #endregion

    // ��� �� �ڽİ��� ��ȯ
    public static List<Node> GetChildren(Node parent)
    {
        List<Node> children = new List<Node>();

        RootNode rootNode = parent as RootNode;
        if (rootNode && rootNode.child != null)
        {
            children.Add(rootNode.child);
        }

        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator && decorator.child != null)
        {
            children.Add(decorator.child);
        }

        ConfluenceNode confluence = parent as ConfluenceNode;
        if (confluence && confluence.child != null)
        {
            children.Add(confluence.child);
        }

        CompositeNode composite = parent as CompositeNode;
        if (composite)
        {
            return composite.children;
        }

        return children;
    }

    public static void Traverse(Node node, System.Action<Node> visiter)
    {
        if(node)
        {
            visiter.Invoke(node);
            var children = GetChildren(node);
            children.ForEach((n) => Traverse(n, visiter));
        }
    }
    // Clone���� ����
    public BehaviourTree Clone()
    {
        BehaviourTree tree = Instantiate(this);
        tree.rootNode = tree.rootNode.Clone();

        tree.nodes = new List<Node>();
        Traverse(tree.rootNode, (n) => 
        {
            tree.nodes.Add(n);
        });
        return tree;
    }

    public void Bind(Container container)
    {
        Traverse(rootNode, (n) => 
        {
            n.container = container;
            n.btContainer = btContainer;
        });
    }
}
