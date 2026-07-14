using UnityEngine;
using UnityEngine.UIElements;

public class HpBarUI : MonoBehaviour
{
    [Header("HpバーのUIドキュメント")]
    [SerializeField] UIDocument hpBar;

    VisualElement slider;

    [SerializeField] EntityStatus entityStatus;

    void Start()
    {
        slider = hpBar.rootVisualElement.Q<VisualElement>("HPBar");
    }

    void FixedUpdate()
    {
        SetHp(entityStatus.life, entityStatus.HIT_POINT);
    }

    public void SetHp(float hp, float maxHp)
    {
        if (slider == null || maxHp <= 0f)
            return;

        float ratio = hp / maxHp;
        slider.style.width = Length.Percent(ratio * 80.0f);
    }
}
