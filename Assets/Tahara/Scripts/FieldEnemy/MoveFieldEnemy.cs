using UnityEngine;

public class MoveFieldEnemy : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private float speed = 2f;           // 移動速度
    [Header("移動範囲設定")]
    [SerializeField] private float moveRadius = 3f;       // 初期位置から移動できる範囲の半径
    [Header("目標地点到達判定設定")]
    [SerializeField] private float targetTolerance = 0.1f; // 目標地点到達判定の許容距離
    [Header("停止設定")]
    [SerializeField] private float waitTime = 1f;          // 目標地点到達後に停止する時間（秒）

    private Vector3 originPosition; // 初期位置（移動範囲の中心）
    private Vector3 targetPosition; // 現在の移動目標地点
    private bool isWaiting;         // 停止中フラグ
    private float waitTimer;        // 残り停止時間

    private void Start()
    {
        // 開始時に初期位置を記録し、最初の目標地点を設定する
        originPosition = transform.position;
        SetNewTargetPosition();
    }

    private void Update()
    {
        // 停止中なら時間を進める
        if (isWaiting)
        {
            UpdateWait();
            return;
        }

        // フレームごとに目標地点へ移動する
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        Vector3 direction = targetPosition - transform.position;
        direction.z = 0f; // Z軸方向の奥行き差は無視してXY平面で移動する

        if (direction.sqrMagnitude <= targetTolerance * targetTolerance)
        {
            // 目標に到達したら停止状態に入り、停止後に次の目標を設定する
            StartWaiting();
            return;
        }

        transform.position += direction.normalized * speed * Time.deltaTime;
    }

    private void SetNewTargetPosition()
    {
        // 初期位置を中心に、指定した半径内のランダム位置を目標にする
        Vector2 randomOffset = Random.insideUnitCircle * moveRadius;
        targetPosition = originPosition + new Vector3(randomOffset.x, randomOffset.y, 0f);
    }

    private void StartWaiting()
    {
        isWaiting = true;
        waitTimer = waitTime;
    }

    private void UpdateWait()
    {
        waitTimer -= Time.deltaTime;
        if (waitTimer <= 0f)
        {
            isWaiting = false;
            SetNewTargetPosition();
        }
    }

    private void OnDrawGizmosSelected()
    {
        // インスペクタで選択中に移動範囲を可視化する
        Vector3 center = Application.isPlaying ? originPosition : transform.position;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(center, moveRadius);
    }
}
