using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NekoUnit : Unit
{
    private void Awake()
    {
        name = "NekoUnit";
        drawCount = 1;

        Load();
    }

    // 유닛이 가지고 있는 카드들을 불러온다.
    override public void Load()
    {
        cards.Add(Resources.Load("Prefaps/Cards/TestCard") as GameObject);
        cards.Add(Resources.Load("Prefaps/Cards/TestCard") as GameObject);
        cards.Add(Resources.Load("Prefaps/Cards/TestCard") as GameObject);
    }
}
