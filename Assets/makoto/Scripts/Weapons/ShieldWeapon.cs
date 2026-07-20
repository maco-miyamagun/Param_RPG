using System.Collections;
using UnityEngine;

public class ShieldWeapon : MonoBehaviour
{
    [SerializeField] float durationSeconds = 1.5f;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(durationSeconds);

        Destroy(this.transform.parent.gameObject);
    }
}
