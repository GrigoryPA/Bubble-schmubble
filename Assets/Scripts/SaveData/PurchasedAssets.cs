using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveData
{
    //Сериализуемый класс, описывающий приобретенные доступные активы
    [System.Serializable]
    public class PurchasedAssets
    {
        [SerializeField]
        public List<StickerPack> purchasedStickerPacks;

        public PurchasedAssets() { }

        public PurchasedAssets(StickerPack stickerPack)
        {
            purchasedStickerPacks = new List<StickerPack> (1);
            purchasedStickerPacks.Add(stickerPack);
        }

        public PurchasedAssets(List<StickerPack> purchasedStickerPacks)
        {
            this.purchasedStickerPacks = purchasedStickerPacks;
        }
    }
}
