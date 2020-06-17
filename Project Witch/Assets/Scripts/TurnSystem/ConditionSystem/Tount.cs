using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tount : HitedCondition
{
    public Tount() : base() { }
    public Tount(int count) : base(count) { }

    override public void Enter(GameObject gameObject)
    {
        base.Enter(gameObject);
        HateSystem.instance.Tounted(target.GetComponent<Unit>().Position);
    }

    override public void Exit()
    {
        HateSystem.instance.TountCancel(target.GetComponent<Unit>().Position);
        base.Exit();
    }

    public override bool Update()
    {
        if (!base.Update())
            return false;

        return DecreaseCount(1);
    }
}
