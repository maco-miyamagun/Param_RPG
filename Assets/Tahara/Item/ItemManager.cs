using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<string> HealItems; // 回復アイテムのリスト
    public List<string> AttackItems; // 攻撃アイテムのリスト
    public int SelectedAttackItemIndex; // 現在選択中の攻撃アイテムのインデックス
    public const int MaxAttackItems = 3; // 攻撃アイテムの所持上限

    void Start()
    {
        HealItems = new List<string>(); // 回復アイテムリストの初期化
        AttackItems = new List<string>(); // 攻撃アイテムリストの初期化
        SelectedAttackItemIndex = 0;
    }

    public void AddOrReplaceAttackItem(string itemName)
    {
        if (AttackItems == null)
        {
            AttackItems = new List<string>();
        }

        if (AttackItems.Count < MaxAttackItems)
        {
            AttackItems.Add(itemName);
            Debug.Log($"AttackItemを追加しました: {itemName} ({AttackItems.Count}/{MaxAttackItems})");
            return;
        }

        if (SelectedAttackItemIndex < 0 || SelectedAttackItemIndex >= AttackItems.Count)
        {
            SelectedAttackItemIndex = 0;
        }

        string replacedItem = AttackItems[SelectedAttackItemIndex];
        AttackItems[SelectedAttackItemIndex] = itemName;
        Debug.Log($"AttackItemを交換しました: {replacedItem} → {itemName} (インデックス {SelectedAttackItemIndex + 1})");
    }
}

