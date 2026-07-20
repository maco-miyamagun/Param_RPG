using UnityEngine;

public class Weapon : MonoBehaviour
{
    // 武器のステータスのスクリプト
    [SerializeField] SkillStatus skillStatus;
    // 攻撃関連のスクリプト
    EntityAttackCtrl attackCtrl;

    [SerializeField] WeaponOwner weapon_owner;

    [SerializeField] bool is_defense;

    // 必要なスクリプトを取ってくる
    void Start()
    {
        if (weapon_owner == WeaponOwner.Player)
        {
            attackCtrl = FindAnyObjectByType<PlayerCtrl>().GetComponent<EntityAttackCtrl>();
        }
        else
        {
            attackCtrl = FindAnyObjectByType<EnemyMoveCtrl>().GetComponent<EntityAttackCtrl>();
        }


        // 攻撃にクールダウンをかける
        attackCtrl.AttackCooldown(skillStatus.base_cooldown_time);
    }

    // 敵に当たったら、ダメージ処理をする
    public void OnTriggerEnter2D(Collider2D collision)
    {
        WeaponOwner TARGET_TAG_NAME = weapon_owner == WeaponOwner.Player ? WeaponOwner.Enemy : WeaponOwner.Player;

        if (is_defense)
        {
            if (collision.CompareTag("Weapon"))
            {
                Destroy(collision.transform.parent.gameObject);
                Destroy(this.transform.parent.gameObject);
            }
            return;
        }

        if (collision.CompareTag(TARGET_TAG_NAME.ToString()))
        {
            if (!collision.gameObject.TryGetComponent<EntityDamageCtrl>(out var damage_ctrl)) return;

            damage_ctrl.DamageAnim();
            damage_ctrl.TakeDamage(attackCtrl.CalculateDamage(skillStatus.base_damage));

            Destroy(this.transform.parent.gameObject);
        }
    }
}

public enum WeaponOwner
{
    Player,
    Enemy,
}