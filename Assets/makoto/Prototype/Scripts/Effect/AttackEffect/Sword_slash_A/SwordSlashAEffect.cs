using System;
using System.Collections;
using UnityEngine;

public class SwordSlashAEffect : MonoBehaviour, IAttackEffect
{
    public int Damage { get; private set; } = 2;
    [SerializeField] GameObject effectPrefab;
    [SerializeField] float startAngleZ = -70f;
    [SerializeField] float endAngleZ = 70f;
    [SerializeField] float durationSeconds = 0.01f;

    GameObject effect_instance;

    public void HitTarget(GameObject target)
    {
        if (effect_instance == null) return;

        Destroy(effect_instance); // ヒット時にエフェクトを消す
        effect_instance = null;
    }

    IEnumerator IAttackEffect.PlayEffect(GameObject _from)
    {
        if (effect_instance != null)
        {
            Destroy(effect_instance);
            effect_instance = null;
        }

        effect_instance = Instantiate(effectPrefab, transform.position, Quaternion.identity, transform);

        // Euler角(度)で -70 → +70 を確実に回す
        float safeDuration = Mathf.Max(0f, durationSeconds);
        if (safeDuration <= 0f)
        {
            effect_instance.transform.localRotation = Quaternion.Euler(0f, 0f, endAngleZ);
        }
        else
        {
            float elapsed = 0f;
            while (effect_instance != null && elapsed < safeDuration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / safeDuration);
                float currentAngleZ = Mathf.Lerp(startAngleZ, endAngleZ, t);
                effect_instance.transform.localRotation = Quaternion.Euler(0f, 0f, currentAngleZ);
                yield return null;
            }

            if (effect_instance != null)
            {
                effect_instance.transform.localRotation = Quaternion.Euler(0f, 0f, endAngleZ);
            }
        }

        if (effect_instance != null)
        {
            Destroy(effect_instance);
            effect_instance = null;
        }
    }
}
