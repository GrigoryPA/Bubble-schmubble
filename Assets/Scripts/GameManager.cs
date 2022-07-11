using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static SaveData.RecordsList recordsList;
    public const string defaultRecords = "initializing records list";
    public static int newRecordIndex = -1;
    public StickerPack selectedPack;

    void Awake()
    {
        if (!instance)
        {
            PlayerPrefs.DeleteKey("RECORDS");
            instance = this;
            LoadRecordsList();
            SceneManager.sceneLoaded += this.OnLoadCallback;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {
    }

    public void LoadRecordsList()
    {
        if (SaveManager.FindPP(SaveData.RecordsList.KEY))
        {
            recordsList = SaveManager.LoadPP<SaveData.RecordsList>(SaveData.RecordsList.KEY);
        }
        else
        {
            recordsList = new SaveData.RecordsList(defaultRecords);
            SaveManager.SavePP<SaveData.RecordsList>(SaveData.RecordsList.KEY, recordsList);
        }
    }
}
