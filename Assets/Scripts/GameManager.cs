using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public int maxActionsNumber = 10;
    public UnityEvent<int, int> onUpdateDataEvent;
    public UnityEvent onEndGameEvent;

    private int currentScore = 0;
    private int currentRemainingMotions = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
        currentRemainingMotions = maxActionsNumber;
        onUpdateDataEvent.Invoke(currentScore, currentRemainingMotions);
    }

    public void TakeScore(int matchCount)
    {
        currentRemainingMotions--;
        currentScore += matchCount < 3 ? 1 : matchCount - 1;
        onUpdateDataEvent.Invoke(currentScore, currentRemainingMotions);

        if (currentRemainingMotions <= 0)
        {
            onEndGameEvent.Invoke();
        }
    }
}
