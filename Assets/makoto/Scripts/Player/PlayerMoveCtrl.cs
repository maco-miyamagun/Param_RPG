using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveCtrl : MonoBehaviour
{
    // ステータス
    [SerializeField] EntityStatus entityStatus;

    // 物理移動に使う2Dリジッドボディ
    [SerializeField] Rigidbody2D rb;

    Vector2 moveInput; // 移動量

    [SerializeField] GameObject fire_core; // 発射コア

    bool stamina_finished = false; // スタミナが尽きているか

    // 入力システムから移動入力を受け取る
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        Move(moveInput);
        Direct(moveInput);
    }

    // 入力方向に応じて移動させる
    public void Move(Vector2 moveInput)
    {
        Vector2 moveDirection = new(moveInput.x, moveInput.y);

        float moveSpeed = CalclateMoveSpeed(moveDirection.magnitude != 0);

        rb.MovePosition(rb.position + moveSpeed * Time.deltaTime * moveDirection);
    }

    // 方向を変える
    public void Direct(Vector2 moveInput)
    {
        if (moveInput.y > 0)
        {
            fire_core.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (moveInput.y < 0)
        {
            fire_core.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }

        if (moveInput.x > 0)
        {
            fire_core.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        }
        else if (moveInput.x < 0)
        {
            fire_core.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
    }

    // 移動スピードを決める（スタミナ管理）
    public float CalclateMoveSpeed(bool isMoving)
    {
        const float DECREMENT_STAMINA = 20f;
        const float INCREMENT_STAMINA = 3f;

        if (entityStatus.stamina <= 0)
        {
            stamina_finished = true;
        }

        if (stamina_finished)
        {
            entityStatus.stamina += Time.deltaTime * INCREMENT_STAMINA;

            if (entityStatus.stamina >= entityStatus.STAMINA_MAX)
            {
                entityStatus.stamina = entityStatus.STAMINA_MAX;
                stamina_finished = false;
            }

            return entityStatus.AGI * 0.2f;
        }

        if (!Keyboard.current.shiftKey.isPressed && entityStatus.stamina <= entityStatus.STAMINA_MAX)
        {
            entityStatus.stamina += Time.deltaTime * INCREMENT_STAMINA;

            if (entityStatus.stamina >= entityStatus.STAMINA_MAX)
            {
                entityStatus.stamina = entityStatus.STAMINA_MAX;
            }

            return entityStatus.AGI;
        }

        if (Keyboard.current.shiftKey.isPressed && isMoving)
        {
            entityStatus.stamina -= Time.deltaTime * DECREMENT_STAMINA;
        }

        return entityStatus.AGI * 1.7f;
    }
}
