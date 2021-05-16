using System;
using UnityEngine.U2D;

namespace UnityEngine.AddressableAssets
{
    [Serializable]
    public class AssetReferenceAtlas : AssetReferenceT<SpriteAtlas>
    {
        public AssetReferenceAtlas(string guid) : base(guid) { }
    }
}