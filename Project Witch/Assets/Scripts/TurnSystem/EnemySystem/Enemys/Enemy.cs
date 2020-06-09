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
    private int position;
    public int Position { get { return position; } }
    [SerializeField] protected Hate hate;

    private void Awake()
    {
        maxHp = 100;
        hp = maxHp;
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
        Unit target = SearchTarget();
        currentState.Execute(target);
    }

    // 상태 변화 조건에 따라 상태가 변화함
    virtual public void ChangeState()
    {

    }

    // CardSystem이 준비 했던 카드를 사용하게 한다.
    public void CardUse()
    {
        int depth = (position - 1) / 3 + 1;
        CardSystem.instance.Use(gameObject, depth);
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
        EnemySystem.instance.Die(gameObject);
    }

    // 외부에서 포지션을 설정해줘야 함
    virtual public void SetPosition(int num)
    {
        position = num;
    }

    // 대상을 찾는다. State에 따라서 대상 검색 조건이 바뀐다.
    virtual public Unit SearchTarget()
    {
        return hate.AllRandom();
    }

    // 공격을 받으면 증가
    virtual public void DamagedHate(GameObject ally, float hitDamage)
    {
        hate.DamagedHate(ally, hitDamage);
    }
}
