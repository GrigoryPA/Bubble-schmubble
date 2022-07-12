using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StickerPackController : MonoBehaviour
{
    public StickerPack stikerPack;

    private void Start()
    {
        Image image = gameObject.GetComponentInChildren<Image>();
        image.sprite = stikerPack.mainIcon;
    }

    public void SetStickerPackAsUsed()
    {
        GameManager.instance.selectedPack = stikerPack;
    }
}
