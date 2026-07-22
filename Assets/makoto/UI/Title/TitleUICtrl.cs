using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class TitleUICtrl : MonoBehaviour
{
    [SerializeField] UIDocument uIDocument;

    public void OnGameStart(InputValue inputValue)
    {
        SceneManager.LoadScene("GameData");
    }
}
