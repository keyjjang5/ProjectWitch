using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : Unit
{
    private void Awake()
    {
        name = "BaseUnit";
        drawCount = 1;

        maxHp = 100;
        hp = maxHp;
        Load(1);
    }
}
