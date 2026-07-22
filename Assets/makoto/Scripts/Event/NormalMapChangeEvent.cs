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

        yield return messageWinCtrl.ShowMessage("「医療道具」を揃える。か");
        yield return messageWinCtrl.WaitForNextMessage();
        yield return messageWinCtrl.ShowMessage("…4つくらいあれば良いと言われたっけ");
        yield return messageWinCtrl.WaitForNextMessage();
        yield return messageWinCtrl.ShowMessage("この施設はモンスターが特に多くて危険という触れ込みだけど…");
        yield return messageWinCtrl.WaitForNextMessage();
        yield return messageWinCtrl.ShowMessage("果たして私は無事に帰れるのかなぁ");
        yield return messageWinCtrl.WaitForNextMessage();
        yield return messageWinCtrl.ShowMessage("（突如空想の世界の怪物が現代に現れてはや50年）");
        yield return messageWinCtrl.WaitForNextMessage();
        yield return messageWinCtrl.ShowMessage("（生き残った人類はこうして生きるため）");
        yield return messageWinCtrl.WaitForNextMessage();
        yield return messageWinCtrl.ShowMessage("（物資を文明の残骸から回収するのに必死になってて）");
        yield return messageWinCtrl.WaitForNextMessage();
        yield return messageWinCtrl.ShowMessage("（その生存者達の子供が私…）");
        yield return messageWinCtrl.WaitForNextMessage();
        yield return messageWinCtrl.ShowMessage("ま、それはそれとして、任務は頑張らないとね");
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
        yield return messageWinCtrl.ShowMessage("「医療器具」が4つ揃った!");
        yield return messageWinCtrl.WaitForNextMessage();
        yield return messageWinCtrl.ShowMessage("これで怪我などの不測の事態に少しは対応しやすくなる!");
        yield return messageWinCtrl.WaitForNextMessage();
        yield return messageWinCtrl.ShowMessage("これで一旦この施設での任務は完了かな…");
        yield return messageWinCtrl.WaitForNextMessage();
        yield return messageWinCtrl.ShowMessage("何回こういうことを繰り返せば、文明は元に戻るんだろう…");
        yield return messageWinCtrl.WaitForNextMessage();

        SceneManager.LoadScene("Title");
    }
}
