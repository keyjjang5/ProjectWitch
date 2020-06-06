using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardSystem : MonoBehaviour
{
    /*
     * instance : Singleton 패턴 사용
     * deck : 덱을 관리
     * hand : 손 패를 관리
     * readyFlag : 사용 준비중을 확인
     * readyCard : 사용 준비중인 카드를 가지고 있음
     */
    public static CardSystem instance;

    [SerializeField] Deck deck;
    [SerializeField] Hand hand;

    private bool readyFlag;
    public bool ReadyFlag { get { return readyFlag; } }
    [SerializeField] GameObject readyCard;

    private void Awake()
    {
        instance = this;
        readyFlag = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        deck = Deck.instance;
        hand = Hand.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(readyFlag)
        {
            if (Input.GetMouseButton(1))
                Cancel();
        }
    }

    // 사용 할 카드를 선택한다.
    public void Select(GameObject card)
    {
        readyCard = card;
        readyFlag = true;
    }

    // readyCard를 사용한다.
    public GameObject Use(GameObject target, int depth)
    {
        if (hand.Use(readyCard, target, depth))
            deck.AddGraveyard(readyCard);

        hand.Sort(readyCard);

        Cancel();

        return target;
    }

    // 준비 중이던 카드를 초기화 한다.
    public void Cancel()
    {
        readyCard.GetComponent<Card>().Cancel();
        readyCard = null;
        readyFlag = false;
    }

    // 카드를 뽑는다.
    public void Draw()
    {
        GameObject temp;

        if (deck.GetCardsLength() <= 0)
            deck.MixGraveyard();

        if (deck.GetCardsLength() <= 0)
        {
            Debug.Log("Draw못해욧");
            return;
        }

        temp = deck.Remove(0);

        hand.Draw(temp);

        Debug.Log("Draw / 덱에 남은 개수 : " + deck.GetCardsLength() + "손에 있는 개수 : " + hand.GetCardsLength());
    }

    // 턴 시작 시에 사용한다.
    public void CardStart()
    {
        hand.HandStart();
        Draw();
        Draw();

        hand.SortAll();
    }

    // 턴 종료시에 사용한다.
    public void CardEnd()
    {

    }
}
