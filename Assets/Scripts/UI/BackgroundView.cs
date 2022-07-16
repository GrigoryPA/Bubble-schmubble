using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundView : MonoBehaviour
{
    public List<Image> images;
    // Start is called before the first frame update
    void Start()
    {
        SetSpritesForImages();
    }

    public void SetSpritesForImages()
    {
        StickerPack pack = GameManager.instance.selectedPack;
        int i = 0;

        foreach (var img in images)
        {
            img.sprite = pack.spriteList[i];
            i = (i < (pack.spriteList.Count - 1)) ? i + 1 : 0;
        }
    }
}
