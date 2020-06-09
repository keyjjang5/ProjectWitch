﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseState : State
{
    public static State instance;

    public BaseState() : base()
    {
        instance = this;
    }

    override public void Enter()
    {
        Debug.Log("Enter : BaseState");
    }

    override public void Exit()
    {
        Debug.Log("Exit : BaseState");
    }

    override public void Execute(Unit target)
    {
        Debug.Log("Target Name : " + target.name);
        target.Hited(10);
    }

    override public string GetName()
    {
        return "BaseState 작동중";
    }
}
