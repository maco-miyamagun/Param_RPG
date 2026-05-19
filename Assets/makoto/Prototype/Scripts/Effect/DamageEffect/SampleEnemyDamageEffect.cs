using UnityEngine;
using System.Collections;

// サンプル敵の被ダメージ演出
public class SampleEnemyDamageEffect : MonoBehaviour, IDamageEffect
{
    // ダメージ時の演出
    public IEnumerator PlayEffect(GameObject _from, float life, float maxLife)
    {
        yield return new WaitForSeconds(0.5f);
    }

    // 死亡時の演出
    public IEnumerator PlayDeadEffect(GameObject _from)
    {
        Destroy(this.gameObject);
        yield return null;
    }
}
