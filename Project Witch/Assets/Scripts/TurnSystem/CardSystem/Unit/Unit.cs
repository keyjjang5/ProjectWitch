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
     */

    protected List<GameObject> cards = new List<GameObject>();
    public List<GameObject> Cards { get { return cards; } }
    [SerializeField] protected int hp;
    public int Hp { get { return hp; } }
    protected int maxHp;
    public int MaxHp { get { return maxHp; } }
    protected uint drawCount;
    public uint DrawCount { get { return drawCount; } }
    protected new string name;

    private void Awake()
    {
        name = "Unit";
        drawCount = 0;
        maxHp = 100;
        hp = maxHp;
    }

    void Start()
    {
        
    }

    // num만큼 체력을 잃는다.
    virtual public void Hited(int num)
    {
        hp -= num;

        if (hp <= 0)
            Die();
    }

    // 죽는다.
    virtual public void Die()
    {
        Debug.Log(name + " : Died");
    }

    // 유닛이 가지고 있는 카드들을 불러온다.
    virtual public void Load(int num)
    {
        for (int i = 1; i < 6; i++)
        {
            int j = (int)DataBase.instance.UnitData[num]["Card" + i];

            if (0 == j)
                continue;

            GameObject temp = Resources.Load(DataBase.instance.CardData[j]["Path"] as string) as GameObject;
            cards.Add(temp);
        }
    }

    // Unit이 가지고 있는 카드를 정렬한다.
    virtual public void Sort()
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

    public float GetHPRate()
    {
        return (float)hp / (float)maxHp;
    }
}
