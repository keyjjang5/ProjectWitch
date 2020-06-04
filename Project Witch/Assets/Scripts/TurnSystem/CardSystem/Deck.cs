using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    /*
     * instance : Singleton 패턴 사용
     * cards : 덱에 들어있는 카드
     * graveyard : 무덤에 들어있는 카드
     */

    public static Deck instance;
    [SerializeField] List<GameObject> saveDeck = new List<GameObject>();
    [SerializeField] List<GameObject> tempDeck = new List<GameObject>();
    [SerializeField] List<GameObject> graveyard = new List<GameObject>();
    [SerializeField] List<Unit> allys = new List<Unit>();

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        AddUnit();
        AddUnit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 무덤에 있는 카드를 덱으로 옮기고 섞는다.
    public void MixGraveyard()
    {
        tempDeck.AddRange(graveyard);
        foreach(GameObject card in tempDeck)
        {
            card.transform.SetParent(transform.Find("Deck"));
        }
        graveyard.Clear();

        Shuffle();
    }

    // 덱에 있는 카드를 섞는다.
    public void Shuffle()
    {
        List<GameObject> temp = new List<GameObject>();

        while (tempDeck.Count > 0)
        {
            int i = Random.Range(0, tempDeck.Count);
            temp.Add(tempDeck[i]);
            tempDeck.RemoveAt(i);
        }

        tempDeck.AddRange(temp);
        temp.Clear();
    }

    // 덱에 카드를 추가한다.(임시 버전)
    public void AddCard(Unit unit)
    {
        foreach (GameObject card in unit.Cards)
        {
            saveDeck.Add(card);
        }
    }

    // 덱에서 Unit이 없어짐에 따라 카드를 제거한다.
    public void RemoveCard(Unit unit)
    {
        foreach (GameObject card in unit.Cards)
        {
            saveDeck.Remove(card);
        }
    }

    // 내 덱을 복사해서 전투에 사용할 덱을 만든다.
    public void CreateCopyDeck()
    {
        GameObject newCard;

        foreach (GameObject card in saveDeck)
        {
            newCard = Instantiate(card);
            newCard.SetActive(false);
            newCard.transform.SetParent(transform.Find("Deck"));
            tempDeck.Add(newCard);
        }
    }

    public void ClearCopyDeck()
    {
        tempDeck.Clear();
    }

    // 사용한 카드를 무덤에 보낸다.
    public void AddGraveyard(GameObject usedCard)
    {
        graveyard.Add(usedCard);
        usedCard.SetActive(false);
        usedCard.transform.SetParent(transform.Find("Graveyard"));
    }

    // 전투덱에 있는 num번째 카드를 제거한다.
    public GameObject Remove(int num)
    {
        GameObject temp = tempDeck[num];
        tempDeck.RemoveAt(num);

        return temp;
    }

    // 전투덱의 길이를 반환한다.
    public int GetCardsLength()
    {
        return tempDeck.Count;
    }

    // 덱에 Unit을 추가한다. (미완성, 이후에 자유로운 추가가 가능해질 것)
    public void AddUnit()
    {
        allys.Add(new BaseUnit());
        AddCard(allys[allys.Count - 1]);
    }

    // 덱에 있는 Unit을 제거한다.
    public void RemoveUnit(Unit unit)
    {
        allys.Remove(unit);
        RemoveCard(unit);
    }

    public void test()
    {
        RemoveUnit(allys[0]);
    }
}
