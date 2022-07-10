using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordsView : MonoBehaviour
{
    public List<Text> recordsTextsList;
    public Color selectedRecordColor;

    private void OnEnable()
    {
        int i = GameManager.recordsList.records.Length - 1;
        foreach (Text recordText in recordsTextsList)
        {
            recordText.text = GameManager.recordsList.records[i].ToString();
            
            if (GameManager.newRecordIndex == i)
            {
                GameManager.newRecordIndex = -1;
                recordText.color = selectedRecordColor;
            }

            i--;
            if (i < 0)
            {
                break;
            }
        }
    }
}
