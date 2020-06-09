using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class State
{
    public State()
    {
        
    }

    virtual public void Enter()
    {

    }

    virtual public void Exit()
    {

    }

    virtual public void Execute(Unit target)
    {

    }

    virtual public string GetName()
    {
        return "name";
    }
}
