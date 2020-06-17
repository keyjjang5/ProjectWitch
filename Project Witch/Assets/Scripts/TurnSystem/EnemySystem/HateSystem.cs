using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HateSystem : MonoBehaviour
{
    /*
     * instance : Singleton 패턴 사용
     * hates : 각 유닛이 가지고 있는 hate 클래스의 집합
     */
    public static HateSystem instance;
    [SerializeField]
    List<Hate> hates = new List<Hate>();

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // hate 추가
    public void Add(Hate hate)
    {
        hates.Add(hate);
    }

    // hates를 갱신
    public void HateUpdate()
    {
        foreach(Hate hate in hates)
        {
            hate.Update();
        }
    }

    // 유닛이 죽으면 실행
    public void DieUnit(int num)
    {
        foreach (Hate hate in hates)
        {
            hate.DieUnit(num);
        }
    }

    // 적이 죽으면 실행
    public void DieEnemy(int num)
    {
        hates.RemoveAt(num);
    }

    public void Tounted(int num)
    {
        foreach (Hate hate in hates)
        {
            hate.Tounted(num);
        }
    }

    public void TountCancel(int num)
    {
        foreach (Hate hate in hates)
        {
            hate.TountCancel(num);
        }
    }
}
