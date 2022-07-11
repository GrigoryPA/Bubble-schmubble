using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StickerPackController : MonoBehaviour
{
    public StickerPack stikerPack;
    private Button button;

    private void Awake()
    {
        button = GetComponentInChildren<Button>();
    }

    public void SelectThisStickerPack()
    {
        GameManager.instance.selectedPack = stikerPack;
    }
}
