using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//������������� �����, ����������� ������� ��������� �������� ����
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
