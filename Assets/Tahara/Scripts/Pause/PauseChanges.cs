using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Tabキーで指定したキャンバスのアクティブ/非アクティブを切り替えるスクリプト
/// </summary>
public class PauseChanges : MonoBehaviour
{
    // Inspectorで指定するキャンバス
    
    [Header(("TABキーで表示/非表示を切り替えるキャンバス"))]
    [SerializeField] private Canvas targetCanvas;

    void Start()
    {
        // もしキャンバスが割り当てられていなければエラーを表示
        if (targetCanvas == null)
        {
            Debug.LogError("Canvasが指定されていません");
        }
    }

    void Update()
    {
        // キャンバスが未設定なら処理を中断
        if (targetCanvas == null)
        {
            return;
        }

        // キーボード入力を取得
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return;
        }

        // Tabキーが押されたらキャンバスの表示/非表示を切り替え
        if (keyboard.tabKey.wasPressedThisFrame)
        {
            bool isActive = !targetCanvas.gameObject.activeSelf;
            targetCanvas.gameObject.SetActive(isActive);
            Debug.Log($"キャンバス状態: {(isActive ? "アクティブ" : "非アクティブ")}");
        }
    }
}

