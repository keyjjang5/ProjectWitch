using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    /*
     * states : Enemy가 가지는 여러 상태들
     * currentState : Enemy가 현재 취하고 있는 상태
     * previousState : Enemy가 직전에 취하고 있던 상태
     * hate : 각각의 유닛에 대한 적대도
     */

    [SerializeField] protected List<State> states = new List<State>();
    [SerializeField] protected State currentState;
    [SerializeField] protected State previousState;
    [SerializeField] protected Hate hate;

    public List<State> States { get { return states; } }
    public Hate Hate { get { return hate; } }

    override public float HpRate { get { return hp / maxHp; } }

    private void Awake()
    {
        maxHp = (float)(int)DataBase.instance.EnemyData[unitNum]["MaxHp"];
        hp = (float)(int)DataBase.instance.EnemyData[unitNum]["Hp"];
        atk = (float)(int)DataBase.instance.EnemyData[unitNum]["Atk"];

        hate = new Hate();
    }

    // Start is called before the first frame update
    void Start()
    {

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
        currentState.Execute(this);
    }

    // 상태 변화 조건에 따라 상태가 변화함
    virtual public void ChangeState()
    {

    }

    // 상태 변화 조건에 따라 상태가 변화함
    virtual public void ChangeState(int num)
    {
        previousState = currentState;
        currentState = states[num];

        previousState.Exit();
        currentState.Enter();
    }

    // CardSystem이 준비 했던 카드를 사용하게 한다.
    public void CardUse()
    {
        CardSystem.instance.Use(gameObject);
    }

    // num만큼 체력을 잃는다.
    public void Hited(GameObject unit, int damage)
    {
        hp -= damage;
        ChangeState();
        DamagedHate(unit, damage);
        HateSystem.instance.HateUpdate();

        if (hp <= 0)
            Die();
    }

    // 죽는다.
    override public void Die()
    {
        Debug.Log(gameObject.name + " : Died");
        HPSystem.instance.DieEnemy(this);
        EnemySystem.instance.Die(gameObject);
    }

    

    // 대상을 찾는다. State에 따라서 대상 검색 조건이 바뀐다.
    // State가 알아서 상태를 바꾸는 것으로 변경 하면서 같이 변경, 이제 State가 대상을 탐색함
    //virtual public Unit SearchTarget()
    //{
    //    return hate.AllRandom();
    //}

    // 공격을 받으면 증가
    virtual public void DamagedHate(GameObject unit, float hitDamage)
    {
        hate.DamagedHate(unit, hitDamage);
    }
}
