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
        Debug.Log(GetName() + " : Enter");
    }

    virtual public void Exit()
    {
        Debug.Log(GetName() + " : Exit");
    }

    virtual public void Execute(Enemy enemy)
    {

    }

    virtual public string GetName()
    {
        return "State";
    }
}
