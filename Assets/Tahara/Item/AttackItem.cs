using UnityEngine;
using UnityEngine.InputSystem;

public class AttackItem : MonoBehaviour
{
    public string itemName; // アイテムの名前
    private ItemManager itemManager; // アイテムマネージャーの参照
    private bool canPickup = false;

    void Start()
    {
        itemManager = FindFirstObjectByType<ItemManager>(); // シーン内のItemManagerを探して参照を取得
    }

    void Update()
    {
        // プレイヤーが接触していてFキーが押されたら拾う
        if (canPickup && Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            PickupItem();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = true;
            Debug.Log("Fキーでアイテムを拾うことができます");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = false;
        }
    }

    private void PickupItem()
    {
        if (itemManager == null)
        {
            Debug.LogError("ItemManagerが見つかりません");
            return;
        }

        string nameText = !string.IsNullOrEmpty(itemName)
            ? itemName
            : "AttackItem";

        Debug.Log($"AttackItemを拾いました: {nameText}");
        itemManager.AddOrReplaceAttackItem(nameText);
        Destroy(gameObject);
    }
}


