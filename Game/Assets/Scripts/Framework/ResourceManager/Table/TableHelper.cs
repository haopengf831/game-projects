using System.Collections.Generic;
using System.Threading.Tasks;
using Loxodon.Framework.Execution;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class TableHelper
{
    private static Dictionary<string, AsyncOperationHandle<TextAsset>> m_DicAsyncOperationHandles = new Dictionary<string, AsyncOperationHandle<TextAsset>>();
    private static int m_ParserCount;
    public static readonly Dictionary<string, Dictionary<int, List<string>>> DicAllTables = new Dictionary<string, Dictionary<int, List<string>>>();
    public static readonly Dictionary<string, Dictionary<int, Dictionary<int, string>>> DicAll = new Dictionary<string, Dictionary<int, Dictionary<int, string>>>();

    /// <summary>
    /// 开始读表 (异步) 开发及上线时调用
    /// </summary>
    /// <returns></returns>
    public static async Task ReadTable()
    {
        m_ParserCount = 0;
        if (m_DicAsyncOperationHandles == null)
        {
            m_DicAsyncOperationHandles = new Dictionary<string, AsyncOperationHandle<TextAsset>>();
        }
        m_DicAsyncOperationHandles.Add("MainRoom", Addressables.LoadAssetAsync<TextAsset>("MainRoom"));
        await WaitTask();
    }

    /// <summary>
    /// 等待读表结果
    /// </summary>
    /// <returns></returns>
    private static async Task WaitTask()
    {
        foreach (var pair in m_DicAsyncOperationHandles)
        {
            await pair.Value.Task;
            if (pair.Value.Status == AsyncOperationStatus.Succeeded)
            {
                string content = pair.Value.Result.text;
                Executors.RunAsync(() => TxtParser.Read(pair.Key, content)).Callbackable().OnCallback(tableResult =>
                {
                    if (tableResult.Exception == null)
                    {
                        DicAllTables[tableResult.Result.TableName] = tableResult.Result.DicTableContent;
                        DicAll[tableResult.Result.TableName] = tableResult.Result.DicTable;
                    }
                    m_ParserCount++;
                    if (m_ParserCount == m_DicAsyncOperationHandles.Count)
                    {
                        foreach (var handle in m_DicAsyncOperationHandles.Values)
                        {
                            Addressables.Release(handle);
                        }
                        m_DicAsyncOperationHandles.Clear();
                        m_DicAsyncOperationHandles = null;
                    }
                });
            }
        }
    }
}