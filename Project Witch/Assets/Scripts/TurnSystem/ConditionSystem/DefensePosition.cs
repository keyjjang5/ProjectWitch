using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensePosition : CountdownCondition
{
    public DefensePosition() : base() { }

    public DefensePosition(int countdown) : base(countdown) { }

    public override void Enter(GameObject gameObject)
    {
        base.Enter(gameObject);
        target.GetComponent<Unit>().ChangeDef(target.GetComponent<Unit>().BattleDef - 0.3f);
    }

    public override void Exit()
    {
        target.GetComponent<Unit>().ChangeDef(target.GetComponent<Unit>().BattleDef + 0.3f);
        base.Exit();
    }
}
