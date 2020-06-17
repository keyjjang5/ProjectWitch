using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    /*
     * position : Unit이 있는 위치
     * maxHp : 최대 체력    
     * hp : 현재 체력       
     * atk : 공격력         
     * battleMaxHp : 전투 시에 사용하는 최대 체력
     * battleHp : 전투 시에 사용하는 현재 체력
     * battleAtk : 전투 시에 사용하는 공격력
     * unitNum : Unit이 CSV상에서 가지는 번호
     * addedCondition : 추가된 상태이상
     * conditionIcons : 추가된 상태이상들의 아이콘
     */

    [SerializeField] protected float hp;
    protected float maxHp;
    protected float atk;
    protected float def = 1;

    [SerializeField] protected float battleHp;
    protected float battleMaxHp;
    protected float battleAtk;
    protected float battleDef;

    protected int position;
    protected int unitNum;
    [SerializeField] protected List<Condition> addedConditions = new List<Condition>();
    protected List<GameObject> conditionIcons = new List<GameObject>();

    public float MaxHp { get { return maxHp; } }
    public float Hp { get { return hp; } }
    public float Atk { get { return atk; } }
    public float Def { get { return def; } }

    public float BattleHp { get { return battleHp; } }
    public float BattleMaxHp { get { return battleMaxHp; } }
    public float BattleAtk { get { return battleAtk; } }
    public float BattleDef { get { return battleDef; } }
    virtual public float HpRate { get { return battleHp / battleMaxHp; } }

    public int Position { get { return position; } }
    public int UnitNum { get { return unitNum; } }

    virtual public void Hited(float damage)
    {
        Reset:
        foreach(Condition condition in addedConditions)
        {
            if (condition as HitedCondition != null)
                if (!(condition as HitedCondition).Update())
                    goto Reset;
        }
    }

    virtual public void Recover(float heal)
    {
        
    }

    virtual public void Die()
    {
        //ConditionClear();
    }

    // 외부에서 포지션을 설정해줘야 함
    public void SetPosition(int num)
    {
        position = num;
    }

    // 외부에서 설정해줘야 함.
    public void SetUnitNum(int num)
    {
        unitNum = num;
    }

    virtual public void AddCondition(Condition condition)
    {
        foreach (Condition addedCondition in addedConditions)
        {
            if (addedCondition.GetType() == condition.GetType())
            {
                if (addedCondition as CountdownCondition != null)
                {
                    addedCondition.Overlap((addedCondition as CountdownCondition).Countdown);
                    return;
                }
                if (addedCondition as CountCondition != null)
                {
                    addedCondition.Overlap((addedCondition as CountCondition).Count);;
                    return;
                }
                if (addedCondition as AttackedCondition != null)
                {
                    return;
                }
                if (addedCondition as HitedCondition != null)
                {
                    return;
                }
            }
        }
        GameObject temp = Resources.Load("Sprites/Temp/Condition/" + condition.GetType()) as GameObject;
        GameObject newTemp = Instantiate(temp, transform.Find("AddedCondition"));
        conditionIcons.Add(newTemp);

        addedConditions.Add(condition);
        ConditionSystem.instance.AddTarget(condition, gameObject);
    }

    virtual public void RemoveCondition(Condition condition)
    {
        int i = addedConditions.IndexOf(condition);
        addedConditions.Remove(condition);

        Debug.Log("i : " + i);
        Destroy(conditionIcons[i]);
        conditionIcons.RemoveAt(i);
    }

    virtual public void BattleReadiness()
    {
        battleMaxHp = maxHp;
        battleHp = hp;
        battleAtk = atk;
        battleDef = def;
    }

    virtual public void BattleEnd()
    {
        hp = battleHp;
    }

    virtual public void ConditionUpdate()
    {
        int i = 0;
        foreach (GameObject icon in conditionIcons)
        {
            icon.transform.localPosition = new Vector3(-4, 0, 0);
            icon.transform.localPosition += new Vector3(i, 0, 0);
            i += 2;
        }
    }

    virtual public void ConditionClear()
    {
        //foreach(Condition condition in addedConditions)
        //{
        //    RemoveCondition(condition);
        //}

        for(int i = 0;i<addedConditions.Count;i++)
        {
            Debug.Log("ConditionClear : " + addedConditions[0].GetType());
            RemoveCondition(addedConditions[0]);
        }
    }

    // CardSystem이 준비 했던 카드를 사용하게 한다.
    virtual public void UseCard()
    {
        CardSystem.instance.Use(gameObject);
    }

    virtual public void ChangeDef(float newDef)
    {
        battleDef = newDef;
    }

    public Condition SearchCondition(System.Type target)
    {
        foreach(Condition condition in addedConditions)
        {
            if (condition.GetType() == target)
                return condition;
        }

        return null;
    }
}
