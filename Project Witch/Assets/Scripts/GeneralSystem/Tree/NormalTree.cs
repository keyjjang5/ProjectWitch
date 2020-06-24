using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
// T는 Node를 기반으로 한다.
public class NormalTree
{
    /* node[depth][num] 의 형태로 사용한다.
     * depth : 이 트리에서 노드의 깊이(층)
     * num : depth에 해당하는 깊이의 num번 째 것
     * 
     * root : 이 트리의 뿌리
     * nodes : 이 트리가 가지고 있는 모든 노드       
     */
    readonly Node root;

    [SerializeField]
    protected  List<Dictionary<int, Node>> nodes = new List<Dictionary<int, Node>>();

    public List<Dictionary<int, Node>> Nodes { get { return nodes; } }
    public Node Root { get { return root; } }

    public NormalTree()
    {
        root = new Node();
        Nodes.Add(new Dictionary<int, Node>());
        Nodes[0].Add(0, root);
    }

    // nDepth의 랜덤한 노드 하나를 찾아서 반환한다.
    public Node GetNDepthRandomNode(int depth)
    {
        if (Nodes.Count <= depth)
        {
            Debug.Log("GetNDepthRandomNode Error");
            return null;
        }

        int rand = Random.Range(0, Nodes[depth].Count);
        Node t = Nodes[depth][rand];

        return t;
    }

    // depth의 num번째 노드를 찾아서 반환한다.
    public Node GetNode(int depth, int num)
    {
        if (Nodes.Count <= depth)
        {
            Debug.Log("GetNode Error");
            return null;
        }
        if (Nodes[depth].Count <= num)
        {
            Debug.Log("GetNode Error");
            return null;
        }

        Node t = Nodes[depth][num];

        return t;
    }

    // 매개변수로 받은 노드의 depth를 반환한다. 찾을 수 없으면 -1을 반환한다.
    public int SearchDepth(Node node)
    {
        for (int i = 0; i < Nodes.Count; i++)
        {
            for (int j = 0; j < Nodes[i].Count; j++)
            {
                if (Nodes[i][j] == node)
                    return i;
            }
        }

        return -1;
    }

    // 매개변수로 받은 노드가 몇번째 노드인지 반환한다. 찾을 수 없으면 -1을 반환한다.
    public int SearchNumber(Node node)
    {
        for (int i = 0; i < Nodes.Count; i++)
        {
            for (int j = 0; j < Nodes[i].Count; j++)
            {
                if (Nodes[i][j] == node)
                    return j;
            }
        }
        
        return -1;
    }

    // 깊이 우선 탐색, 너비 우선 탐색 등을 할 수 있음
    // Breadth-First Search
    public Node BFS(Node node)
    {
        Node t = default;
        return t;
    }
    // Depth-First Search
    public Node DFS(Node node)
    {
        Node t = default;
        return t;
    }

    // 노드를 만들고 연결한다.
    public void AddNode(Node parent, Node child)
    {
        int i = SearchDepth(parent);
        if (Nodes.Count < i + 1)
            Nodes.Add(new Dictionary<int, Node>());

        Nodes[i + 1].Add(Nodes[i + 1].Count, child);
        parent.AddChild(child);
    }

    // 해당하는 depth에 노드를 만들기만 한다.
    public void CreateNode(int depth, Node child)
    {
        if (depth >= Nodes.Count)
            Nodes.Add(new Dictionary<int, Node>());

        int i = Nodes[depth].Count;
        Nodes[depth].Add(i, child);
    }

    // 어떤 노드에 다른 노드를 추가 한다.
    public void ConnectNode(Node parent, Node child)
    {
        parent.AddChild(child);
        child.AddParent(parent);
    }

    // 노드를 지울 수 있음. 이 때 트리구조가 붕괴 되면 안됨
    public void Remove(Node node)
    {

    }
}
