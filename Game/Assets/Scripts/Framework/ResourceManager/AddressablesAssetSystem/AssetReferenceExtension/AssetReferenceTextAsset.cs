using System;

namespace UnityEngine.AddressableAssets
{
    [Serializable]
    public class AssetReferenceTextAsset : AssetReferenceT<TextAsset>
    {
        public AssetReferenceTextAsset(string guid) : base(guid) { }
    }
}