using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    /*
     * instance : Singleton 패턴 사용
     * cards : 손에 있는 카드들의 List
     * maxCost : 플레이어가 가지고 있는 최대치의 Cost
     * currentCost : 플레이어가 가지고 있는 사용 가능한 Cost
     * costText : 현재 코스트와 최대 코스트를 나타내는 Text오브젝트
     */

    public static Hand instance;
    [SerializeField] List<GameObject> cards = new List<GameObject>();
    private int maxCost;
    [SerializeField] int currentCost;

    public GameObject costText;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        maxCost = 3;
        currentCost = maxCost;
        costText = GameObject.Find("UI Canvas/Cost");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 플레이어의 차례가 시작 할 때 사용한다.
    public void HandStart()
    {
        ChargeCost(maxCost);
        costText.GetComponent<Text>().text = currentCost + " / " + maxCost;
    }

    // 플레이어의 차례가 끝날 때 사용한다.
    public void HandEnd()
    {

    }

    // 손에 카드를 추가 한다.
    public void Add()
    {
        
    }

    // 손에 있는 num번쨰 카드를 버린다.
    public GameObject Discard(int num)
    {
        GameObject temp = cards[num];
        if (temp.GetComponent<Card>().Seal)
            return null;

        cards.RemoveAt(num);

        return temp;
    }

    // 손에 있는 card를 target에게 사용한다.
    public bool Use(GameObject card, GameObject target)
    {
        Card tempCard = card.GetComponent<Card>();
        int cost = tempCard.BattleCost;

        if (cost > currentCost)
            return false;
        if (!tempCard.Use(target))
            return false;

        // 카드 사용 코스트 소모, 0보다 작으면 0으로 퉁침
        if (cost >= 0)
            currentCost -= cost;

        // Ready 예외사항 : 이런 식으로 하기 싫은데 좋은 방법을 못찾겠음
        if (cost == tempCard.BattleCost)
        {
            CountCondition tempCon = card.transform.parent.GetComponent<Unit>().SearchCondition(typeof(Ready)) as CountCondition;
            if (tempCon != null)
                tempCon.DecreaseCount(tempCon.Count);
        }

        costText.GetComponent<Text>().text = currentCost + " / " + maxCost;

        cards.Remove(card);

        return true;
    }

    // 덱에서 손으로 카드를 가져온다.
    public void Draw(GameObject card)
    {
        cards.Add(card);
        card.SetActive(true);

        Sort(card);
    }

    // 손 패의 개수를 반환한다.
    public int GetCardsLength()
    {
        return cards.Count;
    }

    /*손 패를 보기 좋게 정렬한다.아래의 Sort로 바꿈, 보존용
    public void Sort()
    {
        int num = cards.Count;
        int distance;
        // 홀수
        if (num % 2 == 1)
        {
            distance = (num - 1) / 2;

            for (int i = 0; i < num; i++)
            {
                cards[i].transform.SetPositionAndRotation(new Vector3(-distance * 1, cards[i].transform.position.y, -distance * 0.1f),
                                                            cards[i].transform.rotation);
                distance -= 1;
            }
        }
        // 짝수
        else
        {
            distance = num / 2;

            for (int i = 0; i < num; i++)
            {
                cards[i].transform.SetPositionAndRotation(new Vector3((-distance * 1) + 0.5f, cards[i].transform.position.y, -distance * 0.1f),
                                                            cards[i].transform.rotation);
                distance -= 1;
            }
        }
    }
    */

    // 가저온 카드를 가지고 있는 Unit에게 정리 요청
    public void Sort(GameObject card)
    {
        card.GetComponentInParent<Undead>().Sort();
    }

    // 모든 카드를 정리
    public void SortAll()
    {
        
    }

    // num 만큼 currentCost를 회복함
    public void ChargeCost(int num)
    {
        currentCost += num;
        if (currentCost > maxCost)
            currentCost = maxCost;
    }

    public void ChangeCost(int num)
    {
        foreach(GameObject card in cards)
        {
            card.GetComponent<Card>().ChangeCost(num);
        }
    }
}
