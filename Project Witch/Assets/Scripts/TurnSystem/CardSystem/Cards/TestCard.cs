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
    }

    // Update is called once per frame
    void Update()
    {

    }

    override public void Use(GameObject target)
    {
        Debug.Log("TestCard : " + target.name);
        target.GetComponent<Enemy>().Hited(20);
    }
}
