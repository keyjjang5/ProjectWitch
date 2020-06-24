using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    // 자식들을 알 수 있음
    List<Node> childs = new List<Node>();
    // 부모를 알 수 있음
    List<Node> parents = new List<Node>();
    // root를 알 수 있음
    protected Node root;

    public int ChildCount { get { return childs.Count; } }
    public int ParentCount { get { return parents.Count; } }

    // 트리를 알 수 있음?

    public GameObject temp;
    
    // 이 노드가 루트 일 때만 사용
    public Node()
    {
        root = this;
    }

    // 루트가 아닌 노드를 생성할 때 사용
    public Node(Node root)
    {
        Debug.Log("Node Create");
        temp = GameObject.Instantiate(Resources.Load("Temp/Node")as GameObject);
        
        this.root = root;
    }

    // 자식들을 탐색 할 수 있음
    public void AddChild(Node child)
    {
        if (childs.IndexOf(child) != -1)
            return;

        childs.Add(child);
        child.AddParent(this);
    }

    public void AddParent(Node parent)
    {
        if (parents.IndexOf(parent) != -1)
            return;

        parents.Add(parent);
        parent.AddChild(this);
    }

    public Node GetChild(int index)
    {
        if (childs.Count <= index)
            return null;

        return childs[index];
    }

    public int GetChildIndex(Node child)
    {
        return childs.IndexOf(child);
    }

    public Node GetParent(int index)
    {
        if (parents.Count <= index)
            return null;

        return parents[index];
    }

    public int GetParentIndex(Node parent)
    {
        return parents.IndexOf(parent);
    }
}
