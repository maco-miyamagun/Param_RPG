using UnityEngine;
using System.Collections;

// プレイヤー被ダメージ時のHP表示更新
public class PlayerDamageEffect : MonoBehaviour, IDamageEffect
{
    // HPバーUI
    [SerializeField] HpBarUI hpBar;

    // ダメージ時の演出
    public IEnumerator PlayEffect(GameObject _from, float life, float maxLife)
    {
        hpBar.SetHp(life, maxLife);
        yield return new WaitForSeconds(0.5f);
    }

    // 死亡時の演出
    public IEnumerator PlayDeadEffect(GameObject _from)
    {
        hpBar.SetHp(0f, 100f);
        Destroy(this.gameObject);
        yield return null;
    }
}
