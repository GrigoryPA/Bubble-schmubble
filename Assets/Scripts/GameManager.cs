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
    public static int newRecordIndex = -1;
    public static SaveData.CashAccount cashAccount;

    public StickerPack selectedPack;

    public const string RECORDS_KEY = "RECORDS";
    public const string DEFAULT_RECORDS = "initializing records list";
    public const string CASH_ACCOUNT_KEY = "CASH_ACCOUNT";
    public const string START_CASH = "start cash account";

    void Awake()
    {
        if (!instance)
        {
            instance = this;
            LoadRecordsList();
            LoadCashAccount();

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

    public static void LoadRecordsList()
    {
        if (SaveManager.FindPP(RECORDS_KEY))
        {
            recordsList = SaveManager.LoadPP<SaveData.RecordsList>(RECORDS_KEY);
        }
        else
        {
            recordsList = new SaveData.RecordsList(DEFAULT_RECORDS);
            SaveManager.SavePP<SaveData.RecordsList>(RECORDS_KEY, recordsList);
        }
    }

    public static void LoadCashAccount()
    {
        if (SaveManager.FindPP(CASH_ACCOUNT_KEY))
        {
            cashAccount = SaveManager.LoadPP<SaveData.CashAccount>(CASH_ACCOUNT_KEY);
        }
        else
        {
            cashAccount = new SaveData.CashAccount(START_CASH);
            SaveManager.SavePP<SaveData.CashAccount>(CASH_ACCOUNT_KEY, cashAccount);
        }
    }

    public static void SaveCashAccount() => SaveManager.SavePP<SaveData.CashAccount>(CASH_ACCOUNT_KEY, cashAccount);
}
