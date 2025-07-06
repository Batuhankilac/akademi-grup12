using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleNextQuest : MonoBehaviour
{
    public void NextQuest()
    {
        FindAnyObjectByType<E_QuestSystem>().NextQuest();
    }
}
