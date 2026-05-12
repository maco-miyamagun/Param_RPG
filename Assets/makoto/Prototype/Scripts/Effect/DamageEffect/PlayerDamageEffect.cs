using UnityEngine;
using System.Collections;

public class PlayerDamageEffect : MonoBehaviour, IDamageEffect
{
    [SerializeField] HpBarUI hpBar;

    public IEnumerator PlayEffect(GameObject _from, float life, float maxLife)
    {
        hpBar.SetHp(life, maxLife);
        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator PlayDeadEffect(GameObject _from)
    {
        throw new System.NotImplementedException();
    }
}
