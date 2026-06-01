using UnityEngine;

// プレイヤーのレベルと経験値を管理する
[RequireComponent(typeof(StatusCtrl))]
public class PlayerLevelCtrl : MonoBehaviour
{
    // レベルに応じたステータス計算に使う
    StatusCtrl statusCtrl;

    // 現在レベル
    [Header("現在レベル")]
    [SerializeField]
    int level = 1;

    // 現在経験値
    [Header("現在経験値")]
    [SerializeField]
    int experience = 0;

    // セーブデータローダー
    SaveDataLoader saveDataLoader;

    // ステータス初期化
    void Awake()
    {
        saveDataLoader = FindFirstObjectByType<SaveDataLoader>();

        statusCtrl = GetComponent<StatusCtrl>();
        if (statusCtrl != null)
        {
            statusCtrl.SetStatusFromJson();
            statusCtrl.SetStatusForLevel(level);
        }

        if (saveDataLoader != null)
        {
            SaveData currentSaveData = saveDataLoader.GetCurrentSaveData();
            Level = currentSaveData.level;
            Experience = currentSaveData.experience;
        }
    }

    // レベル設定時にステータスを更新
    public int Level
    {
        get { return level; }
        set
        {
            if (value > 0)
            {
                level = value;
                if (statusCtrl != null)
                {
                    statusCtrl.SetStatusForLevel(level);
                }
            }
        }
    }

    // 経験値設定時にレベルアップ判定
    public int Experience
    {
        get { return experience; }
        set
        {
            if (value >= 0)
            {
                experience = value;
                CheckLevelUp();
            }
        }
    }

    // 経験値が閾値を超えたらレベルアップ
    private void CheckLevelUp()
    {
        int experienceThreshold = GetExperienceForNextLevel(level);
        if (experience >= experienceThreshold)
        {
            level++;
            experience -= experienceThreshold; // 余剰分を繰り越す
            Debug.Log("Level Up! Current Level: " + level);
            if (statusCtrl != null)
            {
                statusCtrl.SetStatusForLevel(level);
            }
        }
    }

    // 次のレベルに必要な経験値を返す
    private int GetExperienceForNextLevel(int current_level)
    {
        return current_level * 100; // 次のレベルに必要な経験値の例
    }
}
