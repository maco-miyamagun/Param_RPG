using System;
using System.Collections;
using UnityEngine;

// 剣の斬撃エフェクトA
public class SwordSlashAEffect : MonoBehaviour, IAttackEffect
{
    // 基礎ダメージ
    [Header("基礎ダメージ")]
    public int BaseDamage { get; set; } = 2;
    // 計算後の最終ダメージ
    [Header("最終ダメージ")]
    public int Damage { get; set; } = 0;
    // 回転開始角度
    [Header("回転開始角度")]
    [SerializeField] float startAngleZ = -70f;
    // 回転終了角度
    [Header("回転終了角度")]
    [SerializeField] float endAngleZ = 70f;
    // 回転にかける時間
    [Header("回転にかける時間")]
    [SerializeField] float durationSeconds = 0.01f;

    // ヒット済みなら回転を止める
    bool isEffectActive = true;

    // 何かに当たったら停止フラグを立てる
    public void HitTarget(GameObject target)
    {
        isEffectActive = false;
    }

    IEnumerator IAttackEffect.PlayEffect(GameObject _from)
    {
        // Euler角(度)で -70 → +70 を確実に回す
        float safeDuration = Mathf.Max(0f, durationSeconds);
        if (safeDuration <= 0f)
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, endAngleZ);
        }
        else
        {
            float elapsed = 0f;
            while (elapsed < safeDuration)
            {
                if (!isEffectActive)
                {
                    break;
                }

                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / safeDuration);
                float currentAngleZ = Mathf.Lerp(startAngleZ, endAngleZ, t);
                transform.localRotation = Quaternion.Euler(0f, 0f, currentAngleZ);
                yield return null;
            }
        }

        isEffectActive = false;
        Destroy(this.gameObject);
    }
}
