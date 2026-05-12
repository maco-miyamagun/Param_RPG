using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class StatusCtrl : MonoBehaviour
{
    [SerializeField]
    TextAsset statusText;

    int STRENGTH_BASE;
    float STRENGTH_GROWTH;
    public int Strength;

    int VITALITY_BASE;
    float VITALITY_GROWTH;
    public int Vitality;

    int AGILITY_BASE;
    float AGILITY_GROWTH;
    public int Agility;

    public int HealthPoint;

    public int DefensePoint;

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

    public void SetStatusForLevel(int level)
    {
        Strength = STRENGTH_BASE + Mathf.FloorToInt((level - 1) * STRENGTH_GROWTH);
        Vitality = VITALITY_BASE + Mathf.FloorToInt((level - 1) * VITALITY_GROWTH);
        Agility = AGILITY_BASE + Mathf.FloorToInt((level - 1) * AGILITY_GROWTH);

        HealthPoint = Vitality * 5;
        DefensePoint = (Strength + Vitality) / 2;
    }
}