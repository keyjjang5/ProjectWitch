using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NekoUnit : Unit
{
    private void Awake()
    {
        name = "NekoUnit";
        drawCount = 2;

        maxHp = 120;
        hp = maxHp;
        Load(2);
    }
}
