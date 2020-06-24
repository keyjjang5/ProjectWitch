using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogNode : Node
{
    // 이 노드가 루트 일 때만 사용
    public FogNode() : base() { }

    // 루트가 아닌 노드를 생성할 때 사용
    public FogNode(Node root)
    {
        string name = "Fog";
        Debug.Log(name + " Create");
        temp = GameObject.Instantiate(Resources.Load("Temp/" + name + "Node") as GameObject);

        this.root = root;
    }
}
