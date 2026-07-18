using UnityEngine;
using UnityEngine.UIElements;

public class BattleClearUICtrl : MonoBehaviour
{
    [SerializeField] UIDocument uIDocument;

    Label level_ui;
    VisualElement exbar_ui;

    [SerializeField] ShareData shareData;

    [SerializeField] PlayerLevelCtrl playerLevelCtrl;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        level_ui = uIDocument.rootVisualElement.Q<Label>("Level");
        exbar_ui = uIDocument.rootVisualElement.Q<VisualElement>("EXPBar");

        SetVisible(false);
    }

    void FixedUpdate()
    {
        SetUI();
    }

    public void SetUI()
    {
        SetEXPBar(shareData.experience, playerLevelCtrl.GetExperienceForNextLevel(shareData.level));
        SetLevelUI(shareData.level);
    }

    public void SetEXPBar(float ex, float maxEX)
    {
        if (exbar_ui == null || maxEX <= 0f)
        {
            Debug.Log(maxEX);

            exbar_ui.style.width = Length.Percent(0.1f);
            return;
        }

        float ratio = ex / maxEX;
        exbar_ui.style.width = Length.Percent(ratio * 100.0f);
    }

    public void SetLevelUI(int level)
    {
        level_ui.text = "LV." + level;
    }

    public void SetVisible(bool flag)
    {
        uIDocument.rootVisualElement.style.visibility = flag ? Visibility.Visible : Visibility.Hidden;
    }
}
