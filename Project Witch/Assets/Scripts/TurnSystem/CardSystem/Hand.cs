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
    uint maxCost;
    [SerializeField] uint currentCost;

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

    // 손에 있는 num번쨰 카드를 제거한다.
    public void Remove(int num)
    {
        cards.RemoveAt(num);
    }

    // 손에 있는 card를 target에게 사용한다.
    public bool Use(GameObject card, GameObject target)
    {
        uint cost = card.GetComponent<Card>().Cost;
        if (cost > currentCost)
            return false;

        // 카드 사용 코스트 소모
        currentCost -= cost;
        costText.GetComponent<Text>().text = currentCost + " / " + maxCost;

        card.GetComponent<Card>().Use(target);
        cards.Remove(card);

        return true;
    }

    // 덱에서 손으로 카드를 가져온다.
    public void Draw(GameObject card)
    {
        card.transform.SetParent(transform.root);
        card.transform.localScale = Vector3.one;
        card.transform.SetPositionAndRotation(new Vector3(transform.position.x, -3, transform.position.z),
                                                            transform.rotation);
        cards.Add(card);
        card.transform.SetParent(transform.Find("Hand"));
        card.SetActive(true);
    }

    // 손 패의 개수를 반환한다.
    public int GetCardsLength()
    {
        return cards.Count;
    }

    // 손 패를 보기 좋게 정렬한다.
    public void Sort()
    {
        int num = cards.Count;
        int distance;
        // 홀수
        if (num % 2 == 1)
        {
            distance = (num - 1) / 2;
            
            for(int i = 0;i<num;i++)
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

    // num 만큼 currentCost를 회복함
    public void ChargeCost(uint num)
    {
        currentCost += num;
        if (currentCost > maxCost)
            currentCost = maxCost;
    }
}
