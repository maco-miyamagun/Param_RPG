using System.Collections;
using UnityEngine;

public class GoblinCtrl : MonoBehaviour
{
    [SerializeField] EntityStatus status;
    [SerializeField] WeaponCtrl weaponCtrl;
    [SerializeField] EntityDamageCtrl damageCtrl;

    BattleManage battleManage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        battleManage = FindObjectsByType<BattleManage>(FindObjectsSortMode.None)[0];

        StartCoroutine(MoveLogic());
    }

    void FixedUpdate()
    {
        if (damageCtrl.IsDead())
        {
            battleManage.WinPlayer();
            Destroy(this.gameObject);
        }
    }

    IEnumerator MoveLogic()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.4f);
        }
    }
}
