using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected List<State> states = new List<State>();
    [SerializeField] protected State currentState;
    [SerializeField] protected State previousState;
    protected int maxHp;
    [SerializeField] protected int hp;


    // Start is called before the first frame update
    void Start()
    {
        maxHp = 100;
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (CardSystem.instance.ReadyFlag)
            CardUse();
    }

    // 상태에 따라 행동함
    virtual public void Action()
    {
        currentState.Execute();
    }

    // 상태 변화 조건에 따라 상태가 변화함
    virtual public void ChangeState()
    {

    }

    // CardSystem이 준비 했던 카드를 사용하게 한다.
    public void CardUse()
    {
        CardSystem.instance.Use(gameObject);
    }

    // num만큼 체력을 잃는다.
    virtual public void Hited(int num)
    {
        hp -= num;
        ChangeState();

        if (hp <= 0)
            Die();
    }

    // 죽는다.
    virtual public void Die()
    {
        Debug.Log(gameObject.name + " : Died");
        Destroy(gameObject);
    }
}
