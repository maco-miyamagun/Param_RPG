using System.Collections;
using UnityEngine;

public interface IDamageEffect
{
    IEnumerator PlayEffect(GameObject _from, float life, float maxLife);
    IEnumerator PlayDeadEffect(GameObject _from);
}
