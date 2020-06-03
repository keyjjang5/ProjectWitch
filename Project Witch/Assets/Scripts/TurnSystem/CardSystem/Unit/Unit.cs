using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    /*
     * cards : 이 유닛이 가지고 있는 카드들
     * hp : 이 유닛의 체력
     * drawCount : 이 유닛이 드로우 하는 카드의 수
     */

    private List<GameObject> cards = new List<GameObject>();
    private int hp;
    private uint drawCount;
    public uint DrawCound { get { return drawCount; } }
    private string name;

    public Unit()
    {
        name = "Unit";
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
}
