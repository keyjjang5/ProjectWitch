using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCard : Card
{
    // Start is called before the first frame update
    void Start()
    {
        name = "Test";
        cost = 2;

        minRange = 2;
        maxRange = 3;
    }

    // Update is called once per frame
    void Update()
    {

    }

    override public bool Use(GameObject target, int depth)
    {
        if (!CheckRange(depth))
            return false;

        Debug.Log("TestCard : " + target.name);

        Enemy enemy = target.GetComponent<Enemy>();

        enemy.Hited(20);

        enemy.DamagedHate(transform.parent.gameObject, 20);
        HateSystem.instance.HateUpdate();

        UIUpdate();

        return true;
    }
}
