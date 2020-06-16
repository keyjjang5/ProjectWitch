using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoarCard : Card
{
    override public bool Use(GameObject target)
    {
        int depth = (target.GetComponent<Enemy>().Position - 1) / 3 + 1;
        float atk = transform.parent.GetComponent<Undead>().BattleAtk;

        if (!CheckRange(depth))
            return false;
        if (seal)
            return false;

        Debug.Log("Card : " + target.name);

        // 각 카드 기믹 작성
        /*
         * 자기 자신에게 버프(도발)을 건다.
         *  > 버프는 상태이상의 일종, 상태이상을 만들어야함
         * 적은 도발이 걸린 적을 우선 공격한다
         *  > 모든 적 탐색방법에 최우선으로 설정
         */
        //

        UIUpdate();

        return true;
    }
}
