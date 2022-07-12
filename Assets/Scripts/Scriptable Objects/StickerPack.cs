using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewStickerPack", menuName = "User Data/Sticker Pack", order = 51)]
public class StickerPack : ScriptableObject 
{
    public Sprite mainIcon;
    public List<Sprite> spriteList;
}
