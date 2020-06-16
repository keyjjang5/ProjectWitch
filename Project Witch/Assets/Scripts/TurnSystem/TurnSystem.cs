using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    /*
     * instance : Singleton 패턴 사용
     * cardSystem : CardSystem 저장
     * enemySystem : EnemySystem 저장
     */

    public static TurnSystem instance;
    private CardSystem cardSystem;
    private EnemySystem enemySystem;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        cardSystem = CardSystem.instance;
        enemySystem = EnemySystem.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 전투 시작
    public void BattleStart()
    {
        cardSystem.BattleStart();
        enemySystem.Load(3);
        PlayerTurnStart();

        HPSystem.instance.UpdateHp();
    }

    // 전투 종료
    public void BattleEnd()
    {
        cardSystem.BattleEnd();
        Debug.Log("BattleEnd");
    }

    // 플레이어의 턴을 시작
    public void PlayerTurnStart()
    {
        ConditionSystem.instance.PlayerTurnStart();
        cardSystem.CardStart();
    }

    // 플레이어의 턴이 끝남
    public void PlayerTurnEnd()
    {
        ConditionSystem.instance.PlayerTurnEnd();

        cardSystem.CardEnd();

        EnemyTurnStart();
    }

    // 적의 턴을 시작
    public void EnemyTurnStart()
    {
        ConditionSystem.instance.EnemyTurnStart();
        enemySystem.EnemyStart();
    }

    // 적의 턴이 끝남
    public void EnemyTurnEnd()
    {
        ConditionSystem.instance.EnemyTurnEnd();

        enemySystem.EnemyEnd();

        PlayerTurnStart();
    }
}