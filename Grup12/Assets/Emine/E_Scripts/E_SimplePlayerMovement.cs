using UnityEngine;
using Photon.Pun;

public class E_SimplePlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;

    PhotonView view;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        view = GetComponent<PhotonView>();
    }

    void FixedUpdate()
    {
        if (view.IsMine)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 move = new Vector3(moveX, 0, moveZ) * speed;

            // Rigidbody ile hareket ettiriyoruz
            rb.MovePosition(rb.position + move * Time.fixedDeltaTime);

            if (move != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(move);
                rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, 0.15f);
            }
        }
        
    }
}
