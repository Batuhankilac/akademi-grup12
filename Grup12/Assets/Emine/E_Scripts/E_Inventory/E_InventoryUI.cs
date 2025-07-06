using System.Collections.Generic;
using UnityEngine;

public class E_InventoryUI : MonoBehaviour
{
    public List<E_InventorySlot> slots; // Sahnedeki slotlarý inspector’dan elle vereceðiz

    public void UpdateUI(List<E_InventoryItem> items)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < items.Count)
            {
                slots[i].SetItem(items[i]); // Eþya varsa göster
            }
            else
            {
                slots[i].ClearSlot(); // Eþya yoksa temizle
            }
        }
    }
}
