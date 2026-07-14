using System.Collections;
using UnityEngine;

public class GoblinCtrl : MonoBehaviour
{
    [SerializeField] EntityStatus status;
    [SerializeField] WeaponCtrl weaponCtrl;
    [SerializeField] EntityDamageCtrl damageCtrl;

    BattleManage battleManage;

    [SerializeField] EnemyMoveCtrl move_ctrl;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        battleManage = FindObjectsByType<BattleManage>(FindObjectsSortMode.None)[0];

        StartCoroutine(AILogic());
    }

    void FixedUpdate()
    {
        if (damageCtrl.IsDead())
        {
            battleManage.WinPlayer();
            Destroy(this.gameObject);
        }
    }

    IEnumerator AILogic()
    {
        while (true)
        {
            move_ctrl.is_skip_move = false;
            yield return ChasingPlayer();
            move_ctrl.is_skip_move = true;

            yield return new WaitForSeconds(1f);
            weaponCtrl.OnAttack();
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator ChasingPlayer()
    {
        const float LIMIT_TIME = 4f;
        const float DISTANCE = 1.5f;
        const float TIME_INCREMENT = 0.1f;

        for (float t = 0; t < LIMIT_TIME; t += TIME_INCREMENT)
        {
            if (move_ctrl.GetPlayerDistance() <= DISTANCE)
            {
                yield break;
            }

            yield return new WaitForSeconds(TIME_INCREMENT);
        }
    }
}
