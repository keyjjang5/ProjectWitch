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
        if (minRange > depth || depth > maxRange)
        {
            Debug.Log("당신은 사정거리가 맞지 않습니다.");
            return false;
        }

        Debug.Log("TestCard : " + target.name);
        target.GetComponent<Enemy>().Hited(20);

        return true;
    }
}
