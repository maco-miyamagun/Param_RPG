using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] ShareData shareData;
    [SerializeField] PlayerShareData playerShareData;
    [SerializeField] PlayerLevelCtrl playerLevelCtrl;

    void Awake()
    {
        foreach (var item in FindObjectsByType<PlayerShareData>(FindObjectsSortMode.None))
        {
            if (item.playerShareDataType == PlayerShareDataType.CLIENT) continue;

            playerShareData.SetData(item.GetShareData(), item.GetStatusData());
        }

        playerLevelCtrl.SetStatusForLevel(shareData.level);
    }
}
