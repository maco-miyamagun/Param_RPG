using UnityEngine;

public class PlayerShareData : MonoBehaviour
{
    [SerializeField] ShareData shareData;
    [SerializeField] EntityStatus entityStatus;

    public PlayerShareDataType playerShareDataType;

    public void SetData(ShareData data1, EntityStatus data2)
    {
        shareData.level = data1.level;
        shareData.experience = data1.experience;
        shareData.battle_enemy_id = data1.battle_enemy_id;

        entityStatus.STR = data2.STR;
        entityStatus.VIT = data2.VIT;
        entityStatus.AGI = data2.AGI;
        entityStatus.HIT_POINT = data2.HIT_POINT;
        entityStatus.STAMINA_MAX = data2.STAMINA_MAX;
        entityStatus.life = data2.life;
        entityStatus.stamina = data2.stamina;
    }

    public ShareData GetShareData()
    {
        return shareData;
    }

    public EntityStatus GetStatusData()
    {
        return entityStatus;
    }
}

public enum PlayerShareDataType
{
    HOST,
    CLIENT,
}