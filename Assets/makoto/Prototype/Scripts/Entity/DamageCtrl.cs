using System;
using UnityEngine;

// 被ダメージ処理と死亡判定を行う
[RequireComponent(typeof(StatusCtrl))]
public class DamageCtrl : MonoBehaviour
{
    // 防御力などの参照用
    StatusCtrl statusCtrl;
    // ダメージ演出の実装
    IDamageEffect damageEffect;

    // 現在HP
    [Header("現在HP")]
    [SerializeField]
    int life = 0;

    // 当たり判定で受け付ける攻撃タグ
    [Header("攻撃エフェクトのタグ")]
    [SerializeField] private string attackEffect_tag_name;

    // 必須コンポーネントを取得してHPを初期化する
    void Start()
    {
        statusCtrl = GetComponent<StatusCtrl>();
        damageEffect = GetComponent<IDamageEffect>();

        if (statusCtrl != null && Life == 0)
        {
            Life = statusCtrl.HealthPoint;
        }
    }

    // HPの安全な更新
    public int Life
    {
        get { return life; }
        set
        {
            if (value > 0)
            {
                life = value;
            }
        }
    }

    // 攻撃エフェクトに触れたらダメージを受ける
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (attackEffect_tag_name == "") return;

        if (collision.CompareTag(attackEffect_tag_name))
        {
            if (collision.TryGetComponent<IAttackEffect>(out var effect))
            {
                if (effect == null)
                {
                    Debug.LogError("AttackEffect component is missing on the collided object!");
                }

                TakeDamage(effect.Damage);
                effect.HitTarget(gameObject);
            }
        }
    }

    // 防御力を考慮してダメージを反映する
    public void TakeDamage(int damage)
    {
        int effectiveDamage = Mathf.Max(damage - statusCtrl.DefensePoint / 2, 0);
        life -= effectiveDamage;

        if (IsDead())
        {
            StartCoroutine(damageEffect.PlayDeadEffect(gameObject));
        }
        else
        {
            StartCoroutine(damageEffect.PlayEffect(gameObject, life, statusCtrl.HealthPoint));
        }
    }

    // HPが0以下なら死亡
    public bool IsDead()
    {
        return life <= 0;
    }
}
