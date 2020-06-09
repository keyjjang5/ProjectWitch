using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AngryState : State
{
    public static State instance;

    public AngryState() : base()
    {
        instance = this;
    }

    override public void Enter()
    {
        Debug.Log("Enter : AngryState");
    }

    override public void Exit()
    {
        Debug.Log("Exit : AngryState");
    }

    override public void Execute(Unit target)
    {
        Debug.Log("15 Damage");
    }

    override public string GetName()
    {
        return "BaseState 작동중";
    }
}
