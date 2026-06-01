using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class HpBarUI : MonoBehaviour
{
    [Header("HpバーのUIドキュメント")]
    [SerializeField] UIDocument hpBar;

    VisualElement slider;
    float maxWidth;

    void Start()
    {
        slider = hpBar.rootVisualElement.Query<VisualElement>("Slider").First();

        maxWidth = slider.resolvedStyle.maxWidth.value;
        slider.style.width = maxWidth;
    }

    public void SetHp(float hp, float maxHp)
    {
        float ratio = hp / maxHp;
        slider.style.width = maxWidth * ratio;
    }
}
