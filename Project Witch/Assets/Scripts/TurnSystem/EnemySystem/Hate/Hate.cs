using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Hate
{
    /*
     * hatePoints : 각 유닛에 해당하는 hatePoint List
     * hateLevels : 각 유닛에 해당하는 hateLevel List
     * damage : 이 적이 가지는 공격비례상수
     * debuff : 이 적이 가지는 디버프비례상수
     * buff :   이 적이 가지는 버프비례상수
     * heal :   이 적이 가지는 회복비례상수
     */

    [SerializeField] List<int> hatePoints = new List<int>();
    [SerializeField] List<HateLevel> hateLevels = new List<HateLevel>();
    protected float damage;
    protected float debuff;
    protected float buff;
    protected float heal;

    public Hate()
    {
        for (int i = 0; i < Deck.instance.Allys.Count; i++)
        {
            hatePoints.Add(0);
            hateLevels.Add(HateLevel.Apathy);
        }
        damage = 1.0f;
        debuff = 1.0f;
        buff = 1.0f;
        heal = 1.0f;
    }

    // HatePoint와 HateLevel Update
    virtual public void Update()
    {
        for (int i = 0; i < hatePoints.Count; i++)
        {
            if (hateLevels[i] == HateLevel.Tount)
                continue;

            Synchronize(i);
        }
    }

    // HatePoint와 HateLevel 동기화
    virtual public void Synchronize(int i)
    {
        if (hatePoints[i] <= 0)
            hateLevels[i] = HateLevel.Apathy;
        if (hatePoints[i] > 0)
            hateLevels[i] = HateLevel.Realization;
        if (hatePoints[i] > 10)
            hateLevels[i] = HateLevel.Vigilance;
        if (hatePoints[i] > 30)
            hateLevels[i] = HateLevel.Danger;
    }

    virtual public Undead TountRandom()
    {
        List<int> tountMembers = new List<int>();
        for (int i = 0; i < hateLevels.Count; i++)
        {
            if (hateLevels[i] == HateLevel.Tount)
                tountMembers.Add(i);
        }

        if (tountMembers.Count <= 0)
            return AllRandom();

        return Deck.instance.GetUnit(Random.Range(0, tountMembers.Count));
    }

    // 가장 HatePoint가 높은 적을 공격
    virtual public Undead FirstPriority()
    {
        foreach(HateLevel level in hateLevels)
            if (level == HateLevel.Tount)
                return TountRandom();

        int biggest = -1;
        int num = -1;
        for (int i = 0; i < hatePoints.Count; i++)
        {
            int hp = hatePoints[i];
            if (biggest < hp)
            {
                biggest = hp;
                num = i;
            }
        }
        
        return Deck.instance.GetUnit(num);
    }

    // Apathy 등급인 적 중 하나를 공격
    virtual public Undead ApathyRandom()
    {
        foreach (HateLevel level in hateLevels)
            if (level == HateLevel.Tount)
                return TountRandom();

        List<int> apathyMembers = new List<int>();
        for (int i = 0; i < hateLevels.Count; i++)
        {
            if (hateLevels[i] == HateLevel.Apathy)
                apathyMembers.Add(i);
        }

        if (apathyMembers.Count < 1)
            return AllRandom();

        return Deck.instance.GetUnit(Random.Range(0, apathyMembers.Count));
    }

    // 모든 적 중 랜덤으로 공격
    virtual public Undead AllRandom()
    {
        foreach (HateLevel level in hateLevels)
            if (level == HateLevel.Tount)
                return TountRandom();

        return Deck.instance.GetUnit(Random.Range(0, hatePoints.Count));
    }

    // HateLevel이 높을 수록 높은 확률로 공격
    public Undead HateRandom()
    {
        foreach (HateLevel level in hateLevels)
            if (level == HateLevel.Tount)
                return TountRandom();

        List<int> convertNum = new List<int>();
        int temp = 0;
        foreach(HateLevel level in hateLevels)
        {
            int hp = 0;
            if (level == HateLevel.Apathy)
                hp = 0;
            else if (level == HateLevel.Realization)
                hp = 1;
            else if (level == HateLevel.Vigilance)
                hp = Random.Range(3, 5);
            else if (level == HateLevel.Danger)
                hp = Random.Range(7, 10);

            convertNum.Add(hp);
            temp += hp;
        }

        temp = Random.Range(0, temp);

        for (int i = 0; i < convertNum.Count; i++)
        {
            temp -= convertNum[i];
            if (temp < 1)
                return Deck.instance.GetUnit(i);
        }

        return AllRandom();
    }

    // HateLevel이 낮을 수록 높은 확률로 공격
    virtual public Undead ReverseHateRandom()
    {
        foreach (HateLevel level in hateLevels)
            if (level == HateLevel.Tount)
                return TountRandom();

        List<int> convertNum = new List<int>();
        int temp = 0;
        foreach (HateLevel level in hateLevels)
        {
            int hp = 0;
            if (level == HateLevel.Apathy)
                hp = 0;
            else if (level == HateLevel.Realization)
                hp = Random.Range(7, 10);
            else if (level == HateLevel.Vigilance)
                hp = Random.Range(3, 5);
            else if (level == HateLevel.Danger)
                hp = 1;

            convertNum.Add(hp);
            temp += hp;
        }

        temp = Random.Range(0, temp);

        for (int i = 0; i < convertNum.Count; i++)
        {
            temp -= convertNum[i];
            if (temp < 1)
                return Deck.instance.GetUnit(i);
        }

        return AllRandom();
    }

    // 공격을 받으면 증가
    virtual public void DamagedHate(GameObject ally, float hitDamage)
    {
        int targetNum = Deck.instance.Allys.IndexOf(ally);
        hatePoints[targetNum] += (int)(hitDamage * damage);
    }

    // 유닛이 죽을 때 사용
    virtual public void DieUnit(int num)
    {
        hatePoints.RemoveAt(num);
        hateLevels.RemoveAt(num);
    }

    // 도발에 당함
    virtual public void Tounted(int num)
    {
        hateLevels[num] = HateLevel.Tount;
    }
    
    // 도발에서 벗어남
    virtual public void TountCancel(int num)
    {
        Synchronize(num);
    }
}
