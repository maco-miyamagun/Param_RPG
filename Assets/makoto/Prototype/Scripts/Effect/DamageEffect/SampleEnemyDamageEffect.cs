using UnityEngine;
using System.Collections;

// サンプル敵の被ダメージ演出
public class SampleEnemyDamageEffect : MonoBehaviour, IDamageEffect
{
    // 敵マネージャーの参照
    EnemyManager enemyManager;

    // 初期化
    void Awake()
    {
        enemyManager = FindFirstObjectByType<EnemyManager>();
    }

    // ダメージ時の演出
    public IEnumerator PlayEffect(GameObject _from, float life, float maxLife)
    {
        yield return new WaitForSeconds(0.5f);
    }

    // 死亡時の演出
    public IEnumerator PlayDeadEffect(GameObject _from)
    {
        enemyManager.WinPlayer();

        Destroy(this.gameObject);
        yield return null;
    }
}
