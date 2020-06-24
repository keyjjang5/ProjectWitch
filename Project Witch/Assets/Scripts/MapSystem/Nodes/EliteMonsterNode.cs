using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteMonsterNode : Node
{
    // 이 노드가 루트 일 때만 사용
    public EliteMonsterNode() : base() { }

    // 루트가 아닌 노드를 생성할 때 사용
    public EliteMonsterNode(Node root)
    {
        string name = "EliteMonster";
        Debug.Log(name + " Create");
        temp = GameObject.Instantiate(Resources.Load("Temp/" + name + "Node") as GameObject);

        this.root = root;
    }
}
