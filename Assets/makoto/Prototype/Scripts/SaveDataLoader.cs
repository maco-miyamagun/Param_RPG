using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class SaveDataLoader : MonoBehaviour
{
    //データファイルのパス
    string filePath;
    //セーブデータのインスタンス
    SaveData saveData;

    void Awake()
    {
        //セーブデータファイルの初期化
        DataFileInitialize();
    }

    //セーブデータファイルの初期化
    void DataFileInitialize()
    {

        //環境によって、ファイル参照ディレクトリを書き換え
#if UNITY_EDITOR
        //開発中の場合
        filePath = Application.dataPath + "/SaveData/save_data.json";
#else
        //ビルド後の場合
		filePath = Application.persistentDataPath + "/save_data.json";
#endif

        // データファイルが存在しない場合は初期データを作成して保存する
        if (!File.Exists(filePath))
        {
            //初期セーブデータの作成
            saveData = new SaveData
            {
                level = 1,
                experience = 0,
                battle_enemy_id = "sample_enemy"
            };

            //セーブデータの文字列変換とファイルへの書き込み
            WriteSaveData();
        }
        else
        {
            //データファイルが存在する場合は読み込む
            GetSaveData();
        }
    }

    //セーブデータの読み込み
    public void GetSaveData()
    {
        //セーブデータの読み込み
        string json_data = File.ReadAllText(filePath);
        saveData = JsonUtility.FromJson<SaveData>(json_data);
    }

    //セーブデータファイルの変更・書き込み
    public void WriteSaveData()
    {
        // セーブデータの文字列変換
        string json_data = JsonUtility.ToJson(saveData, true);
        // ファイルへの書き込み
        File.WriteAllText(filePath, json_data);
    }

    public SaveData GetCurrentSaveData()
    {
        return saveData;
    }
}


//セーブデータのクラス
[System.Serializable]
public class SaveData
{
    //プレイヤーレベル
    public int level;
    //経験値
    public int experience;
    //戦闘する敵のID
    public string battle_enemy_id;

    public List<string> weapon_list;

    public int life;
    public int health_item_num;

    public float x_pos;
    public float y_pos;
}