using System.Collections.Generic;
using UnityEngine;

public class E_InventoryManager : MonoBehaviour
{
    public static E_InventoryManager Instance;

    public List<E_InventoryItem> items = new List<E_InventoryItem>();
    public E_InventoryUI inventoryUI;
    public E_InventoryItem selectedItem;

    private E_InventorySlot selectedSlot; // Yeni: Se�ili slotu tut

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
        // Ayn� isimde bir e�ya var m� diye kontrol et
        E_InventoryItem existingItem = items.Find(item => item.itemName == newItem.itemName);

        if (existingItem != null)
        {
            // Varsa sadece say�s�n� art�r
            existingItem.count += newItem.count;

            // UI'yi g�ncelle
            inventoryUI.UpdateUI(items);

            // Ayn� e�yay� tutan slotu bul ve se�
            int index = items.IndexOf(existingItem);
            E_InventorySlot slot = inventoryUI.slots[index];
            SetSelectedItem(existingItem, slot);
        }
        else
        {
            // Yoksa yeni e�ya olarak ekle
            items.Add(newItem);
            inventoryUI.UpdateUI(items);

            int lastIndex = items.Count - 1;
            E_InventorySlot newSlot = inventoryUI.slots[lastIndex];
            SetSelectedItem(newItem, newSlot);
        }
    }



    // Slot bilgisiyle birlikte item'� ayarlayan yeni metod
    public void SetSelectedItem(E_InventoryItem item, E_InventorySlot clickedSlot)
    {
        selectedItem = item;

        // Eski slot varsa �er�evesini kald�r
        if (selectedSlot != null)
            selectedSlot.DeselectSlot();

        selectedSlot = clickedSlot;

        // Yeni se�ilen slotu vurgula
        if (clickedSlot != null)
            clickedSlot.SelectSlot();
    }

    public E_InventoryItem GetSelectedItem()
    {
        return selectedItem;
    }
}
