using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountCondition : Condition
{
    [SerializeField] protected int count;
    
    public int Count { get { return count; } }

    public CountCondition() : base()
    {
        count = 1;
    }

    public CountCondition(int count) : base()
    {
        this.count = count;
    }

    virtual public bool DecreaseCount(int num)
    {
        count -= num;

        if (count <= 0)
        {
            ConditionSystem.instance.Remove(this, target);
            return false;
        }

        return true;
    }

    virtual new public bool Update()
    {
        return true;
    }

    override public void Overlap(int num)
    {
        base.Overlap(num);
        count += num;
    }

}
