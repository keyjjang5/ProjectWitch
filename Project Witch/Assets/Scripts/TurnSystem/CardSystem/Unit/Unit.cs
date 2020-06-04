using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    /*
     * cards : 이 유닛이 가지고 있는 카드들
     * hp : 이 유닛의 체력
     * drawCount : 이 유닛이 드로우 하는 카드의 수
     * name : 이 유닛의 이름
     */

    protected List<GameObject> cards = new List<GameObject>();
    public List<GameObject> Cards { get { return cards; } }
    private int hp;
    protected uint drawCount;
    public uint DrawCound { get { return drawCount; } }
    protected string name;

    public Unit()
    {
        name = "Unit";
        drawCount = 0;
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
    virtual public void Load()
    {

    }
}
