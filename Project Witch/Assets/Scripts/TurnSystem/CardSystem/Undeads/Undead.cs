using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Undead : Unit
{
    /*
     * cards : 이 유닛이 가지고 있는 카드들
     * hp : 이 유닛의 체력
     * drawCount : 이 유닛이 드로우 하는 카드의 수
     * name : 이 유닛의 이름
     * position : 이 유닛의 위치, 덱의 allys 의 번호와 같다.
     * unitNum : 이 유닛의 CSV상에서의 번호
     * atk : 공격력
     */

    protected List<GameObject> cards = new List<GameObject>();
    protected int drawCount;
    protected new string name;

    protected int battleDrawCount;

    public List<GameObject> Cards { get { return cards; } }
    public int DrawCount { get { return drawCount; } }
    public int BattleDrawCount { get { return battleDrawCount; } set { battleDrawCount = value; } }


    private void Awake()
    {
        name = DataBase.instance.UnitData[unitNum]["Name"] as string;
        drawCount = (int)DataBase.instance.UnitData[unitNum]["DrawCount"];
        maxHp = (int)DataBase.instance.UnitData[unitNum]["MaxHp"];
        hp = (int)DataBase.instance.UnitData[unitNum]["Hp"];
        atk = (int)DataBase.instance.UnitData[unitNum]["Atk"];

        Load(unitNum);
    }

    void Start()
    {
        
    }

    // num만큼 체력을 잃는다.
    override public void Hited(float num)
    {
        battleHp -= (int)num;

        if (hp <= 0 || battleHp <= 0)
            Die();
    }

    // 죽는다.
    override public void Die()
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
        for (int i = 2; i < transform.childCount; i++)
        {
            transform.GetChild(i).localPosition = new Vector3(0, 0.55f, 1);
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                transform.GetChild(i).Translate(0, 0.7f * j, 0);
                j++;
            }
        }
    }

    override public void BattleReadiness()
    {
        base.BattleReadiness();
        battleDrawCount = drawCount;
    }
}
