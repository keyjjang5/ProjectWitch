using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    /*
     * cards : 이 유닛이 가지고 있는 카드들
     * hp : 이 유닛의 체력
     * drawCount : 이 유닛이 드로우 하는 카드의 수
     * name : 이 유닛의 이름
     * position : 이 유닛의 위치, 덱의 allys 의 번호와 같다.
     * unitNUm : 이 유닛의 CSV상에서의 번호
     */

    protected List<GameObject> cards = new List<GameObject>();
    [SerializeField] protected float hp;
    protected float maxHp;
    protected int drawCount;
    protected new string name;
    private int position;
    protected int unitNum;

    public List<GameObject> Cards { get { return cards; } }
    public float MaxHp { get { return maxHp; } }
    public float Hp { get { return hp; } }
    public int DrawCount { get { return drawCount; } }
    public int Position { get { return position; } }
    public int UnitNum { get { return unitNum; } }

    private void Awake()
    {
        name = DataBase.instance.UnitData[unitNum]["Name"] as string;
        drawCount = (int)DataBase.instance.UnitData[unitNum]["DrawCount"];
        maxHp = (int)DataBase.instance.UnitData[unitNum]["MaxHp"];
        hp = (int)DataBase.instance.UnitData[unitNum]["Hp"];
        Load(unitNum);
    }

    void Start()
    {
        
    }

    // num만큼 체력을 잃는다.
    public void Hited(float num)
    {
        hp -= (int)num;

        if (hp <= 0)
            Die();
    }

    // 죽는다.
    virtual public void Die()
    {
        Debug.Log(name + " : Died");
        HPSystem.instance.DieUnit(this);
        Deck.instance.DieUnit(gameObject);
    }

    // 유닛이 가지고 있는 카드들을 불러온다.
    public void Load(int num)
    {
        for (int i = 1; i < 6; i++)
        {
            int j = (int)DataBase.instance.UnitData[num]["Card" + i];

            if (0 == j)
                continue;

            GameObject temp = Resources.Load(DataBase.instance.CardData[j]["Path"] as string) as GameObject;
            temp.GetComponent<Card>().SetCardNum(j);
            temp.SetActive(false);

            cards.Add(temp);
        }
    }

    // Unit이 가지고 있는 카드를 정렬한다.
    public void Sort()
    {
        int j = 0;
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).localPosition = new Vector3(0, 0.55f, 1);
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                transform.GetChild(i).Translate(0, 0.7f * j, 0);
                j++;
            }
        }
    }

    // 체력 퍼센트를 반환한다.
    public float GetHpRate()
    {
        return (float)hp / (float)maxHp;
    }

    // 외부에서 포지션을 설정해줘야 함
    public void SetPosition(int num)
    {
        position = num;
    }

    // 외부에서 유닛번호를 설정해줘야 함
    public void SetUnitNum(int num)
    {
        unitNum = num;
    }
}
