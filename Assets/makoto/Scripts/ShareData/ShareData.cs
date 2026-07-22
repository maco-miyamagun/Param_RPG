using UnityEngine;

public class ShareData : MonoBehaviour
{
    public int level; // レベル
    public int experience; // 経験値
    public string battle_enemy_id; // バトルする敵のID
    public int door_id;
    public bool enable_first_enemy;
    public bool enable_second_enemy;
    public bool enable_third_enemy;

    public bool exist_bos;
    public int story_progress;

    public bool keyitem_map01;
    public bool keyitem_map02;
    public bool keyitem_map04;
    public bool keyitem_map06;

    public float x_pos;
    public float y_pos;
}