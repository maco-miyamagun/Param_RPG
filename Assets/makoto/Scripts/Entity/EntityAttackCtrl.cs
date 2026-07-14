using System.Collections;
using UnityEngine;

public class EntityAttackCtrl : MonoBehaviour
{
    // ステータスのデータ
    [SerializeField] EntityStatus entityStatus;

    public bool AttackEnabled = true; // 攻撃できるかどうか

    // ステータスからダメージを計算する
    public int CalculateDamage(int base_damage)
    {
        return entityStatus.STR + base_damage;
    }

    // クールダウン時間を計算する
    float CalculateCooldown(float base_cooldown_time)
    {
        return Mathf.Max(0.2f, base_cooldown_time);
    }

    // 攻撃のクールダウン処理
    IEnumerator AttackCooldownCor(float base_cooldown_time)
    {
        // 二重呼び出し防止
        if (!AttackEnabled) yield break;

        // 攻撃できないようにする
        AttackEnabled = false;

        // クールダウン時間
        yield return new WaitForSeconds(CalculateCooldown(base_cooldown_time));

        // 攻撃できるように戻す
        AttackEnabled = true;
    }

    // クールダウン処理のラッパー
    public void AttackCooldown(float base_cooldown_time)
    {
        StartCoroutine(AttackCooldownCor(base_cooldown_time));
    }
}
