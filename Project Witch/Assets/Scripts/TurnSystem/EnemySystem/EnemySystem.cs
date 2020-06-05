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
            enemy.GetComponent<Enemy>().Action();
        }

        TurnSystem.instance.EnemyTurnEnd();
    }

    // 적턴 종료 시 사용한다.
    public void EnemyEnd()
    {

    }

    // 적의 상태를 갱신한다.
    public void EnemyUpdate()
    {

    }

    // 파일에서 데이터를 읽어온다. 미완성
    public void Load()
    {
        enemies.Add(Resources.Load("Prefaps/Enemys/BaseEnemy") as GameObject);
        enemies.Add(Resources.Load("Prefaps/Enemys/BaseEnemy") as GameObject);
        enemies.Add(Resources.Load("Prefaps/Enemys/BaseEnemy") as GameObject);
        // 몬스터들 데이터 읽어오기
        List<GameObject> temp = new List<GameObject>();

        foreach (GameObject enemy in enemies)
        {
            GameObject newEnemy = Instantiate(enemy);
            newEnemy.SetActive(false);
            temp.Add(newEnemy);
        }
        enemies.Clear();
        enemies.AddRange(temp);


        Summon(enemies);
    }

    // 적 소환 함수, 미완성
    public void Summon(List<GameObject> enemies)
    {
        int i = enemies.Count;
        if (i % 2 == 1)
            i--;

        i = -i;
        
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
            // newEnemy 위치 등등 조절부
            enemy.transform.Translate(-i, 0, 0);

            i += 2;
        }
    }

    public void Die(GameObject enemy)
    {
        enemies.Remove(enemy);
        Destroy(enemy);
    }
}
