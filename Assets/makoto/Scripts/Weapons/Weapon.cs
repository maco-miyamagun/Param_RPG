using UnityEngine;

public class Weapon : MonoBehaviour
{
    // 武器のステータスのスクリプト
    [SerializeField] SkillStatus skillStatus;

    // 武器管理スクリプト
    WeaponCtrl weaponCtrl;
    // 攻撃関連のスクリプト
    EntityAttackCtrl attackCtrl;

    [SerializeField] string TARGET_TAG_NAME; //攻撃先のタグ

    // 必要なスクリプトを取ってくる
    void Start()
    {
        weaponCtrl = FindAnyObjectByType<WeaponCtrl>();
        attackCtrl = weaponCtrl.GetComponent<EntityAttackCtrl>();

        // 攻撃にクールダウンをかける
        weaponCtrl.AttackCooldown(skillStatus.base_cooldown_time);
    }

    // 敵に当たったら、ダメージ処理をする
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TARGET_TAG_NAME))
        {
            if (!collision.gameObject.TryGetComponent<EntityDamageCtrl>(out var damage_ctrl)) return;

            damage_ctrl.TakeDamage(attackCtrl.CalculateDamage(skillStatus.base_damage));
            Destroy(this.transform.parent.gameObject);
        }
    }
}