using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StoreItem : MonoBehaviour
{
    public List<StickerPack> poolItems;
    public GameObject purchasePanel;
    [Header("Buying with money")]
    public Button moneyButton;
    public int moneyCost;
    [Header("Buying with crystal")]
    public Button crystalButton;
    public int crystalCost;
    [Header("Buying with video")]
    public Button videoButton;
    public int videoCost;

    public UnityEvent OnPurchaseStarted;

    private void Awake()
    {
        moneyButton.interactable = GameManager.cashAccount.money >= moneyCost;
        moneyButton.GetComponentInChildren<Text>().text = moneyCost.ToString();
        moneyButton.onClick.AddListener(SpendMoney);

        crystalButton.interactable = GameManager.cashAccount.crystal >= crystalCost;
        crystalButton.GetComponentInChildren<Text>().text = crystalCost.ToString();
        crystalButton.onClick.AddListener(SpendCrystal);


        videoButton.GetComponentInChildren<Text>().text = videoCost.ToString();
    }

    public void SpendMoney()
    {
        GameManager.cashAccount.money -= moneyCost;
        OnPurchaseStarted.Invoke();
    }

    public void SpendCrystal()
    {
        GameManager.cashAccount.crystal -= crystalCost;
        OnPurchaseStarted.Invoke();
    }
}
