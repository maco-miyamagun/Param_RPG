using System;
using UnityEngine;

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

    void Start()
    {
        //敵の初期化
        InitializeEnemy();
    }

    public void WinPlayer()
    {
        Debug.Log("Player wins!");
    }

    public void LosePlayer()
    {
        Debug.Log("Player loses!");
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
