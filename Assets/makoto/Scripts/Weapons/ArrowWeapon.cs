using System.Collections;
using UnityEngine;

public class ArrowWeapon : MonoBehaviour
{
    [SerializeField] float endPosDeltaY = 10f;
    [SerializeField] float durationSeconds = 2f;

    IEnumerator Start()
    {
        float safeDuration = Mathf.Max(0f, durationSeconds);
        Vector3 old_pos = transform.localPosition;

        float elapsed = 0f;
        while (elapsed < safeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / safeDuration);
            float currentPosDeltaY = Mathf.Lerp(0, endPosDeltaY, t);
            transform.parent.transform.localPosition = old_pos + new Vector3(0, currentPosDeltaY, 0);
            yield return null;
        }

        Destroy(this.transform.parent.gameObject);
    }
}
