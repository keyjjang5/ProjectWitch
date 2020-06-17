using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportShotCard : Card
{
    // 사용했을 때 사용되는 효과를 나타낸다.
    override public bool Use(GameObject target)
    {
        // 사용불능이면 나가
        if (seal)
            return false;

        Debug.Log("SupportShotCard : " + target.name);

        // 각 카드 기믹 작성
        /*
         * 자기 자신에게 SupportShot 상태이상 부여
         * 1 드로우
         */
        transform.parent.GetComponent<Unit>().AddCondition(new SupportShot());
        CardSystem.instance.Draw(1);
        //

        UIUpdate();

        return true;
    }
}
