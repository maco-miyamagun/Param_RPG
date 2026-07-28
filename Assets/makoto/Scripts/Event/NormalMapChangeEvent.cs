using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NormalMapChangeEvent : MonoBehaviour
{
    [SerializeField] BlindEffectCtrl blindEffectCtrl;
    [SerializeField] ShareData shareData;

    [SerializeField] MessageWinCtrl messageWinCtrl;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        blindEffectCtrl.TogglePlayerInput(false);
    }

    IEnumerator Start()
    {
        blindEffectCtrl.TogglePlayerInput(false);

        yield return blindEffectCtrl.BlindShow(true);

        if (shareData.story_progress > 0)
        {
            blindEffectCtrl.TogglePlayerInput(true);
            yield break;
        }

        messageWinCtrl.ToggleShowUI(true);

        yield return messageWinCtrl.ShowMessage("医療器具を4つ揃えよう!");
        yield return messageWinCtrl.WaitForNextMessage();
        yield return messageWinCtrl.ShowMessage("光っているアイテムが医療器具");
        yield return messageWinCtrl.WaitForNextMessage();
        yield return messageWinCtrl.ShowMessage("付近にうろついている敵を倒しつつ、全てのアイテムを集めればクリア!");
        yield return messageWinCtrl.WaitForNextMessage();
        yield return messageWinCtrl.ShowMessage("強い敵に勝つには、レベルを上げると良し！");
        yield return messageWinCtrl.WaitForNextMessage();

        messageWinCtrl.ToggleShowUI(false);
        blindEffectCtrl.TogglePlayerInput(true);

        shareData.story_progress = 1;
    }

    public IEnumerator Ending()
    {
        blindEffectCtrl.TogglePlayerInput(false);

        messageWinCtrl.ToggleShowUI(true);

        yield return messageWinCtrl.ShowMessage("よし!");
        yield return messageWinCtrl.WaitForNextMessage();
        yield return messageWinCtrl.ShowMessage("医療器具が4つ揃った!");
        yield return messageWinCtrl.WaitForNextMessage();
        yield return messageWinCtrl.ShowMessage("ゲームクリア お疲れ様です…");

        SceneManager.LoadScene("Title");
    }
}
