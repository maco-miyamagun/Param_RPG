using UnityEngine;

public class EntityAttackCtrl : MonoBehaviour
{
    // ステータスのデータ
    [SerializeField] EntityStatus entityStatus;

    // ステータスからダメージを計算する
    public int CalculateDamage(int base_damage)
    {
        return entityStatus.STR + base_damage;
    }

    // クールダウン時間を計算する
    public float CalculateCooldown(float base_cooldown_time)
    {
        return Mathf.Max(0.2f, base_cooldown_time);
    }
}
