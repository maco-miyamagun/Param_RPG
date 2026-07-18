using UnityEngine;
using UnityEngine.UIElements;

public class BattleLoseUICtrl : MonoBehaviour
{
    [SerializeField] UIDocument uIDocument;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetVisible(false);
    }

    public void SetVisible(bool flag)
    {
        uIDocument.rootVisualElement.style.visibility = flag ? Visibility.Visible : Visibility.Hidden;
    }
}
