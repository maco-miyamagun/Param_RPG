using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    //セーブデータローダーの参照
    [SerializeField]
    SaveDataLoader saveDataLoader;

    //敵のプレハブの参照（例: "sample_enemy"というIDに対応するプレハブ）
    [SerializeField]
    GameObject sampleEnemyPrefab;

    //生成された敵の参照
    GameObject enemy;

    [SerializeField]
    PlayerLevelCtrl playerLevelCtrl;

    [SerializeField]
    DamageCtrl player_damageCtrl;

    void Start()
    {
        //敵の初期化
        InitializeEnemy();
    }

    public void WinPlayer()
    {
        playerLevelCtrl.Experience += 100;

        SaveData save_data = saveDataLoader.GetCurrentSaveData();
        save_data.experience = playerLevelCtrl.Experience;
        save_data.level = playerLevelCtrl.Level;
        save_data.life = player_damageCtrl.Life;
        save_data.x_pos = playerLevelCtrl.transform.position.x;
        save_data.y_pos = playerLevelCtrl.transform.position.y;
        saveDataLoader.WriteSaveData();

        Debug.Log("Player wins!");
        SceneManager.LoadScene("Field");
    }

    public void LosePlayer()
    {
        Debug.Log("Player loses!");

        // SceneManager.LoadScene("game_over");
    }

    //敵の初期化
    void InitializeEnemy()
    {
        //セーブデータから敵のIDを取得して、対応する敵を生成
        SaveData saveData = saveDataLoader.GetCurrentSaveData();

        //敵のIDに基づいて敵を生成
        switch (saveData.battle_enemy_id)
        {
            //例: "sample_enemy"というIDに対してsampleEnemyPrefabを生成
            case "sample_enemy":
                enemy = Instantiate(sampleEnemyPrefab, transform.position, transform.rotation);
                break;

            //無効なIDの場合のエラーハンドリング
            default:
                Debug.LogError("Invalid enemy ID: " + saveData.battle_enemy_id);
                break;
        }
    }
}
