using System;
using System.Collections.Generic;
using DesignPattern;
using UnityEngine;

namespace CardGameUtils
{
    [Serializable]
    public struct SpriteInfo
    {
        public string AssetName;
        public Sprite Sprite;

    }
    public class SpriteManager : Singleton<SpriteManager>
    {
        public List<SpriteInfo> SpriteInfoList;

        public Sprite GetSprite(string assetName)
        {
            foreach (SpriteInfo info in SpriteInfoList)
            {
                if (assetName == info.AssetName)
                {
                    return info.Sprite;
                }
            }
            return null;
        }
    }
}