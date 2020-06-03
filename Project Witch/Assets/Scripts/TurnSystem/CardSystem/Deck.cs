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
    [SerializeField] List<GameObject> cards = new List<GameObject>();
    [SerializeField] List<GameObject> graveyard = new List<GameObject>();
    [SerializeField] List<Unit> allys = new List<Unit>();

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Add();
        Add();
        Add();
        Add();
        Add();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 무덤에 있는 카드를 덱으로 옮기고 섞는다.
    public void MixGraveyard()
    {
        cards.AddRange(graveyard);
        foreach(GameObject card in cards)
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

        while (cards.Count > 0)
        {
            int i = Random.Range(0, cards.Count);
            temp.Add(cards[i]);
            cards.RemoveAt(i);
        }

        cards.AddRange(temp);
        temp.Clear();
    }

    // 덱에 카드를 추가한다.(임시 버전)
    public void Add()
    {
        GameObject newCard;

        // 실체화
        newCard = Resources.Load("Prefaps/Cards/RealBaseCard") as GameObject;
        newCard = Instantiate(newCard);
        newCard.SetActive(false);
        newCard.transform.SetParent(transform.Find("Deck"));

        cards.Add(newCard);


        newCard = Resources.Load("Prefaps/Cards/RealTestCard") as GameObject;
        newCard = Instantiate(newCard);
        newCard.SetActive(false);
        newCard.transform.SetParent(transform.Find("Deck"));

        cards.Add(newCard);
    }

    // 사용한 카드를 무덤에 보낸다.
    public void AddGraveyard(GameObject usedCard)
    {
        graveyard.Add(usedCard);
        usedCard.SetActive(false);
        usedCard.transform.SetParent(transform.Find("Graveyard"));
    }

    // 덱에있는 num번째 카드를 제거한다.
    public GameObject Remove(int num)
    {
        GameObject temp = cards[num];
        cards.RemoveAt(num);

        return temp;
    }

    // 덱의 길이를 반환한다.
    public int GetCardsLength()
    {
        return cards.Count;
    }
}
