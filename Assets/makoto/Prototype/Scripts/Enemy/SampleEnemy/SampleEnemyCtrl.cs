using System.Collections;
using UnityEngine;

public class SampleEnemyCtrl : MonoBehaviour
{
    StatusCtrl statusCtrl;
    AttackCtrl attackCtrl;

    void Awake()
    {
        statusCtrl = GetComponent<StatusCtrl>();
        attackCtrl = GetComponent<AttackCtrl>();
        if (statusCtrl != null)
        {
            statusCtrl.SetStatusFromJson();
            statusCtrl.SetStatusForLevel(1);
        }

        if (attackCtrl != null)
        {
            attackCtrl.attackEffect_tag_name = "EnemyAttack";
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(MoveLogic());
    }

    IEnumerator MoveLogic()
    {
        while (true)
        {
            yield return StartCoroutine(Move(new Vector2(3f, 0f), 1f));
            attackCtrl.FireAttack(0);
            yield return StartCoroutine(Move(new Vector2(-6f, 0f), 1f));
            attackCtrl.FireAttack(0);
            yield return StartCoroutine(Move(new Vector2(3f, 0f), 1f));
            attackCtrl.FireAttack(0);
        }
    }

    IEnumerator Move(Vector2 move_delta, float moveTime)
    {
        Vector2 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < moveTime)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveTime);
            transform.position = Vector2.Lerp(startPosition, startPosition + move_delta, t);
            yield return null;
        }

        transform.position = startPosition + move_delta;
    }
}
