using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSystem : MonoBehaviour
{
    /*
     * instance : Singleton 패턴 사용
     * units : 연결된 unit들의 정보를 가짐
     * enemies : 연결된 enemy들의 정보를 가짐
     */

    public static HPSystem instance;
    List<Unit> units = new List<Unit>();
    List<Enemy> enemies = new List<Enemy>();

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
    public void ActiveUnitHpBar(Unit unit, int num)
    {
        transform.GetChild(num).gameObject.SetActive(true);
        ConnectUnit(unit);
    }

    // 해당 적의 HpBar를 활성화 시키고 연결한다.
    public void ActiveEnemyHpBar(Enemy enemy, int num)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(enemy.gameObject.transform.GetChild(0).position);
        // screenPos.y += 100.0f;
        float x = screenPos.x;
        float z = screenPos.z;


        transform.GetChild(3 + num).position = new Vector3(x, screenPos.y, z);
        
        transform.GetChild(3 + num).gameObject.SetActive(true);
        ConnectEnemy(enemy);
    }

    // 유닛을 연결한다.
    public void ConnectUnit(Unit unit)
    {
        units.Add(unit); 
    }

    // 적을 연결한다.
    public void ConnectEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    // 연결된 유닛들의 체력UI를 갱신한다.
    public void UpdateHp()
    {
        //for(int i=0;i<units.Count;i++)
        foreach (Unit unit in units)
        {
            transform.GetChild(unit.Position).GetComponent<Slider>().value = unit.GetHpRate();
            transform.GetChild(unit.Position).Find("Text").GetComponent<Text>().text = unit.Hp + " / " + unit.MaxHp;
        }

        //for (int i = 0; i < enemies.Count; i++)
        foreach (Enemy enemy in enemies)
        {
            transform.GetChild(3 + enemy.Position).GetComponent<Slider>().value = enemy.GetHpRate();
            transform.GetChild(3 + enemy.Position).Find("Text").GetComponent<Text>().text = enemy.Hp + " / " + enemy.MaxHp;
        }
    }

    // 적이 죽을 시 UI를 지우고 연결을 해지한다.
    public void DieEnemy(Enemy enemy)
    {
        transform.GetChild(3 + enemy.Position).gameObject.SetActive(false);

        enemies.Remove(enemy);
    }

    // 유닛이 죽을 시 UI를 지우고 연결을 해지한다.
    public void DieUnit(Unit unit)
    {
        transform.GetChild(unit.Position).gameObject.SetActive(false);

        units.Remove(unit);
    }
}
