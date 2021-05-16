using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceLocations;

public abstract class GameObjectLocator : AssetLocator
{
    protected static readonly List<GameObject> GameObjects = new List<GameObject>();

    public static void InstantiateAsync(string prefabKey, Action<GameObject> succeededAction = null,
        Action failedAction = null)
    {
        if (string.IsNullOrWhiteSpace(prefabKey))
        {
            failedAction?.Invoke();
            return;
        }

        InstantiateAsyncByCoroutine(prefabKey, result =>
        {
            GameObjects?.Add(result);
            succeededAction?.Invoke(result);
        }, failedAction);
    }

    public static async Task InstantiateAsyncWithTask(string prefabKey, Action<GameObject> succeededAction = null,
        Action failedAction = null)
    {
        if (string.IsNullOrWhiteSpace(prefabKey))
        {
            failedAction?.Invoke();
            return;
        }

        await InstantiateAsyncByTask(prefabKey, result =>
        {
            GameObjects?.Add(result);
            succeededAction?.Invoke(result);
        }, failedAction);
    }

    public static void InstantiateAsync(IResourceLocation location, Action<GameObject> succeededAction = null,
        Action failedAction = null)
    {
        if (location == null)
        {
            failedAction?.Invoke();
            return;
        }

        InstantiateAsyncByCoroutine(location, result =>
        {
            GameObjects?.Add(result);
            succeededAction?.Invoke(result);
        }, failedAction);
    }

    public static async Task InstantiateAsyncWithTask(IResourceLocation location, Action<GameObject> succeededAction = null,
        Action failedAction = null)
    {
        if (location == null)
        {
            failedAction?.Invoke();
            return;
        }

        await InstantiateAsyncByTask(location, result =>
        {
            GameObjects?.Add(result);
            succeededAction?.Invoke(result);
        }, failedAction);
    }

    public static void Destroy(GameObject obj)
    {
        if (obj == null)
        {
            return;
        }

        if (GameObjects != null && GameObjects.Contains(obj))
        {
            GameObjects.Remove(obj);
        }

        ReleaseInstance(obj);
    }

    public static void DestroyAll()
    {
        GameObjects?.ForEach(Destroy);
        GameObjects?.Clear();
    }

    public static void Release(GameObject obj)
    {
        ReleaseAsset(obj);
    }

    public static void ReleaseAll()
    {
        GameObjects?.ForEach(Release);
        GameObjects?.Clear();
    }
}