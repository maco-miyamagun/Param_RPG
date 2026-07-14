using System.Collections;
using UnityEngine;

public class BattleManage : MonoBehaviour
{
    // 敵のプレハブの参照
    [SerializeField] GameObject goblin_prefub;
    [SerializeField] int GOBLIN_EXP;

    [SerializeField] ShareData shareData;
    [SerializeField] PlayerLevelCtrl playerLevelCtrl;

    int reward_exp;

    void Start()
    {
        //敵の初期化
        InitializeEnemy();
    }

    public void WinPlayer()
    {
        StartCoroutine(CorWinPlayer());
    }

    public IEnumerator CorWinPlayer()
    {
        shareData.experience += reward_exp;
        playerLevelCtrl.CheckLevelUp();

        Debug.Log("Player wins!");

        FindObjectsByType<GameManage>(FindObjectsSortMode.None)[0].ChangeScene("");
        yield return new WaitForSeconds(1f);
    }

    public void LosePlayer()
    {
        StartCoroutine(CorLosePlayer());
    }

    public IEnumerator CorLosePlayer()
    {
        Debug.Log("Player loses!");
        yield return new WaitForSeconds(1f);
    }

    //敵の初期化
    void InitializeEnemy()
    {
        // 敵のIDに基づいて敵を生成
        switch (shareData.battle_enemy_id)
        {
            // ゴブリンの場合
            case "goblin":
                Instantiate(goblin_prefub, transform.position, transform.rotation, this.transform);

                reward_exp = GOBLIN_EXP;
                break;

            // 無効なIDの場合のエラーハンドリング
            default:
                Debug.LogError("Invalid enemy ID: " + shareData.battle_enemy_id);
                break;
        }
    }
}
