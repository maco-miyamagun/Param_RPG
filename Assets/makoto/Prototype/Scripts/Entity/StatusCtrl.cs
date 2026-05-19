using UnityEngine;

// JSONとレベルからステータスを計算する
public class StatusCtrl : MonoBehaviour
{
    // ステータスのJSONデータ
    [SerializeField]
    TextAsset statusText;

    // 基礎値
    int STRENGTH_BASE;
    float STRENGTH_GROWTH;
    // 現在の腕力
    public int Strength;

    int VITALITY_BASE;
    float VITALITY_GROWTH;
    // 現在の体力
    public int Vitality;

    int AGILITY_BASE;
    float AGILITY_GROWTH;
    // 現在の敏捷
    public int Agility;

    // 派生ステータス
    public int HealthPoint;

    public int DefensePoint;

    // JSONから基礎値と成長率を読み込む
    public void SetStatusFromJson()
    {
        EntityStatus status = JsonUtility.FromJson<EntityStatus>(statusText.text);

        STRENGTH_BASE = status.str_base;
        VITALITY_BASE = status.vit_base;
        AGILITY_BASE = status.agi_base;

        STRENGTH_GROWTH = status.str_growth;
        VITALITY_GROWTH = status.vit_growth;
        AGILITY_GROWTH = status.agi_growth;
    }

    // レベルに応じて各ステータスを計算する
    public void SetStatusForLevel(int level)
    {
        Strength = STRENGTH_BASE + Mathf.FloorToInt((level - 1) * STRENGTH_GROWTH);
        Vitality = VITALITY_BASE + Mathf.FloorToInt((level - 1) * VITALITY_GROWTH);
        Agility = AGILITY_BASE + Mathf.FloorToInt((level - 1) * AGILITY_GROWTH);

        HealthPoint = Vitality * 5;
        DefensePoint = (Strength + Vitality) / 2;
    }
}