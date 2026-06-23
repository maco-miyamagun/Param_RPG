using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Playerにアタッチして、Input Systemの数字キーで攻撃アイテムを選択するためのスクリプト
/// </summary>
public class SelectAttackItem : MonoBehaviour
{
    private ItemManager itemManager;

    void Start()
    {
        // ItemManagerの参照を取得する。Playerにアタッチした場合でも探索できるようにする。
        itemManager = GetComponent<ItemManager>();
        if (itemManager == null)
        {
            itemManager = GetComponentInParent<ItemManager>();
        }

        if (itemManager == null)
        {
            itemManager = FindFirstObjectByType<ItemManager>();
        }

        if (itemManager == null)
        {
            Debug.LogError("ItemManagerが見つかりません");
        }
    }

    void Update()
    {
        // 毎フレーム、数字キーが押されたかをチェックする
        CheckNumberKeyInput();
    }

    /// <summary>
    /// 1～0の数字キー入力をチェックし、対応するAttackItemsのインデックスを選択する
    /// </summary>
    void CheckNumberKeyInput()
    {
        if (itemManager == null || itemManager.AttackItems == null || itemManager.AttackItems.Count == 0)
        {
            return;
        }

        Keyboard keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return;
        }

        if (keyboard.digit1Key.wasPressedThisFrame)
        {
            SelectAttackItemByIndex(0);
            return;
        }

        if (keyboard.digit2Key.wasPressedThisFrame)
        {
            SelectAttackItemByIndex(1);
            return;
        }

        if (keyboard.digit3Key.wasPressedThisFrame)
        {
            SelectAttackItemByIndex(2);
            return;
        }
    }

    /// <summary>
    /// 指定したインデックスのAttackItemsを選択し、結果をコンソールに出力する
    /// </summary>
    void SelectAttackItemByIndex(int index)
    {
        if (itemManager.AttackItems == null || index < 0 || index >= itemManager.AttackItems.Count)
        {
            Debug.LogWarning($"キー {GetKeyName(index)} (インデックス {index}): このインデックスのアイテムは存在しません");
            return;
        }

        itemManager.SelectedAttackItemIndex = index;
        string selectedItem = itemManager.AttackItems[index];
        Debug.Log($"選択されたアイテム: {selectedItem} (キー: {GetKeyName(index)})");
    }

    /// <summary>
    /// インデックス番号から対応するキー名を返す
    /// </summary>
    string GetKeyName(int index)
    {
        return (index + 1).ToString();
    }
}
