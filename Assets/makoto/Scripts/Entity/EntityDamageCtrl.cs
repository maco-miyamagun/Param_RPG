using UnityEngine;

public class EntityDamageCtrl : MonoBehaviour
{
    // ステータスのデータ
    [SerializeField] EntityStatus entityStatus;
    [SerializeField] Animator animator;

    public bool is_player = false;

    // 防御力を考慮してダメージを反映する
    public void TakeDamage(int damage)
    {
        int effectiveDamage = Mathf.Max(damage - entityStatus.VIT / 4, 0);
        entityStatus.life -= effectiveDamage;

        if (is_player)
        {
            FindObjectsByType<GameMusicController>(FindObjectsSortMode.None)[0].PlayDamageSE();
        }
        else
        {
            FindObjectsByType<GameMusicController>(FindObjectsSortMode.None)[0].PlayAttackSE();
        }
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
