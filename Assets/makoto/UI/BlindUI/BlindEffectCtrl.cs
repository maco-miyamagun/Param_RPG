using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class BlindEffectCtrl : MonoBehaviour
{
    [SerializeField]
    UIDocument uIDocument;

    VisualElement blind_elm;

    [SerializeField] PlayerInput playerInput;

    void Awake()
    {
        blind_elm = uIDocument.rootVisualElement.Q<VisualElement>("Blind");
    }

    public IEnumerator BlindShow(bool flag)
    {
        const float LIMIT_TIME = 0.5f;
        const float TIME_INCREMENT = 0.01f;

        Color color;

        for (float t = 0; t < LIMIT_TIME; t += TIME_INCREMENT)
        {
            if (flag)
            {
                color = Color.Lerp(new(0, 0, 0, 1), new(0, 0, 0, 0), t / LIMIT_TIME);
            }
            else
            {
                color = Color.Lerp(new(0, 0, 0, 0), new(0, 0, 0, 1), t / LIMIT_TIME);
            }

            blind_elm.style.backgroundColor = new StyleColor(color);

            yield return new WaitForSeconds(TIME_INCREMENT);
        }

        if (flag)
        {
            color = new(0, 0, 0, 0);
        }
        else
        {
            color = new(0, 0, 0, 1);
        }

        blind_elm.style.backgroundColor = new StyleColor(color);
    }

    public void TogglePlayerInput(bool flag)
    {
        if (flag)
        {
            playerInput.SwitchCurrentActionMap("main");
        }
        else
        {
            playerInput.SwitchCurrentActionMap("message_event");
        }
    }

    public void ToggleShowUI(bool flag)
    {
        if (flag)
        {
            uIDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        }
        else
        {
            uIDocument.rootVisualElement.style.display = DisplayStyle.None;
        }
    }
}
