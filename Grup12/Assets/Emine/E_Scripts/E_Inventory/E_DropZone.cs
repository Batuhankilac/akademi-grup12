using UnityEngine;

public class E_DropZone : MonoBehaviour
{
    private bool playerInZone = false;

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

        float xOffset = Random.Range(-size.x / 2f, size.x / 2f);
        float zOffset = Random.Range(-size.z / 2f, size.z / 2f);

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
