using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownCondition : Condition
{
    private int countdown;

    public int Countdown { get { return countdown; } }

    public CountdownCondition()
    {
        this.countdown = 1;
    }

    public CountdownCondition(int countdown)
    {
        this.countdown = countdown;
    }

    override public bool TurnEnd()
    {
        if (!base.TurnEnd())
            return false;

        countdown--;

        if (countdown <= 0)
        {
            ConditionSystem.instance.Remove(this, target);
            return false;
        }

        return true;
    }

    public override void Overlap(int num)
    {
        base.Overlap(num);
        countdown += num;
    }
}
