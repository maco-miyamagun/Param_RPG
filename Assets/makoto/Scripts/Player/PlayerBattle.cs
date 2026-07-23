using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerBattle : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] BattleManage battleManage;
    [SerializeField] ShareData shareData;
    [SerializeField] PlayerLevelCtrl playerLevelCtrl;

    [SerializeField] WeaponCtrl weaponCtrl;
    [SerializeField] EntityAttackCtrl attackCtrl;
    [SerializeField] EntityDamageCtrl damageCtrl;

    [SerializeField] Animator animator;

    [SerializeField] BattleClearUICtrl battleClearUI;
    [SerializeField] BattleLoseUICtrl battleLoseUI;

    bool is_player_win = false;

    void Start()
    {
        StartCoroutine(PlayerLifeCycle());
    }

    IEnumerator PlayerLifeCycle()
    {
        while (!damageCtrl.IsDead())
        {
            yield return null;
        }

        damageCtrl.DeadAnim();
        yield return new WaitForSeconds(GetAnimationClipLength("EntityAnimDamaged"));

        LosePlayer();
    }

    public void OnAttack()
    {
        StartCoroutine(CorAttack());
    }

    IEnumerator CorAttack()
    {
        if (!attackCtrl.AttackEnabled) yield break;

        animator.Play("PlayerAnimChargeSkill", 0);
        yield return new WaitForSeconds(GetAnimationClipLength("PlayerAnimChargeSkill"));
        weaponCtrl.Attack();
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

    public void WinPlayer()
    {
        playerInput.SwitchCurrentActionMap("pause");
        battleClearUI.SetVisible(true);
        battleClearUI.SetUI();

        shareData.experience += battleManage.reward_exp;
        playerLevelCtrl.CheckLevelUp();

        battleClearUI.SetUI();

        is_player_win = true;
    }

    public void LosePlayer()
    {
        Time.timeScale = 0f;
        playerInput.SwitchCurrentActionMap("pause");
        battleLoseUI.SetVisible(true);
    }

    public void OnNext()
    {
        if (damageCtrl.IsDead())
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Title");

            return;
        }
        else if (!is_player_win && !damageCtrl.IsDead()) return;

        shareData.door_id = 404;
        FindObjectsByType<GameManage>(FindObjectsSortMode.None)[0].ChangeScene("");
    }
}
