using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StoreItem : MonoBehaviour
{
    public List<StickerPack> poolItems;
    //public PurchasePanelView purchasePanel;
    public Text infoText;
    [Multiline(10)]
    public string info;
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

    private RewardedAds rewardedVideo;

    private void Awake()
    {
        infoText.text = info;
        UpdateButtonsInteraction();

        moneyButton.GetComponentInChildren<Text>().text = moneyCost.ToString();
        moneyButton.onClick.AddListener(SpendMoney);

        crystalButton.GetComponentInChildren<Text>().text = crystalCost.ToString();
        crystalButton.onClick.AddListener(SpendCrystal);

        rewardedVideo = videoButton.GetComponent<RewardedAds>();
        rewardedVideo.onShowCompleted.AddListener(TakeShowedVideo);
        rewardedVideo.countShows = videoCost;
        videoButton.GetComponentInChildren<Text>().text = (videoCost - rewardedVideo.countShows).ToString() + "/" + videoCost.ToString();
    }

    private void UpdateButtonsInteraction()
    {
        moneyButton.interactable = GameManager.cashAccount.money >= moneyCost;
        crystalButton.interactable = GameManager.cashAccount.crystal >= crystalCost;
    }

    public void SpendMoney()
    {
        GameManager.cashAccount.money -= moneyCost;
        UpdateButtonsInteraction();
        OnPurchaseStarted.Invoke();
    }

    public void SpendCrystal()
    {
        GameManager.cashAccount.crystal -= crystalCost;
        UpdateButtonsInteraction();
        OnPurchaseStarted.Invoke();
    }

    public void TakeShowedVideo()
    {
        if (rewardedVideo.countShows <= 0) //������ ���=�� ���������� ����������
        {
            rewardedVideo.countShows = videoCost;
            OnPurchaseStarted.Invoke();
        }

        videoButton.GetComponentInChildren<Text>().text = (videoCost - rewardedVideo.countShows).ToString() + "/" + videoCost.ToString();
    }
}
