using UnityEngine;

public class PlayerLevelCtrl : MonoBehaviour
{
    // ステータスのデータ
    [SerializeField] EntityStatus entityStatus;

    // シェアデータ
    [SerializeField] ShareData shareData;

    // 経験値が閾値を超えたらレベルアップ
    public void CheckLevelUp()
    {
        int experienceThreshold = GetExperienceForNextLevel(shareData.level);

        if (shareData.experience >= experienceThreshold)
        {
            shareData.level++;
            shareData.experience -= experienceThreshold; // 余剰分を繰り越す

            SetStatusForLevel(shareData.level);
        }
    }

    // 次のレベルに必要な経験値を返す
    public int GetExperienceForNextLevel(int current_level)
    {
        return current_level * 100; // 次のレベルに必要な経験値の例
    }

    // レベルに応じて各ステータスを計算する
    public void SetStatusForLevel(int level)
    {
        const int STR_BASE = 2;
        const int VIT_BASE = 2;
        const int AGI_BASE = 5;

        const float STR_GROWTH = 1.2f;
        const float VIT_GROWTH = 1.2f;
        const float AGI_GROWTH = 1.2f;

        entityStatus.STR = STR_BASE + Mathf.FloorToInt((level - 1) * STR_GROWTH);
        entityStatus.VIT = VIT_BASE + Mathf.FloorToInt((level - 1) * VIT_GROWTH);
        entityStatus.AGI = AGI_BASE + Mathf.FloorToInt((level - 1) * AGI_GROWTH);

        entityStatus.HIT_POINT = entityStatus.VIT * 5;
        entityStatus.STAMINA_MAX = 15;

        entityStatus.stamina = entityStatus.STAMINA_MAX;
    }
}
