using System.Collections;
using System.Linq;
using UnityEngine;

public class HobGoblinCtrl : MonoBehaviour
{
    [SerializeField] EntityStatus status;
    [SerializeField] WeaponCtrl weaponCtrl;
    [SerializeField] EntityDamageCtrl damageCtrl;

    PlayerBattle playerBattle;

    [SerializeField] EnemyMoveCtrl move_ctrl;

    [SerializeField] Animator animator;
    [SerializeField] GameObject fire_core;

    const int STICK_WEAPON = 0;
    const int SHIELD_WEAPON = 1;
    const int BOW_WEAPON = 2;
    const int STICK_WEAPON_SPECIAL = 4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerBattle = FindObjectsByType<PlayerBattle>(FindObjectsSortMode.None)[0];

        StartCoroutine(AILogic());
    }

    IEnumerator AILogic()
    {
        while (!damageCtrl.IsDead())
        {
            IsLifeHalf();
            int dice = ChoiceAttackWeapon();
            weaponCtrl.use_weapon_index = dice;

            if (dice == STICK_WEAPON)
            {
                yield return StickAttack();
            }
            else if (dice == STICK_WEAPON_SPECIAL)
            {
                yield return StickAttackSpecial();
            }
            else if (dice == SHIELD_WEAPON)
            {
                yield return ShieldDefense();
            }
            else if (dice == BOW_WEAPON)
            {
                yield return BowAttack();
            }
        }

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
            return SHIELD_WEAPON;
        }
        else if (dice < 3)
        {
            return BOW_WEAPON;
        }
        else if (dice > 5)
        {
            return STICK_WEAPON_SPECIAL;
        }
        else
        {
            return STICK_WEAPON;
        }
    }

    IEnumerator StickAttack()
    {
        move_ctrl.Direct(move_ctrl.GetPlayerDirection());
        yield return ChasingPlayer();

        if (damageCtrl.IsDead()) yield break;
        animator.Play("EntityAnimChargeSkill", 0);
        yield return new WaitForSeconds(GetAnimationClipLength("EntityAnimChargeSkill"));

        if (damageCtrl.IsDead()) yield break;
        weaponCtrl.Attack();
        yield return new WaitForSeconds(1f);
    }

    IEnumerator StickAttackSpecial()
    {
        move_ctrl.Direct(move_ctrl.GetPlayerDirection());
        yield return ChasingPlayer();

        if (damageCtrl.IsDead()) yield break;
        animator.Play("EntityAnimChargeSkill", 0);
        yield return new WaitForSeconds(GetAnimationClipLength("EntityAnimChargeSkill"));

        if (damageCtrl.IsDead()) yield break;
        StartCoroutine(AttackRotation());
        weaponCtrl.Attack();
        yield return new WaitForSeconds(3f);
    }

    IEnumerator AttackRotation()
    {
        const float LIMIT_TIME = 1f;
        const float TIME_INCREMENT = 0.01f;

        float angleZBase = fire_core.transform.eulerAngles.z;

        for (float t = 0; t < LIMIT_TIME; t += TIME_INCREMENT)
        {
            float result = Mathf.Lerp(0, 360, t / LIMIT_TIME);

            fire_core.transform.rotation = Quaternion.Euler(0, 0, angleZBase + result);

            yield return new WaitForSeconds(TIME_INCREMENT);
        }
    }

    IEnumerator ShieldDefense()
    {
        move_ctrl.is_skip_move = true;

        if (damageCtrl.IsDead()) yield break;
        move_ctrl.Direct(move_ctrl.GetPlayerDirection());
        animator.Play("EntityAnimChargeSkill", 0);
        yield return new WaitForSeconds(GetAnimationClipLength("EntityAnimChargeSkill"));

        if (damageCtrl.IsDead()) yield break;
        weaponCtrl.Attack();
        yield return new WaitForSeconds(3f);

        move_ctrl.is_skip_move = false;
    }

    IEnumerator BowAttack()
    {
        move_ctrl.is_skip_move = true;

        if (damageCtrl.IsDead()) yield break;
        move_ctrl.Direct(move_ctrl.GetPlayerDirection());
        animator.Play("EntityAnimChargeSkill", 0);
        yield return new WaitForSeconds(GetAnimationClipLength("EntityAnimChargeSkill"));

        if (damageCtrl.IsDead()) yield break;
        weaponCtrl.Attack();
        yield return new WaitForSeconds(0.1f);

        weaponCtrl.use_weapon_index = 3;

        weaponCtrl.Attack();
        yield return new WaitForSeconds(2f);

        move_ctrl.is_skip_move = false;
    }

    IEnumerator ChasingPlayer()
    {
        move_ctrl.is_skip_move = false;

        const float LIMIT_TIME = 4f;
        const float DISTANCE = 2.5f;
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

    void IsLifeHalf()
    {
        const int FULL_AGI = 15;
        if (status.AGI == FULL_AGI) return;

        if (status.life < status.HIT_POINT / 2)
        {
            status.AGI = FULL_AGI;
        }
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
