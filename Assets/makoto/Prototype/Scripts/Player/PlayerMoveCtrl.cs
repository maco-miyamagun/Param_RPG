using UnityEngine;

// プレイヤーの移動処理を担当する
[RequireComponent(typeof(StatusCtrl))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMoveCtrl : MonoBehaviour
{
    // 移動速度計算に使うステータス
    StatusCtrl statusCtrl;
    // 物理移動に使う2Dリジッドボディ
    Rigidbody2D rb;

    // 必須コンポーネントを取得する
    void Start()
    {
        statusCtrl = GetComponent<StatusCtrl>();
        rb = GetComponent<Rigidbody2D>();
        if (statusCtrl == null)
        {
            Debug.LogError("StatusCtrl component is missing!");
        }
    }

    // 入力方向に応じて移動させる
    public void Move(Vector2 moveInput)
    {
        Vector2 moveDirection = new(moveInput.x, moveInput.y);

        float moveSpeed = CalclateMoveSpeed();

        rb.MovePosition(rb.position + moveSpeed * Time.deltaTime * moveDirection);
    }

    // 敏捷に応じた移動速度を計算する
    float CalclateMoveSpeed()
    {
        return statusCtrl.Agility / 5f;
    }
}
