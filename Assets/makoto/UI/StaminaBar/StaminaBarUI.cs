using UnityEngine;
using UnityEngine.UIElements;

public class StaminaBarUI : MonoBehaviour
{
    [Header("スタミナバーのUIドキュメント")]
    [SerializeField] UIDocument staminaBar;

    VisualElement slider;

    [SerializeField] EntityStatus entityStatus;

    void Start()
    {
        slider = staminaBar.rootVisualElement.Q<VisualElement>("HPBar");
    }

    void FixedUpdate()
    {
        SetUI(entityStatus.stamina, entityStatus.STAMINA_MAX);
    }

    public void SetUI(float stamina, float maxStamina)
    {
        if (slider == null || stamina <= 0f)
            return;

        float ratio = stamina / maxStamina;
        slider.style.width = Length.Percent(ratio * 80.0f);
    }
}
