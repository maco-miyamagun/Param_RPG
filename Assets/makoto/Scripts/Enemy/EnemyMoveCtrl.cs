using UnityEngine;

public class EnemyMoveCtrl : MonoBehaviour
{
    // ステータス
    [SerializeField] EntityStatus entityStatus;

    // 物理移動に使う2Dリジッドボディ
    [SerializeField] Rigidbody2D rb;

    [SerializeField] GameObject fire_core; // 発射コア

    Rigidbody2D player_transform;

    public bool is_skip_move = false;

    void Awake()
    {
        player_transform = FindObjectsByType<PlayerCtrl>(FindObjectsSortMode.None)[0].GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (is_skip_move) return;

        MovePlayer();
        Direct(GetPlayerDirection());
    }

    // 対象に向かって移動させる
    public void MovePlayer()
    {
        rb.position = Vector2.Lerp(rb.position, player_transform.position, entityStatus.AGI / 5 * Time.fixedDeltaTime);
    }

    // 対象との距離を測る
    public float GetPlayerDistance()
    {
        return Vector2.Distance(player_transform.position, rb.position);
    }

    // 対象との向きを正規化したベクトルで表す
    public Vector2 GetPlayerDirection()
    {
        return (player_transform.position - rb.position).normalized;
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

        if (Mathf.Abs(moveInput.x) < Mathf.Abs(moveInput.y))
        {
            return;
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
}
