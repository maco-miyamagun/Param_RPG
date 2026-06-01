using System.Collections;
using UnityEngine;

// 攻撃エフェクトの発生とクールダウンを管理する
[RequireComponent(typeof(StatusCtrl))]
public class AttackCtrl : MonoBehaviour
{
    // ダメージ計算用ステータス
    StatusCtrl statusCtrl;
    // 生成する攻撃エフェクトのプレハブ
    [Header("攻撃エフェクトのプレハブ")]
    public GameObject[] effectPrefabs;

    // 攻撃可能かどうか
    [Header("攻撃可能かどうか")]
    public bool AttackEnabled = true;

    // エフェクトに付与するタグ
    [Header("攻撃エフェクトのタグ")]
    public string attackEffect_tag_name;

    // 必須コンポーネントを取得する
    void Start()
    {
        statusCtrl = GetComponent<StatusCtrl>();
    }

    // 攻撃クールダウンの処理
    public IEnumerator AttackCooldown()
    {
        if (!AttackEnabled) yield break;

        AttackEnabled = false;
        yield return new WaitForSeconds(CalculateCooldown()); // 攻撃のクールダウン時間
        AttackEnabled = true;
    }

    // 指定インデックスの攻撃エフェクトを発生させる
    public void FireAttack(int effectIndex)
    {
        if (!AttackEnabled) return;
        if (effectPrefabs[effectIndex] == null) return;

        GameObject effectInstance = Instantiate(effectPrefabs[effectIndex], transform.position, Quaternion.identity, transform);

        if (!effectInstance.TryGetComponent<IAttackEffect>(out var attackEffect)) return;

        effectInstance.tag = attackEffect_tag_name;

        attackEffect.Damage = CalculateDamage(attackEffect.BaseDamage);
        StartCoroutine(attackEffect.PlayEffect(gameObject));

        StartCoroutine(AttackCooldown());
    }

    // ステータスからダメージを計算する
    public int CalculateDamage(int effect_damage)
    {
        return statusCtrl.Strength / 2 * effect_damage; // ダメージ計算の例
    }

    // ステータスからクールダウン時間を計算する
    public float CalculateCooldown()
    {
        return Mathf.Max(0.2f, 2 - statusCtrl.Agility / 10); // クールダウン時間の例
    }
}
