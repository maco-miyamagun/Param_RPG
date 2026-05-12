using System.Collections;
using UnityEngine;

[RequireComponent(typeof(StatusCtrl))]
public class AttackCtrl : MonoBehaviour
{
    StatusCtrl statusCtrl;
    public GameObject[] effectPrefabs;

    public bool AttackEnabled = true;

    public string attackEffect_tag_name;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        statusCtrl = GetComponent<StatusCtrl>();
    }

    public IEnumerator AttackCooldown()
    {
        if (!AttackEnabled) yield break;

        AttackEnabled = false;
        yield return new WaitForSeconds(CalculateCooldown()); // 攻撃のクールダウン時間
        AttackEnabled = true;
    }

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

    public int CalculateDamage(int effect_damage)
    {
        return statusCtrl.Strength / 2 * effect_damage; // ダメージ計算の例
    }

    public float CalculateCooldown()
    {
        return Mathf.Max(0.2f, 2 - statusCtrl.Agility / 10); // クールダウン時間の例
    }
}
