using UnityEngine;

public class E_EnemyChase : MonoBehaviour
{
    public float speed = 3f;
    private Transform targetPlayer;
    public Transform prisonArea;
    public Transform startPoint; // Sahnedeki sabit d�n�� noktas�

    private bool carryingPlayer = false;
    private bool returning = false;

    void Update()
    {
        if (carryingPlayer && targetPlayer != null)
        {
            // Oyuncuyu prisonArea'ya ta��
            Vector3 prisonTarget = new Vector3(prisonArea.position.x, transform.position.y, prisonArea.position.z);
            Vector3 direction = (prisonTarget - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, prisonTarget) < 0.5f)
            {
                // B�rak
                targetPlayer.parent = null;
                targetPlayer.position = prisonArea.position + Vector3.up * 1f;
                targetPlayer = null;
                carryingPlayer = false;

                // Eve d�n
                returning = true;
            }
        }
        else if (returning)
        {
            // Sabit startPoint konumuna d�n
            Vector3 homeTarget = new Vector3(startPoint.position.x, transform.position.y, startPoint.position.z);
            Vector3 direction = (homeTarget - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, homeTarget) < 0.1f)
            {
                returning = false;
                Debug.Log("D��man yerine d�nd�.");
            }
        }
        else if (targetPlayer != null)
        {
            // Oyuncuya do�ru hareket et
            Vector3 targetXZ = new Vector3(targetPlayer.position.x, transform.position.y, targetPlayer.position.z);
            Vector3 direction = (targetXZ - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    public void InvokeChase()
    {
        if (carryingPlayer || returning) return;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length == 0)
        {
            Debug.LogWarning("Sahnede hi� oyuncu yok.");
            return;
        }

        int randomIndex = Random.Range(0, players.Length);
        targetPlayer = players[randomIndex].transform;

        Debug.Log("Hedef oyuncu: " + targetPlayer.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (targetPlayer != null && other.transform == targetPlayer && !carryingPlayer)
        {
            carryingPlayer = true;
            targetPlayer.parent = transform;
        }
    }
}
