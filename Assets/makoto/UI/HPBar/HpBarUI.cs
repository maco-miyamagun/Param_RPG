using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class HpBarUI : MonoBehaviour
{
    [Header("HpバーのUIドキュメント")]
    [SerializeField] UIDocument hpBar;

    VisualElement slider;
    VisualElement follow_slider;

    [SerializeField] EntityStatus entityStatus;

    float previous_ratio = 0f;

    IEnumerator corHPAnim;

    void Start()
    {
        slider = hpBar.rootVisualElement.Q<VisualElement>("HPBar");
        follow_slider = hpBar.rootVisualElement.Q<VisualElement>("HPBarEffect");

        follow_slider.style.width = Length.Percent(0);

        previous_ratio = entityStatus.life / entityStatus.HIT_POINT;
    }

    void FixedUpdate()
    {
        SetHp(entityStatus.life, entityStatus.HIT_POINT);
    }

    public void SetHp(float hp, float maxHp)
    {
        if (slider == null || hp <= 0f)
        {
            slider.style.width = Length.Percent(0.1f);
            return;
        }

        float ratio = hp / maxHp;
        slider.style.width = Length.Percent(ratio * 100.0f);

        if (previous_ratio != ratio)
        {
            if (corHPAnim != null)
            {
                StopCoroutine(corHPAnim);
            }

            follow_slider.style.width = Length.Percent((previous_ratio - ratio) * 100f);

            corHPAnim = HPAnim(previous_ratio - ratio);
            StartCoroutine(corHPAnim);
        }

        previous_ratio = ratio;
    }

    IEnumerator HPAnim(float ratio)
    {
        const float TIME_LIMIT = 0.5f;
        const float TIME_INCREMENT = 0.01f;

        for (float t = 0; t <= TIME_LIMIT; t += TIME_INCREMENT)
        {
            yield return new WaitForSeconds(TIME_INCREMENT);

            float result_ratio = Mathf.Lerp(ratio, 0, t * 2);
            follow_slider.style.width = Length.Percent(result_ratio * 100f);
        }
    }
}
