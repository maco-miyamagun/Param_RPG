using UnityEngine;
using System.Collections;

public class SampleEnemyDamageEffect : MonoBehaviour, IDamageEffect
{
    public IEnumerator PlayEffect(GameObject _from, float life, float maxLife)
    {
        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator PlayDeadEffect(GameObject _from)
    {
        Destroy(this.gameObject);
        yield return null;
    }
}
