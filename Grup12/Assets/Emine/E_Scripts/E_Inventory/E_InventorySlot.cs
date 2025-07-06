using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class E_InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI countText;
    private E_InventoryItem currentItem;

    public Button button; // Slot butonuna referans
    public GameObject selectionFrame; // Kýrmýzý çerçeve görseli (Inspector’dan atanacak)

    private void Start()
    {
        button.onClick.AddListener(OnSlotClick);
        DeselectSlot(); // Baþlangýçta çerçeve gizli
    }

    public void SetItem(E_InventoryItem item)
    {
        currentItem = item;
        icon.sprite = item.icon;
        icon.enabled = true;

        countText.text = item.count > 0 ? item.count.ToString() : "";
    }

    public void ClearSlot()
    {
        currentItem = null;
        icon.sprite = null;
        icon.enabled = false;
        countText.text = "";
        DeselectSlot(); // Slot boþsa seçili görünmesin
    }

    private void OnSlotClick()
    {
        E_InventoryManager.Instance.SetSelectedItem(currentItem, this);

        if (currentItem != null)
            Debug.Log("Seçilen item: " + currentItem.itemName);
        else
            Debug.Log("Seçilen item: Boþ slot");
    }

    public void SelectSlot()
    {
        if (selectionFrame != null)
            selectionFrame.SetActive(true);
    }

    public void DeselectSlot()
    {
        if (selectionFrame != null)
            selectionFrame.SetActive(false);
    }
}
