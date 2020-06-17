using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionSystem : MonoBehaviour
{
    /*
     * instance : Singleton 패턴 사용
     * enemyConditions : Enemy에 걸린 모든 상태이상을 모아놓는다.
     * undeadConditions : Undead에 걸린 모든 상태이상을 모아놓는다.
     */

    public static ConditionSystem instance;
    [SerializeField] List<Condition> enemyConditions = new List<Condition>();
    [SerializeField] List<Condition> undeadConditions = new List<Condition>();

    private void Awake()
    {
        instance = this;
    }

    // 대상에게 상태이상을 추가한다.
    public void AddTarget(Condition condition, GameObject target)
    {
        Unit temp = target.GetComponent<Unit>();
        if (temp as Undead != null)
        {
            undeadConditions.Add(condition);
            condition.Enter(target);
        }
        else if (temp as Enemy != null)
        {
            enemyConditions.Add(condition);
            condition.Enter(target);
        }

    }

    // 대상에게 있는 상태이상을 제거한다.
    public void Remove(Condition condition, GameObject target)
    {
        Unit temp = target.GetComponent<Unit>();
        if (temp as Undead != null)
        {
            condition.Exit();
            undeadConditions.Remove(condition);
        }
        else if (temp as Enemy != null)
        {
            condition.Exit();
            enemyConditions.Remove(condition);
        }
    }

    // 플레이어 턴이 시작할 때 사용
    public void PlayerTurnStart()
    {
        foreach (Condition condition in undeadConditions)
        {
            condition.TurnStart();
        }
    }

    // 플레이어 턴이 끝날 때 사용
    public void PlayerTurnEnd()
    {
        Reset:
        foreach(Condition condition in undeadConditions)
        {
            if (!condition.TurnEnd())
                goto Reset;
        }
    }

    // 적 턴이 시작할 때 사용
    public void EnemyTurnStart()
    {
        foreach (Condition condition in enemyConditions)
        {
            condition.TurnStart();
        }
    }

    // 적 턴이 끝날 때 사용
    public void EnemyTurnEnd()
    {
        Reset:
        foreach (Condition condition in enemyConditions)
        {
            if (!condition.TurnEnd())
                goto Reset;
        }
    }

    public List<Condition> SearchCondition(System.Type target)
    {
        List<Condition> returnConditions = new List<Condition>();
        ConditionDivide divide = ConditionDivide.Condition;

        if (target == typeof(Condition))
            divide = ConditionDivide.Condition;
        if (target == typeof(CountdownCondition))
            divide = ConditionDivide.CountdownCondition;
        if (target == typeof(CountCondition))
            divide = ConditionDivide.CountCondition;
        if (target == typeof(AttackedCondition))
            divide = ConditionDivide.AttackedCondition;
        if (target == typeof(HitedCondition))
            divide = ConditionDivide.HitedCondition;
        
        switch (divide)
        {
            case ConditionDivide.Condition:
                foreach (Condition condition in enemyConditions)
                {
                    if (condition as Condition != null)
                        returnConditions.Add(condition);
                }
                foreach (Condition condition in undeadConditions)
                {
                    if (condition as Condition != null)
                        returnConditions.Add(condition);
                }
                break;
            case ConditionDivide.CountdownCondition:
                foreach (Condition condition in enemyConditions)
                {
                    if (condition as CountdownCondition != null)
                        returnConditions.Add(condition);
                }
                foreach (Condition condition in undeadConditions)
                {
                    if (condition as CountdownCondition != null)
                        returnConditions.Add(condition);
                }
                break;
            case ConditionDivide.CountCondition:
                foreach (Condition condition in enemyConditions)
                {
                    if (condition as CountCondition != null)
                        returnConditions.Add(condition);
                }
                foreach (Condition condition in undeadConditions)
                {
                    if (condition as CountCondition != null)
                        returnConditions.Add(condition);
                }
                break;
            case ConditionDivide.AttackedCondition:
                foreach (Condition condition in enemyConditions)
                {
                    if (condition as AttackedCondition != null)
                        returnConditions.Add(condition);
                }
                foreach (Condition condition in undeadConditions)
                {
                    if (condition as HitedCondition != null)
                        returnConditions.Add(condition);
                }
                break;
            case ConditionDivide.HitedCondition:
                foreach (Condition condition in enemyConditions)
                {
                    if (condition as HitedCondition != null)
                        returnConditions.Add(condition);
                }
                foreach (Condition condition in undeadConditions)
                {
                    if (condition as AttackedCondition != null)
                        returnConditions.Add(condition);
                }
                break;
            default:
                break;
        }   

        return returnConditions;
    }
}
