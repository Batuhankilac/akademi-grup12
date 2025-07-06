using System.Collections.Generic;
using UnityEngine;

public class E_QuestSystem : MonoBehaviour
{
    public static E_QuestSystem Instance;

    [TextArea]
    public List<string> questList = new List<string>();
    public int activeQuestIndex = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public string GetActiveQuest()
    {
        if (activeQuestIndex >= 0 && activeQuestIndex < questList.Count)
            return questList[activeQuestIndex];

        return "All quests completed!";
    }

    public void NextQuest()
    {
        if (activeQuestIndex < questList.Count - 1)
            activeQuestIndex++;
        FindObjectOfType<E_Quest>().UpdateQuestText();
    }
}
