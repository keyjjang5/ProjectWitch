using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BerserkState : State
{
    public static State instance;

    public BerserkState() : base()
    {
        instance = this;
    }

    override public void Enter()
    {
        Debug.Log("Enter : BerserkState");
    }

    override public void Exit()
    {
        Debug.Log("Exit : BerserkState");
    }

    override public void Execute(Enemy enemy)
    {
        Debug.Log("20 Damage");
    }

    override public string GetName()
    {
        return "BaseState 작동중";
    }
}
