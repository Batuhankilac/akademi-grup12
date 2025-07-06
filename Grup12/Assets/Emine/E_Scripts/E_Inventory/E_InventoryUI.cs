using System.Collections.Generic;
using UnityEngine;

public class E_InventoryUI : MonoBehaviour
{
    public List<E_InventorySlot> slots; // Sahnedeki slotlar� inspector�dan elle verece�iz

    public void UpdateUI(List<E_InventoryItem> items)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < items.Count)
            {
                slots[i].SetItem(items[i]); // E�ya varsa g�ster
            }
            else
            {
                slots[i].ClearSlot(); // E�ya yoksa temizle
            }
        }
    }
}
