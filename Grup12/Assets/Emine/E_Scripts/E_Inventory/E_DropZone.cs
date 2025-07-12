using UnityEngine;
using System.Collections;

public class E_DropZone : MonoBehaviour
{
    private bool playerInZone = false;

    public bool itemUsed = false;

    private void Update()
    {
        if (playerInZone && Input.GetKeyDown(KeyCode.F))
        {
            E_InventoryItem item = E_InventoryManager.Instance.GetSelectedItem();
            if (item != null)
            {
                GameObject prefab = Resources.Load<GameObject>(item.itemName);
                if (prefab != null)
                {
                    Vector3 spawnPosition = GetRandomPointOnTop();
                    Instantiate(prefab, spawnPosition, Quaternion.identity);
                    E_InventoryManager.Instance.RemoveOneFromSelected();
                }
                else
                {
                    Debug.LogWarning($"'{item.itemName}' adlý prefab Resources klasöründe bulunamadý!");
                }
            }
            else
            {
                Debug.Log("Drop yapýlacak uygun item yok.");
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            E_InventoryItem selectedItem = E_InventoryManager.Instance.GetSelectedItem();
            if (selectedItem != null)
            {
                itemUsed = true;  // Önce iþaretle
                Debug.Log($"{selectedItem.itemName} kullanýlýyor...");
                StartCoroutine(RemoveSelectedItemAfterDelay(3f));
            }
            else
            {
                Debug.Log("Seçili bir item yok.");
            }
        }
    }

    public IEnumerator RemoveSelectedItemAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        E_InventoryItem selectedItem = E_InventoryManager.Instance.GetSelectedItem();
        if (selectedItem != null)
        {
            E_InventoryManager.Instance.RemoveOneFromSelected();
            Debug.Log($"{selectedItem.itemName} envanterden çýkarýldý.");
        }
    }

    private Vector3 GetRandomPointOnTop()
    {
        Collider col = GetComponent<Collider>();
        if (col == null)
        {
            Debug.LogWarning("DropZone'da Collider yok!");
            return transform.position;
        }

        Vector3 size = col.bounds.size;
        Vector3 center = col.bounds.center;

        float xOffset = Random.Range(-size.x / 1.5f, size.x / 1.5f);
        float zOffset = Random.Range(-size.z / 1.5f, size.z / 1.5f);

        Vector3 pointOnTop = new Vector3(center.x + xOffset, col.bounds.max.y + 0.1f, center.z + zOffset);
        return pointOnTop;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
        }
    }
}
