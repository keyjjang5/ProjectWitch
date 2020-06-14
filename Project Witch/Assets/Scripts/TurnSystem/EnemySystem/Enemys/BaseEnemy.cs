using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        states.Add(BaseState.instance);
        states.Add(AngryState.instance);
        states.Add(BerserkState.instance);

        currentState = states[0];
        currentState.Enter();
    }

    // 상태 변화 조건에 따라 상태가 변화함
    override public void ChangeState()
    {
        if (hp < maxHp / 2 && currentState == states[0])
        {
            previousState = currentState;
            previousState.Exit();

            currentState = states[1];
            currentState.Enter();
        }

        if (hp < maxHp / 4 && currentState == states[1])
        {
            previousState = currentState;
            previousState.Exit();

            currentState = states[2];
            currentState.Enter();
        }
    }
}
