using UnityEngine;

/// <summary>
/// ボタンが押されたときに、指定されたキャンバスを非アクティブにするスクリプト
/// </summary>
public class BackButton : MonoBehaviour
{
    // 非表示にしたいキャンバスをInspectorで指定する
    [Header("指定されたキャンバスを非アクティブにする")]
    [SerializeField]
    private Canvas targetCanvas;

    
    /// <summary>
    /// ボタンのOnClickから呼び出す
    /// </summary>
    public void OnBackButtonPressed()
    {
        if (targetCanvas != null)
        {
            // 指定したキャンバスを非表示にする
            targetCanvas.gameObject.SetActive(false);
        }
    }
}
