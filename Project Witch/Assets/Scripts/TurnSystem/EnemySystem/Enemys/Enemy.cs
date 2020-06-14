using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /*
     * states : Enemy가 가지는 여러 상태들
     * currentState : Enemy가 현재 취하고 있는 상태
     * previousState : Enemy가 직전에 취하고 있던 상태
     * hate : 각각의 유닛에 대한 적대도
     * position : Enemy가 있는 위치
     * maxHp : 최대 체력
     * hp : 현재 체력
     * atk : 공격력
     * enemyNum : Enemy가 CSV상에서 가지는 번호
     */
    [SerializeField] protected List<State> states = new List<State>();
    [SerializeField] protected State currentState;
    [SerializeField] protected State previousState;
    [SerializeField] protected Hate hate;
    private int position;
    protected float maxHp;
    [SerializeField] protected float hp;
    protected float atk;
    protected int enemyNum;

    public List<State> States { get { return states; } }
    public Hate Hate { get { return hate; } }
    public int Position { get { return position; } }
    public float MaxHp { get { return maxHp; } }
    public float Hp { get { return hp; } }
    public int EnemyNum { get { return enemyNum; } }
    public float Atk { get { return atk; } }

    private void Awake()
    {
        maxHp = (float)(int)DataBase.instance.EnemyData[enemyNum]["MaxHp"];
        hp = (float)(int)DataBase.instance.EnemyData[enemyNum]["Hp"];
        atk = (float)(int)DataBase.instance.EnemyData[enemyNum]["Atk"];

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
        int depth = (position - 1) / 3 + 1;
        CardSystem.instance.Use(gameObject, depth);
    }

    // num만큼 체력을 잃는다.
    public void Hited(int num)
    {
        hp -= num;
        ChangeState();

        if (hp <= 0)
            Die();
    }

    // 죽는다.
    public void Die()
    {
        Debug.Log(gameObject.name + " : Died");
        HPSystem.instance.DieEnemy(this);
        EnemySystem.instance.Die(gameObject);
    }

    // 외부에서 포지션을 설정해줘야 함
    public void SetPosition(int num)
    {
        position = num;
    }

    // 대상을 찾는다. State에 따라서 대상 검색 조건이 바뀐다.
    // State가 알아서 상태를 바꾸는 것으로 변경 하면서 같이 변경, 이제 State가 대상을 탐색함
    //virtual public Unit SearchTarget()
    //{
    //    return hate.AllRandom();
    //}

    // 공격을 받으면 증가
    virtual public void DamagedHate(GameObject ally, float hitDamage)
    {
        hate.DamagedHate(ally, hitDamage);
    }

    // 체력 퍼센트를 반환한다.
    public float GetHpRate()
    {
        return (float)hp / (float)maxHp;
    }

    // 외부에서 설정해줘야 함.
    public void SetEnemyNum(int num)
    {
        enemyNum = num;
    }
}
