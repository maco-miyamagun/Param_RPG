using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // 追従対象のTransform。Inspectorから設定可能。
    // 未設定の場合はPlayerタグのオブジェクトを自動で検索します。
    [SerializeField]
    private Transform target;

    private void Awake()
    {
        if (target == null)
        {
            var playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                target = playerObj.transform;
            }
            else
            {
                Debug.LogWarning("MoveCamera: Playerタグのオブジェクトが見つかりません。カメラが追従しません。", this);
            }
        }
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        // プレイヤーのX,Y位置をそのままカメラの中心にする
        transform.position = new Vector3(
            target.position.x,
            target.position.y,
            transform.position.z
        );
    }
}
