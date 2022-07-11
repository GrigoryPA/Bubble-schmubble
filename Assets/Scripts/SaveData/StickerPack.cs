using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Сериализуемый класс, описывающий текущие параметры настроек игры
[System.Serializable]
public struct StickerPack
{
    [SerializeField]
    public List<Sprite> spriteList;

    public StickerPack(List<Sprite> spriteList)
    {
        this.spriteList = spriteList;
    }
}
