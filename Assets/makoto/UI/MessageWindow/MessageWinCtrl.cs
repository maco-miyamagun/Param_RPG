using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class MessageWinCtrl : MonoBehaviour
{
    [SerializeField]
    UIDocument uIDocument;

    Label message_text;

    [SerializeField]
    float TEXT_DELAY_TIME;

    [SerializeField]
    float MESSAGE_DELAY_TIME;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        message_text = uIDocument.rootVisualElement.Q<Label>("Message");
        ToggleShowUI(false);
    }

    public IEnumerator ShowMessage(string message)
    {
        string display_text = "";

        foreach (char c in message)
        {
            display_text += c;
            message_text.text = display_text;

            yield return new WaitForSeconds(TEXT_DELAY_TIME);
        }
    }

    public IEnumerator WaitForNextMessage()
    {
        yield return new WaitUntil(() => Keyboard.current.eKey.isPressed);
        yield return new WaitForSeconds(MESSAGE_DELAY_TIME);
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
