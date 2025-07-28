using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    public Animator doorAnimator;

    public List<ShapeData> playerInput = new List<ShapeData>();

    private List<ShapeData> correctCombination = new List<ShapeData>()
    {
        new ShapeData(ShapeType.Circle, ShapeColor.Red),
        new ShapeData(ShapeType.Triangle, ShapeColor.Blue),
        new ShapeData(ShapeType.Square, ShapeColor.Yellow)
    };

    public void CheckCombination()
    {
        bool isCorrect = true;

        for (int i = 0; i < correctCombination.Count; i++)
        {
            if (!playerInput[i].Equals(correctCombination[i]))
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect)
        {
            Debug.Log("✔ Doğru kombinasyon! Kasa açılıyor...");
            doorAnimator.SetTrigger("Open");
        }
        else
        {
            Debug.Log("✖ Yanlış kombinasyon.");
        }
    }
}
