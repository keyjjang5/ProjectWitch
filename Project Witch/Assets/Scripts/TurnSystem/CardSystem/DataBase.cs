using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    /*
     * instance : Singleton 패턴 사용
     * cardData : 카드 데이터를 저장하는 변수
     * unitData : 유닛 데이터를 저장하는 변수
     * enemyData : 적 데이터를 저장하는 변수
     * mapData : 맵 데이터를 저장하는 변수
     */

    public static DataBase instance;

    private List<Dictionary<string, object>> cardData = new List<Dictionary<string, object>>();
    private List<Dictionary<string, object>> unitData = new List<Dictionary<string, object>>();
    private List<Dictionary<string, object>> enemyData = new List<Dictionary<string, object>>();
    private List<Dictionary<string, object>> mapData = new List<Dictionary<string, object>>();

    public List<Dictionary<string, object>> CardData { get { return cardData; } }
    public List<Dictionary<string, object>> UnitData { get { return unitData; } }
    public List<Dictionary<string, object>> EnemyData { get { return enemyData; } }
    public List<Dictionary<string, object>> MapData { get { return mapData; } }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        cardData = CSVParser.Read("Data/Card");
        unitData = CSVParser.Read("Data/Unit");
        enemyData = CSVParser.Read("Data/Enemy");
        mapData = CSVParser.Read("Data/Map");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
