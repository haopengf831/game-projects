using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Loxodon.Framework.Asynchronous;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public abstract class SceneLocator : AssetLocator
{
    public static void LoadSceneAsync(string sceneKey, LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = false, int priority = 100,
        Action<SceneInstance> succeededAction = null, Action failedAction = null)
    {
        if (string.IsNullOrWhiteSpace(sceneKey))
        {
            failedAction?.Invoke();
            return;
        }

        LoadSceneAsyncByCoroutine(sceneKey, loadMode, activateOnLoad, priority, result => { succeededAction?.Invoke(result); }, failedAction);
    }

    public static async Task LoadSceneAsyncWithTask(string sceneKey, LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = false, int priority = 100,
        Action<SceneInstance> succeededAction = null, Action failedAction = null)
    {
        if (string.IsNullOrWhiteSpace(sceneKey))
        {
            failedAction?.Invoke();
            return;
        }

        await LoadSceneAsyncByTask(sceneKey, loadMode, activateOnLoad, priority, result => { succeededAction?.Invoke(result); }, failedAction);
    }

    public static void UnLoadSceneAsync(SceneInstance sceneInstance, bool autoReleaseHandle = false, Action<SceneInstance> succeededAction = null, Action failedAction = null)
    {
        UnLoadSceneAsyncByCoroutine(sceneInstance, autoReleaseHandle, result => { succeededAction?.Invoke(result); }, failedAction);
    }

    public static async void UnLoadSceneAsyncWithTask(SceneInstance sceneInstance, bool autoReleaseHandle = false, Action<SceneInstance> succeededAction = null,
        Action failedAction = null)
    {
        await UnLoadSceneAsyncByTask(sceneInstance, autoReleaseHandle, result => { succeededAction?.Invoke(result); }, failedAction);
    }

    public static IProgressTask<float, SceneInstance> LoadSceneAsyncWithProgress(string sceneKey, LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = true,
        int priority = 100, Action<SceneInstance> action = null)
    {
        ProgressTask<float, SceneInstance> task = new ProgressTask<float, SceneInstance>(p => DoLoad(p, sceneKey, loadMode, activateOnLoad, priority, action));
        return task.Start(30);
    }

    private static IEnumerator DoLoad(IProgressPromise<float, SceneInstance> promise, string sceneKey, LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = true,
        int priority = 100, Action<SceneInstance> action = null)
    {
        var asyncOperation = LoadSceneAsyncOperationHandle(sceneKey, loadMode, activateOnLoad, priority);
        while (!asyncOperation.IsDone)
        {
            promise.UpdateProgress(asyncOperation.PercentComplete);
            yield return null;
        }

        promise.UpdateProgress(1f);
        promise.SetResult(asyncOperation.Result);
    }
}