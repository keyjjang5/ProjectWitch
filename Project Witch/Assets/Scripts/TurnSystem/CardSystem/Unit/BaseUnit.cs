using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : Unit
{
    private void Awake()
    {
        name = "BaseUnit";
        drawCount = 1;

        Load();
    }

    void Start()
    {
        
    }

    // 유닛이 가지고 있는 카드들을 불러온다.
    override public void Load()
    {
        cards.Add(Resources.Load("Prefaps/Cards/BaseCard") as GameObject);
        cards.Add(Resources.Load("Prefaps/Cards/BaseCard") as GameObject);
        cards.Add(Resources.Load("Prefaps/Cards/BaseCard") as GameObject);
    }
}
