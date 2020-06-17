using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedCondition : CountdownCondition
{
    protected bool updateCheck;

    public bool UpdateCheck { get { return updateCheck; } }

    public AttackedCondition() : base() { updateCheck = false; }
    public AttackedCondition(int countdown) : base(countdown) { updateCheck = false; }

    virtual public void Attacked(GameObject target)
    {

    }

    public override void Update()
    {
        base.Update();

        if (updateCheck == true)
            return;
    }
}
