using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

/// <summary>
/// Unity GameObject类的扩展方法
/// </summary>
public static partial class GameObjectExtension
{
    /// <summary>
    /// 建议优先使用此方法, 释放通过Addressables系统加载的实例, 如果不是通过Addressables系统加载的实例则调用Object.Destroy, 建议优先使用此方法
    /// </summary>
    /// <param name="gameObject"></param>
    public static void Release(this GameObject gameObject)
    {
        if (gameObject == null)
        {
            return;
        }

        if (Addressables.ReleaseInstance(gameObject))
        {
            return;
        }

        Object.Destroy(gameObject);
    }

    /// <summary>
    /// 建议优先使用此方法, 释放通过Addressables系统加载的实例, 如果不是通过Addressables系统加载的实例则调用Object.DestroyImmediate, 建议优先使用此方法
    /// </summary>
    /// <param name="gameObject"></param>
    public static void ReleaseImmediate(this GameObject gameObject)
    {
        if (gameObject == null)
        {
            return;
        }

        if (Addressables.ReleaseInstance(gameObject))
        {
            return;
        }

        Object.DestroyImmediate(gameObject);
    }

    /// <summary>
    /// 获取指定对象的所有子节点, 不包含父节点本身
    /// </summary>
    /// <param name="gameObject"></param>
    /// <typeparam name="TChild"></typeparam>
    /// <returns></returns>
    public static IEnumerable<TChild> GetComponentsInChildrenWithoutSelf<TChild>(this GameObject gameObject)
    {
        if (gameObject == null)
        {
            throw new ArgumentNullException(nameof(gameObject));
        }

        var childList = new List<TChild>();
        foreach (Transform childTransform in gameObject.transform)
        {
            if (childTransform.gameObject.GetInstanceID() != gameObject.GetInstanceID())
            {
                if (childTransform.TryGetComponent<TChild>(out var child))
                {
                    childList.Add(child);
                }
            }
        }

        return childList;
    }

    /// <summary>
    /// 获取指定对象Active的所有子节点, 不包含父节点本身
    /// </summary>
    /// <param name="gameObject"></param>
    /// <typeparam name="TChild"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<TChild> GetComponentsInChildrenOnlyActiveWithoutSelf<TChild>(this GameObject gameObject)
    {
        if (gameObject == null)
        {
            throw new ArgumentNullException(nameof(gameObject));
        }

        var childList = new List<TChild>();
        var childTransformList = gameObject.GetComponentsInChildrenWithoutSelf<Transform>();
        foreach (var childTransform in childTransformList)
        {
            if (childTransform.gameObject.activeSelf)
            {
                if (childTransform.TryGetComponent<TChild>(out var child))
                {
                    childList.Add(child);
                }
            }
        }

        return childList;
    }

    /// <summary>
    /// 获取指定对象InActive的所有子节点, 不包含父节点本身
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<TChild> GetComponentsInChildrenOnlyInActiveWithoutSelf<TChild>(this GameObject gameObject)
    {
        if (gameObject == null)
        {
            throw new ArgumentNullException(nameof(gameObject));
        }

        var childList = new List<TChild>();
        var childTransformList = gameObject.GetComponentsInChildrenWithoutSelf<Transform>();
        foreach (var childTransform in childTransformList)
        {
            if (!childTransform.gameObject.activeSelf)
            {
                if (childTransform.TryGetComponent<TChild>(out var child))
                {
                    childList.Add(child);
                }
            }
        }

        return childList;
    }

