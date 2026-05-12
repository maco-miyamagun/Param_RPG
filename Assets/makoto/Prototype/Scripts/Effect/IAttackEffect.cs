using System.Collections;
using UnityEngine;

public interface IAttackEffect
{
    public int Damage { get; }

    void HitTarget(GameObject _from);
    IEnumerator PlayEffect(GameObject _from);
}
