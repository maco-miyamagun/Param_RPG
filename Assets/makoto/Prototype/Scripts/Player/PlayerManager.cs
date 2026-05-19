using UnityEngine;
using UnityEngine.InputSystem;

// 入力と移動/攻撃の仲介を行う
public class PlayerManager : MonoBehaviour
{
    // 移動処理
    PlayerMoveCtrl playerMoveCtrl;
    // 攻撃処理
    AttackCtrl attackCtrl;

    // 直近の移動入力
    Vector2 moveInput;

    // 入力システムから移動入力を受け取る
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    // 入力システムから攻撃入力を受け取る
    public void OnAttack()
    {
        if (attackCtrl == null) return;

        attackCtrl.FireAttack(0);
    }

    // 必須コンポーネントを取得して初期化する
    void Start()
    {
        playerMoveCtrl = GetComponent<PlayerMoveCtrl>();
        attackCtrl = GetComponent<AttackCtrl>();

        if (playerMoveCtrl == null)
        {
            Debug.Log("PlayerMoveCtrl component is missing!");
        }
        if (attackCtrl == null)
        {
            Debug.Log("AttackCtrl component is missing!");
            return;
        }

        // 攻撃エフェクトに付与するタグを指定
        attackCtrl.attackEffect_tag_name = "PlayerAttack";
    }

    // 物理フレームで移動処理を行う
    void FixedUpdate()
    {
        if (playerMoveCtrl == null) return;

        playerMoveCtrl.Move(moveInput);
    }
}
