using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyCard : Card
{
    override public bool Use(GameObject target)
    {
        // 자기 자신한테 사용하는 카드이기 때문에 사정거리가 상관없음
        //if (!CheckRange(depth))
        //return false;
        if (seal)
            return false;

        Debug.Log("ReadyCard : " + target.name);

        // 각 카드 기믹 작성
        /*
         * 자기 자신에게 버프(준비: 다음카드 코스트 -1)을 건다.
         */
        transform.parent.GetComponent<Unit>().AddCondition(new Ready(1));
        //

        UIUpdate();

        return true;
    }
}
