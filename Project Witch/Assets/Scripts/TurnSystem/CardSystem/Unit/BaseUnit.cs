using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : Unit
{
    private void Awake()
    {
        name = "BaseUnit";
        drawCount = 1;

        Load(1);
    }
}
