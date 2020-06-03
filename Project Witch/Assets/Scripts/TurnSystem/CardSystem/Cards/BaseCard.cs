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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public void Use(GameObject target)
    {
        Debug.Log("BaseCard : " + target.name);
        target.GetComponent<Enemy>().Hited(10);
    }
}
