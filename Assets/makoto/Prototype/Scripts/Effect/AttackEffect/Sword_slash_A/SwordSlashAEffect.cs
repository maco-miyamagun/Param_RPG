using System;
using System.Collections;
using UnityEngine;

public class SwordSlashAEffect : MonoBehaviour, IAttackEffect
{
    public int BaseDamage { get; set; } = 2;
    public int Damage { get; set; } = 0;
    [SerializeField] float startAngleZ = -70f;
    [SerializeField] float endAngleZ = 70f;
    [SerializeField] float durationSeconds = 0.01f;

    bool isEffectActive = true;

    public void HitTarget(GameObject target)
    {
        isEffectActive = false;
    }

    IEnumerator IAttackEffect.PlayEffect(GameObject _from)
    {

        // Euler角(度)で -70 → +70 を確実に回す
        float safeDuration = Mathf.Max(0f, durationSeconds);
        if (safeDuration <= 0f)
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, endAngleZ);
        }
        else
        {
            float elapsed = 0f;
            while (elapsed < safeDuration)
            {
                if (!isEffectActive)
                {
                    break;
                }

                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / safeDuration);
                float currentAngleZ = Mathf.Lerp(startAngleZ, endAngleZ, t);
                transform.localRotation = Quaternion.Euler(0f, 0f, currentAngleZ);
                yield return null;
            }
        }

        isEffectActive = false;
        Destroy(this.gameObject);
    }
}
