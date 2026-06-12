using UnityEngine;

/// <summary>
/// 指定したキャンバスがアクティブならゲームを一時停止し、
/// 非アクティブならゲームを再開するスクリプト
/// </summary>
public class CanvasActiveCheck : MonoBehaviour
{
    // 一時停止状態の判定対象となるキャンバス
    [Header("指定されたキャンバスがアクティブなときにゲームを一時停止")]
    [SerializeField] private Canvas targetCanvas;

    // 前フレームのキャンバスアクティブ状態を保持する変数
    private bool previousActiveState;

    void Start()
    {
        // キャンバスが設定されていなければエラー表示
        if (targetCanvas == null)
        {
            Debug.LogError("Canvasが指定されていません");
            return;
        }

        // 起動時の状態を記録して、最初の時間スケールを設定
        previousActiveState = targetCanvas.gameObject.activeSelf;
        UpdateTimeScale(previousActiveState);
    }

    void Update()
    {
        // Canvasが未設定なら処理を中断
        if (targetCanvas == null)
        {
            return;
        }

        // 現在のアクティブ状態を取得
        bool currentActiveState = targetCanvas.gameObject.activeSelf;
        // 状態が変化した場合のみ時間制御を更新
        if (currentActiveState != previousActiveState)
        {
            previousActiveState = currentActiveState;
            UpdateTimeScale(currentActiveState);
        }
    }

    /// <summary>
    /// キャンバスのアクティブ状態に応じてTime.timeScaleを更新する
    /// </summary>
    /// <param name="canvasIsActive">キャンバスがアクティブかどうか</param>
    private void UpdateTimeScale(bool canvasIsActive)
    {
        if (canvasIsActive)
        {
            // キャンバス表示中はゲームを一時停止
            Time.timeScale = 0f;
            Debug.Log("ゲーム一時停止（Time.timeScale = 0）");
        }
        else
        {
            // キャンバス非表示中はゲームを再開
            Time.timeScale = 1f;
            Debug.Log("ゲーム再開（Time.timeScale = 1）");
        }

        Debug.Log($"キャンバス状態: {(canvasIsActive ? "アクティブ" : "非アクティブ")}");
    }
}

