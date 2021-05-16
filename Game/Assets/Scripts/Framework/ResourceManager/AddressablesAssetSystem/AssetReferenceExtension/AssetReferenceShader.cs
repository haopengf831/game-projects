using System;

namespace UnityEngine.AddressableAssets
{
    [Serializable]
    public class AssetReferenceShader : AssetReferenceT<Shader>
    {
        public AssetReferenceShader(string guid) : base(guid) { }
    }
}