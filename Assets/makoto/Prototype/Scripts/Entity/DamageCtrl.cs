using System;
using UnityEngine;

[RequireComponent(typeof(StatusCtrl))]
public class DamageCtrl : MonoBehaviour
{
    StatusCtrl statusCtrl;
    IDamageEffect damageEffect;

    [SerializeField]
    int life = 0;

    [SerializeField] private string attackEffect_tag_name;

    void Start()
    {
        statusCtrl = GetComponent<StatusCtrl>();
        damageEffect = GetComponent<IDamageEffect>();

        if (statusCtrl != null)
        {
            Life = statusCtrl.HealthPoint;
        }
    }

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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(attackEffect_tag_name))
        {
            if (collision.TryGetComponent<AttackCtrl>(out var enemyAttack))
            {
                int damage = enemyAttack.CalculateDamage();
                enemyAttack.attackEffect.HitTarget(gameObject);

                TakeDamage(damage);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        int effectiveDamage = Mathf.Max(damage - statusCtrl.DefensePoint / 2, 0);
        life -= effectiveDamage;

        if (IsDead())
        {
            damageEffect.PlayDeadEffect(gameObject);
        }
        else
        {
            damageEffect.PlayEffect(gameObject, life, statusCtrl.HealthPoint);
        }
    }

    public bool IsDead()
    {
        return life <= 0;
    }
}
