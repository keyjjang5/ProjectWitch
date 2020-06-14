using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveSpiderEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        states.Add(CaveSpiderBaseState.instance);
        states.Add(CaveSpiderHaveHouseState.instance);

        currentState = states[0];
    }
}
