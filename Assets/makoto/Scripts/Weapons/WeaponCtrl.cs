using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCtrl : MonoBehaviour
{
    // 使える武器のプレハブリスト
    [SerializeField] List<GameObject> weapon_prefabs;

    // 攻撃の計算に利用するクラス
    [SerializeField] EntityAttackCtrl attackCtrl;

    public int use_weapon_index = 0; // 現在使う武器のインデックス

    [SerializeField] GameObject fire_point; // 発射点

    public void Attack()
    {
        // 攻撃ができない場合と武器が存在しない場合に処理を中断
        if (!attackCtrl.AttackEnabled) return;
        if (weapon_prefabs[use_weapon_index] == null) return;

        // 指定されたインデックスの武器をインスタンス化
        Instantiate(weapon_prefabs[use_weapon_index], fire_point.transform.position, new(0f, 0f, 0f, 0f), fire_point.transform);
    }
}
