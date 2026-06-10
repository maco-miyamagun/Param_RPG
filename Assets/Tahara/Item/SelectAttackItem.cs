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

        // 1キーはAttackItems[0]、2キーはAttackItems[1]、...、0キーはAttackItems[9]を選択する
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

        if (keyboard.digit4Key.wasPressedThisFrame)
        {
            SelectAttackItemByIndex(3);
            return;
        }

        if (keyboard.digit5Key.wasPressedThisFrame)
        {
            SelectAttackItemByIndex(4);
            return;
        }

        if (keyboard.digit6Key.wasPressedThisFrame)
        {
            SelectAttackItemByIndex(5);
            return;
        }

        if (keyboard.digit7Key.wasPressedThisFrame)
        {
            SelectAttackItemByIndex(6);
            return;
        }

        if (keyboard.digit8Key.wasPressedThisFrame)
        {
            SelectAttackItemByIndex(7);
            return;
        }

        if (keyboard.digit9Key.wasPressedThisFrame)
        {
            SelectAttackItemByIndex(8);
            return;
        }

        if (keyboard.digit0Key.wasPressedThisFrame)
        {
            SelectAttackItemByIndex(9);
        }
    }

    /// <summary>
    /// 指定したインデックスのAttackItemsを選択し、結果をコンソールに出力する
    /// </summary>
    void SelectAttackItemByIndex(int index)
    {
        if (index < itemManager.AttackItems.Count)
        {
            string selectedItem = itemManager.AttackItems[index];
            Debug.Log($"選択されたアイテム: {selectedItem} (キー: {GetKeyName(index)})");
        }
        else
        {
            Debug.LogWarning($"キー {GetKeyName(index)} (インデックス {index}): このインデックスのアイテムは存在しません");
        }
    }

    /// <summary>
    /// インデックス番号から対応するキー名を返す
    /// </summary>
    string GetKeyName(int index)
    {
        return index == 9 ? "0" : (index + 1).ToString();
    }
}
