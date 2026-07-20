using System.Collections;
using UnityEngine;

public class BowWeapon : MonoBehaviour
{
    [SerializeField] float durationSeconds = 0.3f;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(durationSeconds);

        Destroy(this.transform.gameObject);
    }
}
