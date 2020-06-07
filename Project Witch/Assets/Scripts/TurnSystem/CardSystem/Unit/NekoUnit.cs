using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NekoUnit : Unit
{
    private void Awake()
    {
        name = "NekoUnit";
        drawCount = 1;

        Load(2);
    }
}
