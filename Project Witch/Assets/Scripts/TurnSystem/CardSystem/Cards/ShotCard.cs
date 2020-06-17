using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotCard : Card
{
    override public bool Use(GameObject target)
    {
        if (!target.GetComponent<Enemy>())
            return false;

        int depth = (target.GetComponent<Enemy>().Position - 1) / 3 + 1;
        float atk = transform.parent.GetComponent<Undead>().BattleAtk;

        if (!CheckRange(depth))
            return false;
        if (seal)
            return false;

        Debug.Log("ShotCard : " + target.name);

        // 각 카드 기믹 작성
        Enemy enemy = target.GetComponent<Enemy>();

        enemy.Hited(transform.parent.gameObject, (int)atk);
        //

        UIUpdate();

        return true;
    }
}
