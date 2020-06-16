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
}
