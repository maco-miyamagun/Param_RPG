using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] ShareData shareData;
    [SerializeField] PlayerShareData playerShareData;
    [SerializeField] PlayerLevelCtrl playerLevelCtrl;

    void Start()
    {
        foreach (var item in FindObjectsByType<PlayerShareData>(FindObjectsSortMode.None))
        {
            if (item.playerShareDataType == PlayerShareDataType.CLIENT) continue;

            playerShareData.SetData(item.GetShareData(), item.GetStatusData());
        }

        playerLevelCtrl.SetStatusForLevel(shareData.level);
    }
}
