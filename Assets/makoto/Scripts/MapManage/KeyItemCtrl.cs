using UnityEngine;

public class KeyItemCtrl : MonoBehaviour
{
    [SerializeField] int keyitem_map_num;
    [SerializeField] ShareData shareData;
    [SerializeField] NormalMapChangeEvent normalMapChangeEvent;

    void Start()
    {
        if (keyitem_map_num == 1 && shareData.keyitem_map01)
        {
            this.gameObject.SetActive(false);
        }
        if (keyitem_map_num == 2 && shareData.keyitem_map02)
        {
            this.gameObject.SetActive(false);
        }
        if (keyitem_map_num == 4 && shareData.keyitem_map04)
        {
            this.gameObject.SetActive(false);
        }
        if (keyitem_map_num == 6 && shareData.keyitem_map06)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            if (keyitem_map_num == 1)
            {
                shareData.keyitem_map01 = true;
            }
            else if (keyitem_map_num == 2)
            {
                shareData.keyitem_map02 = true;
            }
            else if (keyitem_map_num == 4)
            {
                shareData.keyitem_map04 = true;
            }
            else if (keyitem_map_num == 6)
            {
                shareData.keyitem_map06 = true;
            }
            if (shareData.keyitem_map01 && shareData.keyitem_map02 && shareData.keyitem_map04 && shareData.keyitem_map06)
            {
                StartCoroutine(normalMapChangeEvent.Ending());
                return;
            }

            this.gameObject.SetActive(false);
        }
    }
}
