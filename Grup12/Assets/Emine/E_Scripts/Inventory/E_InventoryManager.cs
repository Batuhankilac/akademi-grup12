using System.Collections.Generic;
using UnityEngine;

public class E_InventoryManager : MonoBehaviour
{
    public static E_InventoryManager Instance;

    public List<E_InventoryItem> items = new List<E_InventoryItem>();
    public E_InventoryUI inventoryUI;
    public E_InventoryItem selectedItem;

    private E_InventorySlot selectedSlot; // Yeni: Seçili slotu tut

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
        // Ayný isimde bir eþya var mý diye kontrol et
        E_InventoryItem existingItem = items.Find(item => item.itemName == newItem.itemName);

        if (existingItem != null)
        {
            // Varsa sadece sayýsýný artýr
            existingItem.count += newItem.count;

            // UI'yi güncelle
            inventoryUI.UpdateUI(items);

            // Ayný eþyayý tutan slotu bul ve seç
            int index = items.IndexOf(existingItem);
            E_InventorySlot slot = inventoryUI.slots[index];
            SetSelectedItem(existingItem, slot);
        }
        else
        {
            // Yoksa yeni eþya olarak ekle
            items.Add(newItem);
            inventoryUI.UpdateUI(items);

            int lastIndex = items.Count - 1;
            E_InventorySlot newSlot = inventoryUI.slots[lastIndex];
            SetSelectedItem(newItem, newSlot);
        }
    }



    // Slot bilgisiyle birlikte item'ý ayarlayan yeni metod
    public void SetSelectedItem(E_InventoryItem item, E_InventorySlot clickedSlot)
    {
        selectedItem = item;

        // Eski slot varsa çerçevesini kaldýr
        if (selectedSlot != null)
            selectedSlot.DeselectSlot();

        selectedSlot = clickedSlot;

        // Yeni seçilen slotu vurgula
        if (clickedSlot != null)
            clickedSlot.SelectSlot();
    }

    public E_InventoryItem GetSelectedItem()
    {
        return selectedItem;
    }
}
