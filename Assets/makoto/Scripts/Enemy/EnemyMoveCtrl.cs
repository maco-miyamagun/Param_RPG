using System.Collections;
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

    public bool is_reach_move = false;
    Vector2 reach_target;
    [SerializeField] Animator animator;

    void Awake()
    {
        player_transform = FindObjectsByType<PlayerCtrl>(FindObjectsSortMode.None)[0].GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (is_reach_move)
        {
            ReachTarget(reach_target);
            return;
        }

        if (is_skip_move)
        {
            animator.SetBool("is_move", false);
            return;
        }

        animator.SetBool("is_move", true);

        MovePlayer();
        Direct(GetPlayerDirection());
    }

    // 対象に向かって移動させる
    public void MovePlayer()
    {
        rb.position = Vector2.Lerp(rb.position, player_transform.position, entityStatus.AGI / 5 * Time.fixedDeltaTime);
    }

    public void ReachTarget(Vector2 target)
    {
        rb.position = Vector2.Lerp(rb.position, target, entityStatus.AGI / 2 * Time.fixedDeltaTime);
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

    public Vector2 GetPlayerDeltaDistance()
    {
        return player_transform.position - rb.position;
    }

    public Vector2 GetPlayerPos()
    {
        return player_transform.position;
    }

    public void SetReachTarget(Vector2 target)
    {
        reach_target = target;
    }

    // 方向を変える
    public void Direct(Vector2 moveInput)
    {
        if (moveInput == Vector2.zero)
        {
            return;
        }

        float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;

        if ((angle >= 135 && angle <= 180) || (angle <= -135 && angle >= -180))
        {
            animator.SetInteger("move_dir", 0);
        }
        else if ((angle >= 0 && angle <= 45) || (angle <= 0 && angle >= -45))
        {
            animator.SetInteger("move_dir", 1);
        }
        else if (angle >= 45 && angle <= 135)
        {
            animator.SetInteger("move_dir", 2);
        }
        else if (angle >= -135 && angle <= -45)
        {
            animator.SetInteger("move_dir", 3);
        }

        fire_core.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }
}
