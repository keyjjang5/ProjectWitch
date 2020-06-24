using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    /*
     * instance : Singleton 패턴 사용
     * saveDeck : 보관중인 덱
     * tempDeck : 전투시에 임시로 만들어 사용하는 덱
     * graveyard : 무덤에 들어있는 카드
     * allys : 동료들
     */

    public static Deck instance;
    [SerializeField] List<GameObject> saveDeck = new List<GameObject>();
    [SerializeField] List<GameObject> battleDeck = new List<GameObject>();
    [SerializeField] List<GameObject> graveyard = new List<GameObject>();
    [SerializeField] List<GameObject> allys = new List<GameObject>();

    public List<GameObject> Allys { get { return allys; } }


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        AddUnit((int)UnitName.Warrior);
        AddUnit((int)UnitName.Supporter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 무덤에 있는 카드를 덱으로 옮기고 섞는다.
    public void MixGraveyard()
    {
        battleDeck.AddRange(graveyard);
        //foreach(GameObject card in tempDeck)
        //{
            //card.transform.SetParent(transform.Find("Deck"));
        //}
        graveyard.Clear();

        Shuffle();
    }

    // 덱에 있는 카드를 섞는다.
    public void Shuffle()
    {
        List<GameObject> temp = new List<GameObject>();

        while (battleDeck.Count > 0)
        {
            int i = Random.Range(0, battleDeck.Count);
            temp.Add(battleDeck[i]);
            battleDeck.RemoveAt(i);
        }

        battleDeck.AddRange(temp);
        temp.Clear();
    }

    // 덱에 카드를 추가한다.(임시 버전)
    public void AddCard(GameObject unit)
    {
        foreach (GameObject card in unit.GetComponent<Undead>().Cards)
        {
            saveDeck.Add(card);
        }
    }

    // 덱에서 Unit이 없어짐에 따라 카드를 제거한다.
    public void RemoveCard(GameObject unit)
    {
        foreach (GameObject card in unit.GetComponent<Undead>().Cards)
        {
            saveDeck.Remove(card);
        }
    }

    // 내 덱을 복사해서 전투에 사용할 덱을 만든다.
    public void CreateCopyDeck()
    {
        ClearCopyDeck();

        GameObject newCard;

        foreach (GameObject ally in allys)
        {
            GameObject newAlly = ally; //Instantiate(ally, transform.GetChild(i));
            newAlly.SetActive(true);

            foreach (GameObject card in ally.GetComponent<Undead>().Cards)
            {
                int i = card.GetComponent<Card>().CardNum;
                newCard = Instantiate(card, newAlly.transform);
                newCard.GetComponent<Card>().SetCardNum(i);

                newCard.SetActive(true);
                newCard.SetActive(false);

                battleDeck.Add(newCard);
            }
        }

        for (int i = 0; i < allys.Count; i++)
        {
            HPSystem.instance.ActiveUnitHpBar(allys[i].GetComponent<Undead>(), i);
        }
    }

    // 전투가 끝나면 임시로 만든 덱을 정리함
    public void ClearCopyDeck()
    {
        Debug.Log("ClearCopyDeck Start");
        battleDeck.Clear();
        graveyard.Clear();
    }

    // 사용한 카드를 무덤에 보낸다.
    public void AddGraveyard(GameObject usedCard)
    {
        graveyard.Add(usedCard);
        usedCard.SetActive(false);
        // usedCard.transform.SetParent(transform.parent.Find("Graveyard"));
    }

    // 전투덱에 있는 num번째 카드를 제거한다.
    public GameObject Remove(int num)
    {
        GameObject temp = battleDeck[num];
        battleDeck.RemoveAt(num);

        return temp;
    }

    // 전투덱의 길이를 반환한다.
    public int GetCardsLength()
    {
        return battleDeck.Count;
    }

    // 덱에 Unit을 추가한다.
    //public void AddUnit(string path)
    //{
    //    GameObject newAlly = Instantiate(Resources.Load(path) as GameObject, transform.GetChild(allys.Count));
    //    newAlly.SetActive(false);
    //    allys.Add(newAlly);
    //    newAlly.GetComponent<Undead>().SetPosition(allys.Count - 1);
    //    AddCard(allys[allys.Count - 1]);
    //}

    // 덱에 Unit을 추가한다.
    public void AddUnit(int num)
    {
        string path = DataBase.instance.UnitData[num]["Path"] as string;

        GameObject loadAlly = Resources.Load(path) as GameObject;
        loadAlly.SetActive(false);

        GameObject newAlly = Instantiate(loadAlly, transform.GetChild(allys.Count));
        newAlly.GetComponent<Undead>().SetUnitNum(num);
        // awake 발생용
        newAlly.SetActive(true);
        newAlly.SetActive(false);

        allys.Add(newAlly);
        newAlly.GetComponent<Undead>().SetPosition(allys.Count - 1);

        AddCard(allys[allys.Count - 1]);
    }

    // 덱에 있는 Unit을 제거한다.
    public void RemoveUnit(GameObject unit)
    {
        allys.Remove(unit);
        RemoveCard(unit);
    }

    // num번째의 유닛을 반환한다.
    public Undead GetUnit(int num)
    {
        return allys[num].GetComponent<Undead>();
    }

    // 모든 유닛의 DrawCount를 반환한다.
    public int GetDrawCount()
    {
        int drawCount = 0;
        foreach(GameObject ally in allys)
        {
            drawCount += ally.GetComponent<Undead>().BattleDrawCount;
        }

        return drawCount;
    }

    // 유닛이 죽을 때 실행한다.
    public void DieUnit(GameObject unit)
    {
        int i = allys.IndexOf(unit);
        HateSystem.instance.DieUnit(i);
        allys.Remove(unit);
        // 영정사진걸기
        // 카드 사용 막기
        for (int j = 2; j < unit.transform.childCount; j++)
        {
            unit.transform.GetChild(j).GetComponent<Card>().Sealed();
        }
        // 상태이상 제거하기
        unit.GetComponent<Unit>().ConditionClear();

        if (allys.Count == 0)
            TurnSystem.instance.BattleEnd();
    }

    // 덱과 무덤의 모든 카드들의 코스트를 num만큼 변경한다.
    public void ChangeCost(int num)
    {
        foreach (GameObject card in battleDeck)
        {
            card.GetComponent<Card>().ChangeCost(num);
        }

        foreach (GameObject card in graveyard)
        {
            card.GetComponent<Card>().ChangeCost(num);
        }
    }
}
