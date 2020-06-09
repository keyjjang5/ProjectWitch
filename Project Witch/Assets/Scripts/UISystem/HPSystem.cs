using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSystem : MonoBehaviour
{
    public static HPSystem instance;
    List<Unit> units = new List<Unit>();

    private void Awake()
    {
        instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 해당 유닛의 HpBar를 활성화 시키고 연결한다.
    public void ActiveHpBar(Unit unit, int num)
    {
        transform.GetChild(num).gameObject.SetActive(true);
        Connect(unit);
    }

    // 유닛을 연결한다.
    public void Connect(Unit unit)
    {
        units.Add(unit);
    }

    // 연결된 유닛들의 체력UI를 갱신한다.
    public void UpdateHp()
    {
        for(int i=0;i<units.Count;i++)
        {
            transform.GetChild(i).GetComponent<Slider>().value = units[i].GetHPRate();
            transform.GetChild(i).Find("Text").GetComponent<Text>().text = units[i].Hp + " / " + units[i].MaxHp;
        }
    }
}
