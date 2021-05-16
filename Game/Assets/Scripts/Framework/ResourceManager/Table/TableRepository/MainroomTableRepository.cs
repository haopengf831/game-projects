using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class MainroomTableRepository : TableRepositoryBase<int ,MainroomTableData>
{
    public override AsyncOperationHandle<TextAsset> LoadTable(string tableKey)
    {
            return Addressables.LoadAssetAsync<TextAsset>(tableKey);
    }
}