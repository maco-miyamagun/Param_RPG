using UnityEngine;

public class Weapon : MonoBehaviour
{
    // 武器のステータスのスクリプト
    [SerializeField] SkillStatus skillStatus;
    // 攻撃関連のスクリプト
    EntityAttackCtrl attackCtrl;

    [SerializeField] WeaponOwner weapon_owner;

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
        string TARGET_TAG_NAME = weapon_owner == WeaponOwner.Player ? "Enemy" : "Player";

        if (collision.CompareTag(TARGET_TAG_NAME))
        {
            if (!collision.gameObject.TryGetComponent<EntityDamageCtrl>(out var damage_ctrl)) return;

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