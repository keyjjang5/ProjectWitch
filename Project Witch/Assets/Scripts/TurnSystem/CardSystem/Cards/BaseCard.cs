using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCard : Card
{
    // Start is called before the first frame update
    void Start()
    {
        name = "Base";
        cost = 1;
        minRange = 1;
        maxRange = 2;
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
        Debug.Log("BaseCard : " + target.name);
        target.GetComponent<Enemy>().Hited(10);

        return true;
    }
}
