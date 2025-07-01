using System.Collections.Generic;
using UnityEngine;

public class E_InventoryManager : MonoBehaviour
{
    public static E_InventoryManager Instance;

    public List<E_InventoryItem> items = new List<E_InventoryItem>();
    public E_InventoryUI inventoryUI;
    public E_InventoryItem selectedItem;
    private E_InventorySlot selectedSlot;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (inventoryUI == null)
            inventoryUI = FindObjectOfType<E_InventoryUI>();

        inventoryUI.UpdateUI(items);
    }

    public void AddItem(E_InventoryItem newItem)
    {
        E_InventoryItem existingItem = items.Find(item => item.itemName == newItem.itemName);

        if (existingItem != null)
        {
            existingItem.count += newItem.count;
            inventoryUI.UpdateUI(items);

            int index = items.IndexOf(existingItem);
            E_InventorySlot slot = inventoryUI.slots[index];
            SetSelectedItem(existingItem, slot);
        }
        else
        {
            items.Add(newItem);
            inventoryUI.UpdateUI(items);

            int lastIndex = items.Count - 1;
            E_InventorySlot newSlot = inventoryUI.slots[lastIndex];
            SetSelectedItem(newItem, newSlot);
        }
    }

    public void SetSelectedItem(E_InventoryItem item, E_InventorySlot clickedSlot)
    {
        selectedItem = item;

        if (selectedSlot != null)
            selectedSlot.DeselectSlot();

        selectedSlot = clickedSlot;

        if (clickedSlot != null)
            clickedSlot.SelectSlot();
    }

    public E_InventoryItem GetSelectedItem()
    {
        return selectedItem;
    }

    public void RemoveOneFromSelected()
    {
        if (selectedItem == null) return;

        selectedItem.count--;

        if (selectedItem.count <= 0)
        {
            int index = items.IndexOf(selectedItem);
            if (index >= 0)
            {
                items.RemoveAt(index);
                inventoryUI.UpdateUI(items);

                selectedItem = null;
                if (items.Count > 0)
                {
                    SetSelectedItem(items[0], inventoryUI.slots[0]);
                }
                else
                {
                    selectedSlot = null;
                }
            }
        }
        else
        {
            inventoryUI.UpdateUI(items);
        }
    }
}