    /// <summary>
    /// SetActive(true)指定对象下的所有子节点
    /// </summary>
    /// <param name="gameObject"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void SetActiveChildren(this GameObject gameObject)
    {
        if (gameObject == null)
        {
            throw new ArgumentNullException(nameof(gameObject));
        }

        var childList = gameObject.GetComponentsInChildrenOnlyInActiveWithoutSelf<Transform>();
        foreach (var child in childList)
        {
            child.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// SetActive(false)指定对象下的所有子节点
    /// </summary>
    /// <param name="gameObject"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void SetInactiveChildren(this GameObject gameObject)
    {
        if (gameObject == null)
        {
            throw new ArgumentNullException(nameof(gameObject));
        }

        var childList = gameObject.GetComponentsInChildrenOnlyActiveWithoutSelf<Transform>();
        foreach (var child in childList)
        {
            child.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 销毁指定对象下的所有子节点
    /// </summary>
    /// <param name="gameObject"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void DestroyChildren(this GameObject gameObject)
    {
        if (gameObject == null)
        {
            throw new ArgumentNullException(nameof(gameObject));
        }

        var childList = gameObject.GetComponentsInChildrenWithoutSelf<Transform>();
        foreach (var child in childList)
        {
            child.Release();
        }
    }

    /// <summary>
    /// 立即销毁指定对象下的所有子节点
    /// </summary>
    /// <param name="gameObject"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void DestroyChildrenImmediate(this GameObject gameObject)
    {
        if (gameObject == null)
        {
            throw new ArgumentNullException(nameof(gameObject));
        }

        var childList = gameObject.GetComponentsInChildrenWithoutSelf<Transform>();
        foreach (var child in childList)
        {
            child.gameObject.ReleaseImmediate();
        }
    }

    /// <summary>
    /// 是否含有指定组件
    /// </summary>
    /// <param name="gameObject"></param>
    /// <typeparam name="TComponent"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static bool ContainsComponent<TComponent>(this GameObject gameObject) where TComponent : Component
    {
        if (gameObject == null)
        {
            throw new ArgumentNullException(nameof(gameObject));
        }

        return gameObject.TryGetComponent<TComponent>(out var component);
    }

    /// <summary>
    /// 是否含有指定组件
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static bool ContainsComponent(this GameObject gameObject, Type type)
    {
        if (gameObject == null)
        {
            throw new ArgumentNullException(nameof(gameObject));
        }

        return gameObject.TryGetComponent(type, out var component);
    }

    /// <summary>
    /// 获取或增加组件。
    /// </summary>
    /// <typeparam name="TComponent">要获取或增加的组件。</typeparam>
    /// <param name="gameObject">目标对象。</param>
    /// <returns>获取或增加的组件。</returns>
    public static TComponent RequireComponent<TComponent>(this GameObject gameObject) where TComponent : Component
    {
        if (gameObject == null)
        {
            throw new ArgumentNullException(nameof(gameObject));
        }

        if (!gameObject.TryGetComponent<TComponent>(out var component))
        {
            component = gameObject.AddComponent<TComponent>();
        }

        return component;
    }

    /// <summary>
    /// 获取或增加组件。
    /// </summary>
    /// <param name="gameObject">目标对象。</param>
    /// <param name="type">要获取或增加的组件类型。</param>
    /// <returns>获取或增加的组件。</returns>
    public static Component RequireComponent(this GameObject gameObject, Type type)
    {
        if (gameObject == null)
        {
            throw new ArgumentNullException(nameof(gameObject));
        }

        if (!gameObject.TryGetComponent(type, out var component))
        {
            component = gameObject.AddComponent(type);
        }

        return component;
    }

    /// <summary>
    /// 移除指定组件
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <param name="gameObject"></param>
    public static void RemoveComponent<TComponent>(this GameObject gameObject) where TComponent : Component
    {
        if (gameObject == null)
        {
            throw new ArgumentNullException(nameof(gameObject));
        }

        if (gameObject.TryGetComponent<TComponent>(out var destroyComponent))
        {
            Object.Destroy(destroyComponent);
        }
    }

    /// <summary>
    /// 移除指定组件
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="type"></param>
    public static void RemoveComponent(this GameObject gameObject, Type type)
    {
        if (gameObject == null)
        {
            throw new ArgumentNullException(nameof(gameObject));
        }

        if (gameObject.TryGetComponent(type, out var destroyComponent))
        {
            Object.Destroy(destroyComponent);
        }
    }

    /// <summary>
    /// 移除所有指定组件
    /// </summary>
    /// <param name="gameObject"></param>
    /// <typeparam name="TComponent"></typeparam>
    /// <exception cref="ArgumentNullException"></exception>
    public static void RemoveComponents<TComponent>(this GameObject gameObject) where TComponent : Component
    {
        if (gameObject == null)
        {
            throw new ArgumentNullException(nameof(gameObject));
        }

        var components = gameObject.GetComponents<TComponent>();
        if (components == null || components.Length <= 0)
        {
            return;
        }

        foreach (var component in components)
        {
            Object.Destroy(component);
        }
    }

    /// <summary>
    /// 移除所有指定组件
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="type"></param>
    public static void RemoveComponents(this GameObject gameObject, Type type)
    {
        if (gameObject == null)
        {
            throw new ArgumentNullException(nameof(gameObject));
        }

        var components = gameObject.GetComponents(type);
        if (components == null || components.Length <= 0)
        {
            return;
        }

        foreach (var component in components)
        {
            Object.Destroy(component);
        }
    }

    /// <summary>
    /// 获取 GameObject 是否在场景中。
    /// </summary>
    /// <param name="gameObject">目标对象。</param>
    /// <returns>GameObject 是否在场景中。</returns>
    /// <remarks>若返回 true，表明此 GameObject 是一个场景中的实例对象；若返回 false，表明此 GameObject 是一个 Prefab。</remarks>
    public static bool IsInScene(this GameObject gameObject)
    {
        if (gameObject == null)
        {
            throw new ArgumentNullException(nameof(gameObject));
        }

        return gameObject.scene.name != null;
    }

    /// <summary>
    /// 递归设置游戏对象的层次。
    /// </summary>
    /// <param name="gameObject"><see cref="UnityEngine.GameObject" /> 对象。</param>
    /// <param name="layer">目标层次的编号。</param>
    public static void SetLayerRecursively(this GameObject gameObject, int layer)
    {
        if (gameObject == null)
        {
            throw new ArgumentNullException(nameof(gameObject));
        }

        var tempChildrenTransform = gameObject.GetComponentsInChildrenWithoutSelf<Transform>();
        foreach (var childTransform in tempChildrenTransform)
        {
            childTransform.gameObject.layer = layer;
        }
    }

    /// <summary>
    /// 获取RectTransform组件
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public static RectTransform GetRectTransform(this GameObject gameObject)
    {
        if (gameObject == null)
        {
            throw new ArgumentNullException(nameof(gameObject));
        }

        return gameObject.TryGetComponent<RectTransform>(out var rectTransform) ? rectTransform : null;
    }

    /// <summary>
    /// 获取GameObject上的所有组件
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public static List<Component> GetComponentsList(this GameObject gameObject)
    {
        if (gameObject == null)
        {
            throw new ArgumentNullException(nameof(gameObject));
        }

        return gameObject.GetComponents<Component>().ToList();
    }

    /// <summary>
    /// 获取GameObject的Size
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Vector3 GetSize(this GameObject gameObject)
    {
        if (gameObject == null)
        {
            throw new ArgumentNullException(nameof(gameObject));
        }

        var size = Vector3.zero;
        if (gameObject.TryGetComponent<RectTransform>(out var rectTransform))
        {
            size = rectTransform.rect.size;
        }
        else
        {
            if (gameObject.TryGetComponent<Renderer>(out var renderer))
            {
                size = renderer.bounds.size;
            }
            else
            {
                if (gameObject.TryGetComponent<Collider>(out var collider))
                {
                    size = collider.bounds.size;
                }
                else
                {
                    if (gameObject.TryGetComponent<MeshFilter>(out var meshFilter))
                    {
                        if (meshFilter.mesh != null)
                        {
                            size = meshFilter.mesh.bounds.size;
                        }
                        else
                        {
                            if (meshFilter.sharedMesh != null)
                            {
                                size = meshFilter.sharedMesh.bounds.size;
                            }
                        }
                    }
                }
            }
        }

        return size.Multiply(gameObject.transform.lossyScale);
    }
}