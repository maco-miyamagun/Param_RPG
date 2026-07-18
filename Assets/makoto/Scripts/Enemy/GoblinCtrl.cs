using System.Collections;
using System.Linq;
using UnityEngine;

public class GoblinCtrl : MonoBehaviour
{
    [SerializeField] EntityStatus status;
    [SerializeField] WeaponCtrl weaponCtrl;
    [SerializeField] EntityDamageCtrl damageCtrl;

    BattleManage battleManage;

    [SerializeField] EnemyMoveCtrl move_ctrl;

    [SerializeField] Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        battleManage = FindObjectsByType<BattleManage>(FindObjectsSortMode.None)[0];

        StartCoroutine(AILogic());
    }

    IEnumerator AILogic()
    {
        while (true)
        {
            if (damageCtrl.IsDead()) break;
            move_ctrl.Direct(move_ctrl.GetPlayerDirection());
            yield return ChasingPlayer();

            if (damageCtrl.IsDead()) break;
            animator.Play("EntityAnimChargeSkill", 0);
            yield return new WaitForSeconds(GetAnimationClipLength("EntityAnimChargeSkill"));

            if (damageCtrl.IsDead()) break;
            weaponCtrl.OnAttack();
            yield return new WaitForSeconds(1f);
        }

        damageCtrl.DeadAnim();
        yield return new WaitForSeconds(GetAnimationClipLength("EntityAnimDamaged"));

        battleManage.WinPlayer();
        Destroy(this.gameObject);
    }

    IEnumerator ChasingPlayer()
    {
        move_ctrl.is_skip_move = false;

        const float LIMIT_TIME = 4f;
        const float DISTANCE = 2.5f;
        const float TIME_INCREMENT = 0.1f;

        for (float t = 0; t < LIMIT_TIME; t += TIME_INCREMENT)
        {
            if (move_ctrl.GetPlayerDistance() <= DISTANCE)
            {
                break;
            }

            yield return new WaitForSeconds(TIME_INCREMENT);
        }

        move_ctrl.is_skip_move = true;
    }

    float GetAnimationClipLength(string clipName)
    {
        if (animator.runtimeAnimatorController == null) return 0f;

        // アニメーションコントローラーに含まれるすべてのクリップから名前を一致させて検索
        AnimationClip clip = animator.runtimeAnimatorController.animationClips.FirstOrDefault(c => c.name == clipName);

        if (clip != null)
        {
            return clip.length; // 成功時（秒）
        }
        else
        {
            Debug.LogError("指定されたクリップが見つかりません: " + clipName);
            return 0f;
        }
    }
}
