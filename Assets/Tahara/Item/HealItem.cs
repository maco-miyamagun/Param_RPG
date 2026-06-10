using UnityEngine;

public class HealItem : MonoBehaviour
{
    public string itemName; // アイテムの名前
    ItemManager itemManager; // アイテムマネージャーの参照
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            string nameText = !string.IsNullOrEmpty(itemName)
                ? itemName
                : "HealItem";
            Debug.Log($"HealItemを拾いました: {nameText}");
            // アイテムマネージャーにアイテム名を追加
            itemManager.HealItems.Add(nameText);
            Destroy(gameObject); // アイテムを消す
        }
    }
    void Start()
    {
        itemManager = FindFirstObjectByType<ItemManager>(); // シーン内のItemManagerを探して参照を取得
    }
}


