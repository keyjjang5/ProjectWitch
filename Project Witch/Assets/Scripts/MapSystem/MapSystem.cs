using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSystem : MonoBehaviour
{
    public static MapSystem instance;
    public Node currentPos;

    [SerializeField] NormalTree nTree = new NormalTree();

    private void Awake()
    {
        instance = this;

        for (int i = 0; i < 2; i++)
        {
            nTree.CreateNode(1, GetRandomNode());
            nTree.Nodes[1][i].temp.transform.Translate(1 * i + 20, -1, 0);
        }
        for (int i = 0; i < 5; i++)
        {
            nTree.CreateNode(2, GetRandomNode());
            nTree.Nodes[2][i].temp.transform.Translate(1 * i + 20, -2, 0);
        }
        for (int i = 0; i < 10; i++)
        {
            nTree.CreateNode(3, GetRandomNode());
            nTree.Nodes[3][i].temp.transform.Translate(1 * i + 20, -3, 0);
        }
        for (int i = 0; i < 20; i++)
        {
            nTree.CreateNode(4, GetRandomNode());
            nTree.Nodes[4][i].temp.transform.Translate(1 * i + 20, -4, 0);
        }

        currentPos = nTree.Root;
        ConnectNextNode(currentPos);
    }

    // 랜덤한 종류의 노드를 생성해서 반환한다.
    public Node GetRandomNode()
    {
        Node node = null;
        NodeName rand = (NodeName)Random.Range((int)NodeName.StartNode, (int)NodeName.EndNode);

        switch (rand)
        {
            case NodeName.StartNode:
                node = new Node(nTree.Root);
                break;
            case NodeName.MonsterNode:
                node = new MonsterNode(nTree.Root);
                break;
            case NodeName.EliteMonstaerNode:
                node = new EliteMonsterNode(nTree.Root);
                break;
            case NodeName.EventNode:
                node = new EventNode(nTree.Root);
                break;
            case NodeName.FogNode:
                node = new FogNode(nTree.Root);
                break;
            case NodeName.ShopNode:
                node = new ShopNode(nTree.Root);
                break;
            case NodeName.CampNode:
                node = new CampNode(nTree.Root);
                break;
            default:
                Debug.Log("RandomNode Error");
                break;
        }

        return node;
    }

    // 다음 층에 있는 노드와 연결한다.
    public void ConnectNextNode(Node currentNode)
    {
        int depth = nTree.SearchDepth(currentNode);
        int number = nTree.SearchNumber(currentNode);

        if (depth < 0)
            return;
        if (number < 0)
            return;

        ReSearch:

        Node nextDepthNode = nTree.GetNDepthRandomNode(depth + 1);
        if (currentNode.GetChildIndex(nextDepthNode) != -1)
            goto ReSearch;
        if(nextDepthNode == null)
        {
            Debug.Log("ConnectNextNode Error");
            return;
        }
        currentNode.AddChild(nextDepthNode);

        // 윗노드 부터 아랫 노드까지
        Vector3 pos = new Vector3(number + 20, -depth, 0);

        depth = nTree.SearchDepth(nextDepthNode);
        number = nTree.SearchNumber(nextDepthNode);

        Vector3 pos2 = new Vector3(number + 20, -depth, 0);

        Debug.DrawLine(pos, pos2, Color.yellow, Mathf.Infinity);
    }

    // 현재 노드의 i번쨰 자식으로 이동한다.
    public void MoveNode(int i)
    {
        Node temp = currentPos.GetChild(i);
        if(temp == null)
        {
            Debug.Log("MoveNode Error");
            return;
        }

        currentPos = temp;
        for (int j = 0; j < 4; j++)
            ConnectNextNode(currentPos);
    }
}
