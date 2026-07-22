using System.Collections;
using UnityEngine;

public class MapWarpSymbol : MonoBehaviour
{
    public int this_door_id;
    public int next_door_id;
    [SerializeField] string map_name;
    public Vector2 player_dir;
    bool is_trigger_active = false;

    [SerializeField] ShareData shareData;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.01f);
        is_trigger_active = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!is_trigger_active) return;
        shareData.door_id = next_door_id;
        shareData.enable_first_enemy = true;
        shareData.enable_second_enemy = true;
        shareData.enable_third_enemy = true;
        FindObjectsByType<GameManage>(FindObjectsSortMode.None)[0].ChangeScene(map_name);
    }
}
