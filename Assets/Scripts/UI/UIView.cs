using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIView : MonoBehaviour
{
    public Text scoreText;
    public Text motionsText;
    private string scoreStr = "Очки: ";
    private string motionsStr = "Шаги: ";
    private int digitsNumber = 4;

    public void SetScoreAndMotionsText(int score, int motions)
    {
        scoreText.text = scoreStr + score.ToString("D" + digitsNumber.ToString());
        motionsText.text = motionsStr + motions.ToString("D" + digitsNumber.ToString());
    }

    public void SetResultText(Text resultText)
    {
        resultText.text = scoreText.text.Substring(scoreStr.Length, digitsNumber);
    }

    public void SetResultMoneyText(Text resultText)
    {
        resultText.text = "+" + GameManager.cashAccount.money.ToString();
    }
} 
