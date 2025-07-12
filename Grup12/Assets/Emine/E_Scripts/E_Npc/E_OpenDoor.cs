using System.Collections;
using UnityEngine;

public class E_OpenDoor : MonoBehaviour
{
    public float interactionDistance = 3f;
    public Animator doorAnimator;
    private Transform player;
    private bool isUsed = false;

    private E_EnemyChase enemyChase;

    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        enemyChase = FindAnyObjectByType<E_EnemyChase>();
    }

    void Update()
    {
        if (player == null || isUsed) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= interactionDistance)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Debug.Log("Oyuncu kapýyý açtý.");
                OpenDoor();
            }
            else if (enemyChase != null && enemyChase.carryingPlayer)
            {
                Debug.Log("NPC kapýya yakýn ve oyuncu taþýnýyor, kapý açýlýyor.");
                NPCOpenDoor();
            }
        }
    }


    public void OpenDoor()
    {
        if (isUsed) return;

        E_InventoryItem selectedItem = E_InventoryManager.Instance.GetSelectedItem();

        if (selectedItem != null && selectedItem.itemName == "Key")
        {
            isUsed = true;
            doorAnimator.SetTrigger("Open");
            E_InventoryManager.Instance.RemoveOneFromSelected();
            StartCoroutine(ResetDoorAfterDelay());
        }
    }

    public void NPCOpenDoor()
    {
        if (isUsed) return;

        E_InventoryItem selectedItem = E_InventoryManager.Instance.GetSelectedItem();

        if (selectedItem != null && selectedItem.itemName == "Key")
        {
            isUsed = true;
            doorAnimator.SetTrigger("Open");

            if (enemyChase.carryingPlayer)
            {
                // Tüm anahtarlarý sil
                while (selectedItem != null && selectedItem.itemName == "Key")
                {
                    E_InventoryManager.Instance.RemoveOneFromSelected();
                    selectedItem = E_InventoryManager.Instance.GetSelectedItem(); // Güncel item'i tekrar al
                }
            }
            else
            {
                // Sadece bir tane sil
                E_InventoryManager.Instance.RemoveOneFromSelected();
            }
            StartCoroutine(ResetDoorAfterDelay());
        }
    }

    IEnumerator ResetDoorAfterDelay()
    {
        yield return new WaitForSeconds(2);
        isUsed = false;
    }
}
