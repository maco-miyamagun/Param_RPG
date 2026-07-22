using System;
using System.Linq;
using UnityEngine;

public class PlayerMapWarpCtrl : MonoBehaviour
{
    [SerializeField] ShareData shareData;
    [SerializeField] PlayerMoveCtrl playerMoveCtrl;

    void Start()
    {
        MapWarpSymbol[] symbols = FindObjectsByType<MapWarpSymbol>(FindObjectsSortMode.None);

        if (shareData.door_id != 404)
        {
            MapWarpSymbol target_symbol = Array.Find<MapWarpSymbol>(symbols, s => s.this_door_id == shareData.door_id);

            this.transform.position = target_symbol.transform.position;
            playerMoveCtrl.Direct(target_symbol.player_dir);
        }
    }
}
