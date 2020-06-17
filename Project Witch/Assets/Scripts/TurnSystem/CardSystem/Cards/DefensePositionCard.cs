using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensePositionCard : Card
{
    override public bool Use(GameObject target)
    {
        // 자기 자신한테 사용하는 카드이기 때문에 사정거리가 상관없음
        //if (!CheckRange(depth))
        //return false;
        if (seal)
            return false;

        Debug.Log("RoarCard : " + target.name);

        // 각 카드 기믹 작성
        /*
         * 자기 자신에게 버프(입는 피해 30% 감소)을 건다.
         */
        transform.parent.GetComponent<Unit>().AddCondition(new DefensePosition(3));
        //

        UIUpdate();

        return true;
    }
}
