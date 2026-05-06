using UnityEngine;

[RequireComponent(typeof(StatusCtrl))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMoveCtrl : MonoBehaviour
{
    StatusCtrl statusCtrl;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        statusCtrl = GetComponent<StatusCtrl>();
        rb = GetComponent<Rigidbody2D>();
        if (statusCtrl == null)
        {
            Debug.LogError("StatusCtrl component is missing!");
        }
    }

    public void Move(Vector2 moveInput)
    {
        Vector2 moveDirection = new(moveInput.x, moveInput.y);

        float moveSpeed = CalclateMoveSpeed();

        rb.MovePosition(rb.position + moveSpeed * Time.deltaTime * moveDirection);
    }

    float CalclateMoveSpeed()
    {
        return statusCtrl.Agility / 5f;
    }
}
