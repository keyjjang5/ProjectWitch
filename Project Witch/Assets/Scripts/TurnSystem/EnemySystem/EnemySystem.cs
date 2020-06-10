using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    /*
     * instance : Singleton 패턴 사용
     * enemies : 몬스터들의 정보
     */
    public static EnemySystem instance;
    [SerializeField] List<GameObject> enemies = new List<GameObject>();

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

    // 적턴 시작 시 사용한다.
    public void EnemyStart()
    {
        foreach(GameObject enemy in enemies)
        {
            if (Deck.instance.Allys.Count == 0)
                break;

            enemy.GetComponent<Enemy>().Action();
        }

        TurnSystem.instance.EnemyTurnEnd();
    }

    // 적턴 종료 시 사용한다.
    public void EnemyEnd()
    {
        HPSystem.instance.UpdateHp();
    }

    // 적의 상태를 갱신한다.
    public void EnemyUpdate()
    {

    }

    // 파일에서 Map데이터 num번째를 읽어온다.
    public void Load(int num)
    {
        // CSV 읽어서 쓸거임
        int count = (int)DataBase.instance.MapData[num]["Count"];

        for (int i = 1; i < count + 1; i++)
        {
            // CSV 읽어서 쓸거임
            int enemyPath = (int)DataBase.instance.MapData[num]["Enemy" + i];
            
            string resourcePath = DataBase.instance.EnemyData[enemyPath]["Path"] as string;
            int posNum = (int)DataBase.instance.MapData[num]["Pos" + i];
            //

            GameObject enemy = Resources.Load(resourcePath) as GameObject;
            GameObject newEnemy = Instantiate(enemy);
            newEnemy.GetComponent<Enemy>().SetPosition(posNum);

            newEnemy.transform.SetParent(transform.Find("Pos" + newEnemy.GetComponent<Enemy>().Position));

            newEnemy.transform.localPosition = Vector3.zero;
            newEnemy.SetActive(false);
            enemies.Add(newEnemy);
        }

        Summon(enemies);
    }

    // 적 소환 함수
    public void Summon(List<GameObject> enemies)
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
            HPSystem.instance.ActiveEnemyHpBar(enemy.GetComponent<Enemy>(), enemy.GetComponent<Enemy>().Position);
            HateSystem.instance.Add(enemy.GetComponent<Enemy>().Hate);
        }
    }

    // Enemy 사망시 사용
    public void Die(GameObject enemy)
    {
        HateSystem.instance.DieEnemy(enemies.IndexOf(enemy));
        enemies.Remove(enemy);
        Destroy(enemy);

        if (enemies.Count == 0)
            TurnSystem.instance.FightEnd();
    }
}
