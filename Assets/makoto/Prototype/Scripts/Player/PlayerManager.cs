using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMoveCtrl))]
[RequireComponent(typeof(AttackCtrl))]
public class PlayerManager : MonoBehaviour
{
    PlayerMoveCtrl playerMoveCtrl;
    AttackCtrl attackCtrl;

    Vector2 moveInput;

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnAttack()
    {
        attackCtrl.FireAttack(0);
    }

    void Start()
    {
        playerMoveCtrl = GetComponent<PlayerMoveCtrl>();
        attackCtrl = GetComponent<AttackCtrl>();

        if (playerMoveCtrl == null)
        {
            Debug.LogError("PlayerMoveCtrl component is missing!");
        }
        if (attackCtrl == null)
        {
            Debug.LogError("AttackCtrl component is missing!");
        }

        attackCtrl.attackEffect_tag_name = "PlayerAttack";
    }

    void FixedUpdate()
    {
        playerMoveCtrl.Move(moveInput);
    }
}
