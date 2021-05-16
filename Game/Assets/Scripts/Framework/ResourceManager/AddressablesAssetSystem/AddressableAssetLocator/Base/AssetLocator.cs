using System;
using System.Collections;
using System.Threading.Tasks;
using Loxodon.Framework.Asynchronous;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public abstract class AssetLocator
{
    protected static Exception Exception => throw new Exception();

    #region 异步加载Asset(除Scene场景以外的所有Unity支持的资源，加载预制体调用这个只是加载进内存，并不是实例化)

    public static async Task LoadAssetAsyncByTask<TObject>(IResourceLocation location,
        Action<TObject> succeededAction = null, Action failedAction = null)
    {
        AsyncOperationHandle<TObject> handle = LoadAssetAsyncOperationHandle<TObject>(location);
        await handle.Task;
        HandleCompleteCallBack(handle, succeededAction, failedAction);
    }

    public static void LoadAssetAsyncByCoroutine<TObject>(IResourceLocation location,
        Action<TObject> succeededAction = null, Action failedAction = null)
    {
        CoroutineTask.Run(LoadAssetCoroutine(location, succeededAction, failedAction));
    }

    private static IEnumerator LoadAssetCoroutine<TObject>(IResourceLocation location, Action<TObject> succeededAction = null,
        Action failedAction = null)
    {
        AsyncOperationHandle<TObject> handle = LoadAssetAsyncOperationHandle<TObject>(location);
        yield return handle;
        HandleCompleteCallBack(handle, succeededAction, failedAction);
    }

    public static async Task LoadAssetAsyncByTask<TObject>(object key, Action<TObject> succeededAction = null,
        Action failedAction = null)
    {
        AsyncOperationHandle<TObject> handle = LoadAssetAsyncOperationHandle<TObject>(key);
        await handle.Task;
        HandleCompleteCallBack(handle, succeededAction, failedAction);
    }

    public static void LoadAssetAsyncByCoroutine<TObject>(object key, Action<TObject> succeededAction = null,
        Action failedAction = null)
    {
        CoroutineTask.Run(LoadAssetCoroutine(key, succeededAction, failedAction));
    }

    private static IEnumerator LoadAssetCoroutine<TObject>(object key, Action<TObject> succeededAction = null,
        Action failedAction = null)
    {
        AsyncOperationHandle<TObject> handle = LoadAssetAsyncOperationHandle<TObject>(key);
        yield return handle;
        HandleCompleteCallBack(handle, succeededAction, failedAction);
    }

    protected static AsyncOperationHandle<TObject> LoadAssetAsyncOperationHandle<TObject>(IResourceLocation location)
    {
        return Addressables.LoadAssetAsync<TObject>(location);
    }

    protected static AsyncOperationHandle<TObject> LoadAssetAsyncOperationHandle<TObject>(object key)
    {
        return Addressables.LoadAssetAsync<TObject>(key);
    }

    #endregion

    #region 异步加载并实例化GameObject

    protected static async Task InstantiateAsyncByTask(IResourceLocation location,
        Action<GameObject> succeededAction = null, Action failedAction = null)
    {
        AsyncOperationHandle<GameObject> handle = InstantiateAsyncOperationHandle(location);
        await handle.Task;
        HandleCompleteCallBack(handle, succeededAction, failedAction);
    }

    protected static void InstantiateAsyncByCoroutine(IResourceLocation location,
        Action<GameObject> succeededAction = null, Action failedAction = null)
    {
        CoroutineTask.Run(InstantiateCoroutine(location, succeededAction, failedAction));
    }

    private static IEnumerator InstantiateCoroutine(IResourceLocation location, Action<GameObject> succeededAction = null,
        Action failedAction = null)
    {
        AsyncOperationHandle<GameObject> handle = InstantiateAsyncOperationHandle(location);
        yield return handle;
        HandleCompleteCallBack(handle, succeededAction, failedAction);
    }

    protected static async Task InstantiateAsyncByTask(object key, Action<GameObject> succeededAction = null,
        Action failedAction = null)
    {
        AsyncOperationHandle<GameObject> handle = InstantiateAsyncOperationHandle(key);
        await handle.Task;
        HandleCompleteCallBack(handle, succeededAction, failedAction);
    }

    protected static void InstantiateAsyncByCoroutine(object key, Action<GameObject> succeededAction = null,
        Action failedAction = null)
    {
        CoroutineTask.Run(InstantiateCoroutine(key, succeededAction, failedAction));
    }

    private static IEnumerator InstantiateCoroutine(object key, Action<GameObject> succeededAction = null,
        Action failedAction = null)
    {
        AsyncOperationHandle<GameObject> handle = InstantiateAsyncOperationHandle(key);
        yield return handle;
        HandleCompleteCallBack(handle, succeededAction, failedAction);
    }

    private static AsyncOperationHandle<GameObject> InstantiateAsyncOperationHandle(IResourceLocation location)
    {
        return Addressables.InstantiateAsync(location);
    }

    private static AsyncOperationHandle<GameObject> InstantiateAsyncOperationHandle(object key)
    {
        return Addressables.InstantiateAsync(key);
    }

    #endregion

    #region 异步加载场景与卸载场景

    protected static async Task LoadSceneAsyncByTask(IResourceLocation location,
        LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = false, int priority = 100,
        Action<SceneInstance> succeededAction = null, Action failedAction = null)
    {
        AsyncOperationHandle<SceneInstance> handle =
            LoadSceneAsyncOperationHandle(location, loadMode, activateOnLoad, priority);
        await handle.Task;
        HandleCompleteCallBack(handle, succeededAction, failedAction);
    }

    protected static void LoadSceneAsyncByCoroutine(IResourceLocation location,
        LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = false, int priority = 100,
        Action<SceneInstance> succeededAction = null, Action failedAction = null)
    {
        CoroutineTask.Run(LoadSceneCoroutine(location, loadMode, activateOnLoad, priority, succeededAction,
            failedAction));
    }

    private static IEnumerator LoadSceneCoroutine(IResourceLocation location, LoadSceneMode loadMode = LoadSceneMode.Single,
        bool activateOnLoad = false, int priority = 100, Action<SceneInstance> succeededAction = null,
        Action failedAction = null)
    {
        AsyncOperationHandle<SceneInstance> handle =
            LoadSceneAsyncOperationHandle(location, loadMode, activateOnLoad, priority);
        yield return handle;
        HandleCompleteCallBack(handle, succeededAction, failedAction);
    }

    protected static async Task LoadSceneAsyncByTask(object key, LoadSceneMode loadMode = LoadSceneMode.Single,
        bool activateOnLoad = false, int priority = 100, Action<SceneInstance> succeededAction = null,
        Action failedAction = null)
    {
        AsyncOperationHandle<SceneInstance> handle =
            LoadSceneAsyncOperationHandle(key, loadMode, activateOnLoad, priority);
        await handle.Task;
        HandleCompleteCallBack(handle, succeededAction, failedAction);
    }

    protected static void LoadSceneAsyncByCoroutine(object key, LoadSceneMode loadMode = LoadSceneMode.Single,
        bool activateOnLoad = false, int priority = 100, Action<SceneInstance> succeededAction = null,
        Action failedAction = null)
    {
        CoroutineTask.Run(LoadSceneCoroutine(key, loadMode, activateOnLoad, priority, succeededAction, failedAction));
    }

    private static IEnumerator LoadSceneCoroutine(object key, LoadSceneMode loadMode = LoadSceneMode.Single,
        bool activateOnLoad = false, int priority = 100, Action<SceneInstance> succeededAction = null,
        Action failedAction = null)
    {
        AsyncOperationHandle<SceneInstance> handle =
            LoadSceneAsyncOperationHandle(key, loadMode, activateOnLoad, priority);
        yield return handle;
        HandleCompleteCallBack(handle, succeededAction, failedAction);
    }

    private static AsyncOperationHandle<SceneInstance> LoadSceneAsyncOperationHandle(IResourceLocation location,
        LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = false, int priority = 100)
    {
        return Addressables.LoadSceneAsync(location, loadMode, activateOnLoad, priority);
    }

    protected static AsyncOperationHandle<SceneInstance> LoadSceneAsyncOperationHandle(object key,
        LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = false, int priority = 100)
    {
        return Addressables.LoadSceneAsync(key, loadMode, activateOnLoad, priority);
    }

    protected static async Task UnLoadSceneAsyncByTask(SceneInstance sceneInstance, bool autoReleaseHandle = false,
        Action<SceneInstance> succeededAction = null, Action failedAction = null)
    {
        AsyncOperationHandle<SceneInstance> handle = UnloadSceneAsyncOperationHandle(sceneInstance, autoReleaseHandle);
        await handle.Task;
        HandleCompleteCallBack(handle, succeededAction, failedAction);
        Addressables.Release(handle);
    }

    protected static void UnLoadSceneAsyncByCoroutine(SceneInstance sceneInstance, bool autoReleaseHandle = false,
        Action<SceneInstance> succeededAction = null, Action failedAction = null)
    {
        CoroutineTask.Run(UnLoadSceneCoroutine(sceneInstance, autoReleaseHandle, succeededAction, failedAction));
    }

    private static IEnumerator UnLoadSceneCoroutine(SceneInstance sceneInstance, bool autoReleaseHandle = false,
        Action<SceneInstance> succeededAction = null, Action failedAction = null)
    {
        AsyncOperationHandle<SceneInstance> handle = UnloadSceneAsyncOperationHandle(sceneInstance, autoReleaseHandle);
        yield return handle;
        HandleCompleteCallBack(handle, succeededAction, failedAction);
        Addressables.Release(handle);
    }

    private static AsyncOperationHandle<SceneInstance> UnloadSceneAsyncOperationHandle(SceneInstance sceneInstance,
        bool autoReleaseHandle = false)
    {
        return Addressables.UnloadSceneAsync(sceneInstance, autoReleaseHandle);
    }

    #endregion

    private static void HandleCompleteCallBack<TObject>(AsyncOperationHandle<TObject> handle,
        Action<TObject> succeededAction = null, Action failedAction = null)
    {
        if (!handle.IsValid())
        {
            failedAction?.Invoke();
            return;
        }

        switch (handle.Status)
        {
            case AsyncOperationStatus.Succeeded:
            {
                succeededAction?.Invoke(handle.Result);
            }
                break;
            case AsyncOperationStatus.Failed:
            {
                failedAction?.Invoke();
            }
                break;
            default:
            {
                Addressables.LogException(handle, Exception);
                throw Exception;
            }
        }
    }

    #region 释放指定资源的内存

    public static void ReleaseAsset<TObject>(TObject obj)
    {
        Addressables.Release(obj);
    }

    #endregion

    #region 释放并销毁GameObject

    public static void ReleaseInstance(GameObject obj)
    {
        if (Addressables.ReleaseInstance(obj))
        {
            Object.Destroy(obj);
        }
    }

    #endregion
}