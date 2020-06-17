using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Condition
{
    /*
     * target : 이 상태이상을 가지고 있는 대상
     * countdown : 이 상태이상이 남아 있는 시간
     */

    protected GameObject target;
    [SerializeField]


    public Condition() { }

    virtual public void Enter(GameObject gameObject)
    {
        target = gameObject;
        target.GetComponent<Unit>().ConditionUpdate();
    }

    virtual public void Exit()
    {
        target.GetComponent<Unit>().RemoveCondition(this);
        target.GetComponent<Unit>().ConditionUpdate();
    }

    virtual public void TurnStart()
    {
        target.GetComponent<Unit>().ConditionUpdate();
    }

    virtual public bool TurnEnd()
    {
        target.GetComponent<Unit>().ConditionUpdate();

        return true;
    }

    virtual public void Overlap(int num)
    {
        Debug.Log("overlap");
        target.GetComponent<Unit>().ConditionUpdate();
    }

    virtual public void Update()
    {

    }

    virtual public void Update(GameObject gameObject)
    {

    }
}
