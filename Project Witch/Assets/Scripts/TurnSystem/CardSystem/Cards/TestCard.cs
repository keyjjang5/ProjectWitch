using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCard : Card
{
    override public bool Use(GameObject target)
    {
        int depth = (target.GetComponent<Enemy>().Position - 1) / 3 + 1;
        float atk = transform.parent.GetComponent<Undead>().BattleAtk;

        if (!CheckRange(depth))
            return false;
        if (seal)
            return false;

        Debug.Log("TestCard : " + target.name);

        // 각 카드 기믹 작성
        Enemy enemy = target.GetComponent<Enemy>();

        enemy.Hited(transform.parent.gameObject, 20);
        //

        UIUpdate();

        return true;
    }
}
