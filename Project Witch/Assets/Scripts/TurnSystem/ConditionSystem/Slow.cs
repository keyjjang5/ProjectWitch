using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : Condition
{
    public Slow():base()
    {

    }
    public Slow(int countdown):base(countdown)
    {

    }

    override public void Enter(GameObject gameObject)
    {
        base.Enter(gameObject);
    }

    override public void TurnStart()
    {
        base.TurnStart();
        target.GetComponent<Undead>().BattleDrawCount -= Countdown;
    }

    override public bool TurnEnd()
    {
        target.GetComponent<Undead>().BattleDrawCount = target.GetComponent<Undead>().DrawCount;

        return base.TurnEnd();
    }
}
