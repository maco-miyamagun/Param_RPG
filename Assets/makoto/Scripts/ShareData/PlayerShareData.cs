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
        shareData.door_id = data1.door_id;
        shareData.enable_first_enemy = data1.enable_first_enemy;
        shareData.enable_second_enemy = data1.enable_second_enemy;
        shareData.enable_third_enemy = data1.enable_third_enemy;
        shareData.exist_bos = data1.exist_bos;
        shareData.story_progress = data1.story_progress;
        shareData.keyitem_map01 = data1.keyitem_map01;
        shareData.keyitem_map02 = data1.keyitem_map02;
        shareData.keyitem_map04 = data1.keyitem_map04;
        shareData.keyitem_map06 = data1.keyitem_map06;

        shareData.x_pos = data1.x_pos;
        shareData.y_pos = data1.y_pos;

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