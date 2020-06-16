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

    [SerializeField] protected float battleHp;
    protected float battleMaxHp;
    protected float battleAtk;

    protected int position;
    protected int unitNum;
    [SerializeField] protected List<Condition> addedConditions = new List<Condition>();
    protected List<GameObject> conditionIcons = new List<GameObject>();

    public float MaxHp { get { return maxHp; } }
    public float Hp { get { return hp; } }
    public float Atk { get { return atk; } }

    public float BattleHp { get { return battleHp; } }
    public float BattleMaxHp { get { return battleMaxHp; } }
    public float BattleAtk { get { return battleAtk; } }
    virtual public float HpRate { get { return battleHp / battleMaxHp; } }

    public int Position { get { return position; } }
    public int UnitNum { get { return unitNum; } }
    

    virtual public void Hited(float damage)
    {

    }

    virtual public void Die()
    {

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
                addedCondition.Overlap(condition.Countdown);
                return;
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

        Destroy(conditionIcons[i]);
        conditionIcons.RemoveAt(i);
    }

    virtual public void BattleReadiness()
    {
        battleMaxHp = maxHp;
        battleHp = hp;
        battleAtk = atk;
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
            icon.transform.Translate(i, 0, 0);
            i += 2;
        }
    }
}
