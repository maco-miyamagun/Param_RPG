using UnityEngine;
using UnityEngine.InputSystem;

public class SelectAttackItem : MonoBehaviour
{
    private ItemManager itemManager;

    void Start()
    {
        // ItemManagerの参照を取得
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
        CheckNumberKeyInput();
    }

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

    string GetKeyName(int index)
    {
        return index == 9 ? "0" : (index + 1).ToString();
    }
}
