using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponCtrl : MonoBehaviour
{
    // 使える武器のプレハブリスト
    [SerializeField] List<GameObject> weapon_prefabs;

    // 攻撃の計算に利用するクラス
    [SerializeField] EntityAttackCtrl attackCtrl;

    public int use_weapon_index = 0; // 現在使う武器のインデックス
    public bool AttackEnabled = true; // 攻撃できるかどうか

    [SerializeField] GameObject fire_point; // 発射点

    // 攻撃のクールダウン処理
    IEnumerator AttackCooldownCor(float base_cooldown_time)
    {
        // 二重呼び出し防止
        if (!AttackEnabled) yield break;

        // 攻撃できないようにする
        AttackEnabled = false;

        // クールダウン時間
        yield return new WaitForSeconds(attackCtrl.CalculateCooldown(base_cooldown_time));

        // 攻撃できるように戻す
        AttackEnabled = true;
    }

    // クールダウン処理のラッパー
    public void AttackCooldown(float base_cooldown_time)
    {
        StartCoroutine(AttackCooldownCor(base_cooldown_time));
    }

    // 入力システムから攻撃入力を受け取る
    public void OnAttack()
    {
        // 攻撃ができない場合と武器が存在しない場合に処理を中断
        if (!AttackEnabled) return;
        if (weapon_prefabs[use_weapon_index] == null) return;

        // 指定されたインデックスの武器をインスタンス化
        Instantiate(weapon_prefabs[use_weapon_index], fire_point.transform.position, Quaternion.identity, fire_point.transform);
    }
}
