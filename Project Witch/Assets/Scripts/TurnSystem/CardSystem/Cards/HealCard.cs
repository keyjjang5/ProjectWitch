using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCard : Card
{
    override public bool Use(GameObject target)
    {
        if (!target.GetComponent<Undead>())
            return false;

        float atk = transform.parent.GetComponent<Undead>().BattleAtk;

        if (seal)
            return false;

        Debug.Log("HealCard : " + target.name);

        // 각 카드 기믹 작성
        Undead undead = target.GetComponent<Undead>();

        undead.Recover((int)(atk * 0.7));
        //

        UIUpdate();

        return true;
    }
}
