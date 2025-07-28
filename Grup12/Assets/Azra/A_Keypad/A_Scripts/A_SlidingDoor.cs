using UnityEngine;

namespace NavKeypad
{
    public class SlidingDoor : MonoBehaviour
    {
        [SerializeField] private Animator anim;

        private bool isOpen = false;

        private void Awake()
        {
            if (anim == null)
                anim = GetComponent<Animator>();

            isOpen = false;
        }

        public void OpenDoor()
        {
            Debug.Log("🚪 OpenDoor() çağrıldı");

            if (isOpen) return; // Zaten açılmışsa bir daha oynatma

            isOpen = true;

            if (anim != null)
                anim.SetTrigger("Open");
            else
                Debug.LogWarning("Animator bağlı değil!");
        }
    }
}
