using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchasePanelView : MonoBehaviour
{
    //public Animator animator;
    public Image icon;

    private StickerPack reward;

    public void StartPurchasing(StoreItem item)
    {
        gameObject.SetActive(true);
        reward = item.poolItems[Random.Range(0, item.poolItems.Count)];
        icon.sprite = reward.mainIcon;
        TakeNewStickerPack();
    }

    private void TakeNewStickerPack()
    {
        if (!GameManager.purchasedAssets.purchasedStickerPacks.Contains(reward))
        {
            GameManager.purchasedAssets.purchasedStickerPacks.Add(reward);
            GameManager.SavePurchasedAssets();
        }
    }
}
