using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<string> HealItems; // 回復アイテムのリスト
    public List<string> AttackItems; // 攻撃アイテムのリスト
    void Start()
    {
        HealItems = new List<string>(); // 回復アイテムリストの初期化
        AttackItems = new List<string>(); // 攻撃アイテムリストの初期化
    }
}
