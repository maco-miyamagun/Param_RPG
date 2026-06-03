using UnityEngine;

public class AttackItem : MonoBehaviour
{
    public string itemName; // アイテムの名前
    ItemManager itemManager; // アイテムマネージャーの参照
    void OnTriggerEnter2D(Collider2D other)
    {
        // プレイヤーと衝突したときの処理
        if (other.CompareTag("Player"))
        {
            string nameText = !string.IsNullOrEmpty(itemName)
                ? itemName
                : "AttackItem";
            Debug.Log($"AttackItemを拾いました: {nameText}");
            // アイテムマネージャーにアイテム名を追加
            itemManager.AttackItems.Add(nameText);
            Destroy(gameObject); // アイテムを消す
        }
    }
    void Start()
    {
        itemManager = FindFirstObjectByType<ItemManager>(); // シーン内のItemManagerを探して参照を取得
    }
}


