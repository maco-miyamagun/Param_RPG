using System.Collections;
using System.Linq;
using UnityEngine;

public class SlimeCtrl : MonoBehaviour
{
    [SerializeField] EntityStatus status;
    [SerializeField] WeaponCtrl weaponCtrl;
    [SerializeField] EntityDamageCtrl damageCtrl;

    PlayerBattle playerBattle;

    [SerializeField] EnemyMoveCtrl move_ctrl;

    [SerializeField] Animator animator;

    const int TACKLE_WEAPON = 0;
    const int JUMP_WEAPON = 1;
    const int BOW_WEAPON = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerBattle = FindObjectsByType<PlayerBattle>(FindObjectsSortMode.None)[0];

        StartCoroutine(AILogic());
    }

    IEnumerator AILogic()
    {
        move_ctrl.is_skip_move = true;
        yield return new WaitForSeconds(2f);
        move_ctrl.is_skip_move = false;

        while (!damageCtrl.IsDead())
        {
            int dice = ChoiceAttackWeapon();
            weaponCtrl.use_weapon_index = dice;

            if (dice == TACKLE_WEAPON)
            {
                yield return TackleAttack();
            }
            else if (dice == JUMP_WEAPON)
            {
                yield return JumpAttack();
            }
            else if (dice == BOW_WEAPON)
            {
                yield return BowAttack();
            }
        }

        move_ctrl.is_reach_move = false;
        move_ctrl.is_skip_move = true;


        damageCtrl.DeadAnim();
        yield return new WaitForSeconds(GetAnimationClipLength("EntityAnimDamaged"));

        playerBattle.WinPlayer();
        Destroy(this.gameObject);
    }

    int ChoiceAttackWeapon()
    {
        int dice = Random.Range(1, 10);

        if (dice > 8)
        {
            return TACKLE_WEAPON;
        }
        else if (dice < 3)
        {
            return BOW_WEAPON;
        }
        else
        {
            return TACKLE_WEAPON;
        }
    }

    IEnumerator TackleAttack()
    {
        move_ctrl.is_skip_move = true;

        if (damageCtrl.IsDead()) yield break;
        move_ctrl.Direct(move_ctrl.GetPlayerDirection());
        animator.Play("SlimeAnimChargeSkill", 0);
        yield return new WaitForSeconds(GetAnimationClipLength("SlimeAnimChargeSkill"));

        if (damageCtrl.IsDead()) yield break;
        move_ctrl.Direct(move_ctrl.GetPlayerDirection());
        weaponCtrl.Attack();


        if (damageCtrl.IsDead()) yield break;
        yield return ReachingPlayer();

        if (damageCtrl.IsDead()) yield break;
        yield return new WaitForSeconds(2f);
    }

    IEnumerator JumpAttack()
    {
        move_ctrl.Direct(move_ctrl.GetPlayerDirection());
        yield return ChasingPlayer();

        if (damageCtrl.IsDead()) yield break;
        animator.Play("SlimeAnimChargeSkill", 0);
        yield return new WaitForSeconds(GetAnimationClipLength("SlimeAnimChargeSkill"));

        if (damageCtrl.IsDead()) yield break;
        weaponCtrl.Attack();
        yield return new WaitForSeconds(1f);
    }

    IEnumerator BowAttack()
    {
        move_ctrl.is_skip_move = true;

        if (damageCtrl.IsDead()) yield break;
        move_ctrl.Direct(move_ctrl.GetPlayerDirection());
        animator.Play("SlimeAnimChargeSkill", 0);
        yield return new WaitForSeconds(GetAnimationClipLength("SlimeAnimChargeSkill"));

        if (damageCtrl.IsDead()) yield break;
        weaponCtrl.Attack();
        yield return new WaitForSeconds(0.5f);

        weaponCtrl.use_weapon_index = 3;

        weaponCtrl.Attack();
        yield return new WaitForSeconds(2f);

        move_ctrl.is_skip_move = false;
    }

    IEnumerator ChasingPlayer()
    {
        move_ctrl.is_skip_move = false;

        const float LIMIT_TIME = 4f;
        const float DISTANCE = 4f;
        const float TIME_INCREMENT = 0.1f;

        for (float t = 0; t < LIMIT_TIME; t += TIME_INCREMENT)
        {
            if (damageCtrl.IsDead())
            {
                move_ctrl.is_skip_move = true;
                break;
            }

            if (move_ctrl.GetPlayerDistance() <= DISTANCE)
            {
                break;
            }

            yield return new WaitForSeconds(TIME_INCREMENT);
        }

        move_ctrl.is_skip_move = true;
    }

    IEnumerator ReachingPlayer()
    {
        move_ctrl.SetReachTarget(move_ctrl.GetPlayerPos());
        move_ctrl.is_reach_move = true;

        const float LIMIT_TIME = 2f;
        const float DISTANCE = 3f;
        const float TIME_INCREMENT = 0.1f;

        for (float t = 0; t < LIMIT_TIME; t += TIME_INCREMENT)
        {
            if (damageCtrl.IsDead())
            {
                move_ctrl.is_reach_move = false;
                break;
            }

            if (move_ctrl.GetPlayerDistance() <= DISTANCE)
            {
                break;
            }

            yield return new WaitForSeconds(TIME_INCREMENT);
        }

        move_ctrl.is_reach_move = false;
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
