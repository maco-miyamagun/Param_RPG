using System.Collections;
using UnityEngine;

public class OneHandedSword : MonoBehaviour
{
    // 回転開始角度
    [Header("回転開始角度")]
    [SerializeField] float startAngleZ = -45f;
    // 回転終了角度
    [Header("回転終了角度")]
    [SerializeField] float endAngleZ = 45f;
    // 回転にかける時間
    [Header("回転にかける時間")]
    [SerializeField] float durationSeconds = 0.01f;

    IEnumerator Start()
    {
        // Euler角(度)で -70 → +70 を確実に回す
        float safeDuration = Mathf.Max(0f, durationSeconds);
        if (safeDuration <= 0f)
        {
            transform.parent.transform.localRotation = Quaternion.Euler(0f, 0f, endAngleZ);
        }
        else
        {
            float elapsed = 0f;
            while (elapsed < safeDuration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / safeDuration);
                float currentAngleZ = Mathf.Lerp(startAngleZ, endAngleZ, t);
                transform.parent.transform.localRotation = Quaternion.Euler(0f, 0f, currentAngleZ);
                yield return null;
            }
        }

        Destroy(this.transform.parent.gameObject);
    }
}
