using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ready : CountCondition
{
    public Ready() : base() { }
    public Ready(int count) : base(count) { }

    public override void Enter(GameObject gameObject)
    {
        Debug.Log("Enter Ready");
        base.Enter(gameObject);
        target.GetComponent<Undead>().ChangeCost(-1);
    }

    public override void Exit()
    {
        Debug.Log("Exit Ready");

        target.GetComponent<Undead>().ChangeCost(1);
        base.Exit();
    }
}
