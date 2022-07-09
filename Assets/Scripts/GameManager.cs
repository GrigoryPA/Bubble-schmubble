using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public int maxActionsNumber = 10;
    public Text scoreText;
    public Text motionsText;
    public UnityEvent onEndGameEvent;

    private int currentScore = 0;
    private int currentRemainingMotions = 0;
    private string scoreStr = "Очки: ";
    private string motionsStr = "Шаги: ";

    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
        currentRemainingMotions = maxActionsNumber;
        UpdateTextUI();
    }

    public void TakeScore(int matchCount)
    {
        currentRemainingMotions--;
        currentScore += matchCount < 3 ? 1 : matchCount - 1;
        UpdateTextUI();

        if (currentRemainingMotions <= 0)
        {
            onEndGameEvent.Invoke();
        }
    }

    private void UpdateTextUI()
    {
        scoreText.text = scoreStr + currentScore.ToString("D4");
        motionsText.text = motionsStr + currentRemainingMotions.ToString("D4");
    }
}
