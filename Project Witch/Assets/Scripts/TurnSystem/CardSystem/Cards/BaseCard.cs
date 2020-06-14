using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCard : Card
{
    override public bool Use(GameObject target, int depth)
    {
        if (!CheckRange(depth))
            return false;

        Debug.Log("BaseCard : " + target.name);

        Enemy enemy = target.GetComponent<Enemy>();

        enemy.Hited(10);

        enemy.DamagedHate(transform.parent.gameObject, 10);
        HateSystem.instance.HateUpdate();

        UIUpdate();

        return true;
    }
}
