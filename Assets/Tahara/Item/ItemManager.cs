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
        // ゲーム開始時にリストと選択状態を初期化する
        HealItems = new List<string>(); // 回復アイテムリストの初期化
        AttackItems = new List<string>(); // 攻撃アイテムリストの初期化
        SelectedAttackItemIndex = 0;
    }

    // 攻撃アイテムを追加する。上限に達している場合は、現在選択中のアイテムと入れ替える
    public void AddOrReplaceAttackItem(string itemName)
    {
        // リストが未生成なら先に初期化する
        if (AttackItems == null)
        {
            AttackItems = new List<string>();
        }

        // 上限未満ならそのまま追加する
        if (AttackItems.Count < MaxAttackItems)
        {
            AttackItems.Add(itemName);
            Debug.Log($"AttackItemを追加しました: {itemName} ({AttackItems.Count}/{MaxAttackItems})");
            return;
        }

        // 選択中インデックスが範囲外なら、最初のアイテムを交換対象にする
        if (SelectedAttackItemIndex < 0 || SelectedAttackItemIndex >= AttackItems.Count)
        {
            SelectedAttackItemIndex = 0;
        }

        // 置き換え前のアイテムを記録してから、選択中の場所を新しいアイテムに差し替える
        string replacedItem = AttackItems[SelectedAttackItemIndex];
        AttackItems[SelectedAttackItemIndex] = itemName;
        Debug.Log($"AttackItemを交換しました: {replacedItem} → {itemName} (インデックス {SelectedAttackItemIndex + 1})");
    }
}

