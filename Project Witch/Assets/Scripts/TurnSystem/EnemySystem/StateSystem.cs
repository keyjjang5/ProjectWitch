using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSystem : MonoBehaviour
{
    /*
     * instance : Singleton 패턴 사용
     * states : state들 저장용
     */
    public static StateSystem instance;
    [SerializeField] List<State> states = new List<State>();

    private void Awake()
    {
        instance = this;
        states.Add(new BaseState());
        states.Add(new AngryState());
        states.Add(new BerserkState());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
