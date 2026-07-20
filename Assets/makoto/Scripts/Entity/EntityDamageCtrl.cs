using UnityEngine;

public class EntityDamageCtrl : MonoBehaviour
{
    // ステータスのデータ
    [SerializeField] EntityStatus entityStatus;
    [SerializeField] Animator animator;

    // 防御力を考慮してダメージを反映する
    public void TakeDamage(int damage)
    {
        int effectiveDamage = Mathf.Max(damage - entityStatus.VIT / 4, 0);
        entityStatus.life -= effectiveDamage;
    }

    public void DamageAnim()
    {
        animator.Play("EntityAnimDamaged", 1);
    }

    public void DeadAnim()
    {
        animator.Play("EntityAnimDamaged", 1);
    }

    // HPが0以下なら死亡
    public bool IsDead()
    {
        return entityStatus.life <= 0;
    }
}
