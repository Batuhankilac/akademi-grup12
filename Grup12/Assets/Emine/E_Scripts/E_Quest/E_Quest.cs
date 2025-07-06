using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class E_Quest : MonoBehaviour
{
    public Button questButton;
    public GameObject questPanel;
    public TextMeshProUGUI questText;

    public Image questButtonImage;
    public Sprite normalSprite;
    public Sprite newQuestSprite;

    public AudioSource audioSource;
    public AudioClip newQuest;

    private string currentQuestText = "";

    private void Start()
    {
        questPanel.SetActive(false);
        questButton.onClick.AddListener(ToggleQuestPanel);

        UpdateQuestText();
    }

    public void ToggleQuestPanel()
    {
        questPanel.SetActive(!questPanel.activeSelf);

        if (questPanel.activeSelf)
        {
            // Butona týklanýnca yeni görev iþareti kaybolsun
            questButtonImage.sprite = normalSprite;
        }
    }

    public void UpdateQuestText()
    {
        string newQuestText = E_QuestSystem.Instance.GetActiveQuest();

        if (newQuestText != currentQuestText)
        {
            currentQuestText = newQuestText;
            questText.text = newQuestText;
            audioSource.PlayOneShot(newQuest);
            questButtonImage.sprite = newQuestSprite;
        }
    }

}
