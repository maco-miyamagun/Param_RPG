using System.Collections;
using UnityEngine;

[RequireComponent(typeof(StatusCtrl))]
public class AttackCtrl : MonoBehaviour
{
    StatusCtrl statusCtrl;
    public IAttackEffect attackEffect;

    public bool AttackEnabled = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        statusCtrl = GetComponent<StatusCtrl>();
        attackEffect = GetComponent<IAttackEffect>();
    }

    public IEnumerator AttackCooldown()
    {
        if (!AttackEnabled) yield break;

        AttackEnabled = false;
        yield return new WaitForSeconds(CalculateCooldown()); // 攻撃のクールダウン時間
        AttackEnabled = true;
    }

    public void FireAttack()
    {
        if (!AttackEnabled) return;
        attackEffect.PlayEffect(gameObject);

        StartCoroutine(AttackCooldown());
    }

    public int CalculateDamage()
    {
        return statusCtrl.Strength / 2 * attackEffect.Damage; // ダメージ計算の例
    }

    public float CalculateCooldown()
    {
        return Mathf.Max(0.2f, 2 - statusCtrl.Agility / 10); // クールダウン時間の例
    }
}
