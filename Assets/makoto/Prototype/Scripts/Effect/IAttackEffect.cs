using System.Collections;
using UnityEngine;

public interface IAttackEffect
{
    public int BaseDamage { get; set; }
    public int Damage { get; set; }

    void HitTarget(GameObject _from);
    IEnumerator PlayEffect(GameObject _from);
}
