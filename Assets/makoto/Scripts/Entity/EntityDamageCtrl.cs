using UnityEngine;

public class EntityDamageCtrl : MonoBehaviour
{
    // ステータスのデータ
    [SerializeField] EntityStatus entityStatus;

    // 防御力を考慮してダメージを反映する
    public void TakeDamage(int damage)
    {
        int effectiveDamage = Mathf.Max(damage, 0);
        entityStatus.life -= effectiveDamage;
    }

    // HPが0以下なら死亡
    public bool IsDead()
    {
        return entityStatus.life <= 0;
    }
}
