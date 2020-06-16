using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyCard : Card
{
    override public bool Use(GameObject target)
    {
        int depth = (target.GetComponent<Enemy>().Position - 1) / 3 + 1;
        if (!CheckRange(depth))
            return false;

        Debug.Log("ReadyCard : " + target.name);

        UIUpdate();

        return true;
    }
}
