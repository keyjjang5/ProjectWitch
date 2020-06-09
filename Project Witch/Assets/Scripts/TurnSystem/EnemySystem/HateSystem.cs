using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HateSystem : MonoBehaviour
{
    public static HateSystem instance;
    List<Hate> hates = new List<Hate>();

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add(Hate hate)
    {
        hates.Add(hate);
    }

    public void HateUpdate()
    {
        foreach(Hate hate in hates)
        {
            hate.Update();
        }
    }
}
