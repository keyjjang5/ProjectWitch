using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveSpiderBaseState : State
{
    public static State instance;

    public CaveSpiderBaseState() : base()
    {
        instance = this;
    }

    override public void Execute(Enemy enemy)
    {
        if (enemy.HpRate >= 0.95f)
        {
            BuildHouse(enemy);
            return;
        }

        Attack(enemy.Hate.AllRandom(), enemy.Atk);
    }

    private void Attack(Undead target, float atk)
    {
        Debug.Log("Attack");
        target.Hited(atk);
    }

    // 거미집을 짓는다. 그에 따른 State로 전환한다.
    private void BuildHouse(Enemy enemy)
    {
        Debug.Log("BuildHouse");
        enemy.ChangeState(enemy.States.IndexOf(CaveSpiderHaveHouseState.instance));
    }

    public override string GetName()
    {
        return "CaveSpiderBaseState";
    }
}

public class CaveSpiderHaveHouseState : State
{
    public static State instance;

    public CaveSpiderHaveHouseState() : base()
    {
        instance = this;
    }

    override public void Execute(Enemy enemy)
    {
        SlowAttack(enemy.Hate.ReverseHateRandom(), enemy.Atk);
    }

    private void SlowAttack(Undead target, float atk)
    {
        Debug.Log("SlowAttack");
        // target에게 상태이상 슬로우 1 부여
        target.AddCondition(new Slow());
        // target에게 공격
        target.Hited(atk * 1.5f);
    }

    public override string GetName()
    {
        return "CaveSpiderHaveHouseState";
    }
}