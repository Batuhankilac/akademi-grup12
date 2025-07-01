using UnityEngine;

public class E_PickupItem : MonoBehaviour
{
    public string itemName;
    public Sprite icon;
    public int amount = 1;

    private bool playerNearby = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }

    private void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.R))
        {
            E_InventoryItem newItem = new E_InventoryItem
            {
                itemName = itemName,
                icon = icon,
                count = amount
            };

            E_InventoryManager.Instance.AddItem(newItem);

            Destroy(gameObject);
        }
    }

}
