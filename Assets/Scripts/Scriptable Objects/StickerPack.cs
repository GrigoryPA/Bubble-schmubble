using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewStickerPack", menuName = "User Data/Sticker Pack", order = 51)]
public class StickerPack : ScriptableObject 
{
    [SerializeField]
    public Sprite mainIcon;
    [SerializeField]
    public List<Sprite> spriteList;
}
