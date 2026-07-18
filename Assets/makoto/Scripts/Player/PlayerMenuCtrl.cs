using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMenuCtrl : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;

    public void TogglePauseMenu(bool flag)
    {
        Time.timeScale = flag ? 0f : 1;

        if (flag)
        {
            playerInput.SwitchCurrentActionMap("menu");
        }
        else
        {
            playerInput.SwitchCurrentActionMap("main");
        }
    }

    public void OnMenu()
    {
        TogglePauseMenu(true);
    }

    public void OnCancel()
    {
        TogglePauseMenu(false);
    }
}
