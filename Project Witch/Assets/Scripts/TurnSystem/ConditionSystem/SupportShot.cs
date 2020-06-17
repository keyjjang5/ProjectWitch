using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportShot : AttackedCondition
{
    public SupportShot() : base() {  }
    public SupportShot(int countdown) : base(countdown) {  }

    override public void Attacked(GameObject target)
    {
        updateCheck = true;

        target.GetComponent<Enemy>().Hited(this.target, (int)(this.target.GetComponent<Unit>().BattleAtk * 0.5));

        updateCheck = false;
    }

    override public void Update(GameObject target)
    {
        base.Update();

        Attacked(target);
    }

    
}
