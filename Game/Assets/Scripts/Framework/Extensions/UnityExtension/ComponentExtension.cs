using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity Component类的扩展方法
/// </summary>
public static partial class ComponentExtension
{
    /// <summary>
    /// ActivatesDeactivates the GameObject, depending on the given true or false/ value.
    /// </summary>
    /// <param name="component"></param>
    /// <param name="value"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void SetActive(this Component component, bool value)
    {
        if (component == null)
        {
            throw new ArgumentNullException(nameof(component));
        }

        component.gameObject.SetActive(value);
    }

    /// <summary>
    /// 建议优先使用此方法, 释放通过Addressables系统加载的实例, 如果不是通过Addressables系统加载的实例则调用Object.Destroy, 建议优先使用此方法
    /// </summary>
    /// <param name="component"></param>
    public static void Release(this Component component)
    {
        if (component == null)
        {
            return;
        }

        component.gameObject.Release();
    }

    /// <summary>
    /// 建议优先使用此方法, 释放通过Addressables系统加载的实例, 如果不是通过Addressables系统加载的实例则调用Object.DestroyImmediate, 建议优先使用此方法
    /// </summary>
    /// <param name="component"></param>
    public static void ReleaseImmediate(this Component component)
    {
        if (component == null)
        {
            return;
        }

        component.gameObject.ReleaseImmediate();
    }

    /// <summary>
    /// 在指定转换的祖先中查找类型为< typeparamref name="TComponent"/>的第一个组件
    /// </summary>
    /// <typeparam name="TComponent">Type of component to find.</typeparam>
    /// <param name="component">Transform for which ancestors must be considered.</param>
    /// <param name="includeSelf">Indicates whether the specified transform should be included.</param>
    /// <returns>The component of type <typeparamref name="TComponent"/>. Null if it none was found.</returns>
    public static TComponent GetAncestorComponent<TComponent>(this Component component, bool includeSelf = true) where TComponent : Component
    {
        if (component == null)
        {
            throw new ArgumentNullException(nameof(component));
        }

        foreach (var transform in component.transform.EnumerateAncestors(includeSelf))
        {
            if (transform.TryGetComponent<TComponent>(out var ancestorComponent))
            {
                return ancestorComponent;
            }
        }

        return null;
    }

    /// <summary>
    /// 在指定转换的祖先中查找类型为type的第一个组件
    /// </summary>
    /// <param name="type">Type of component to find.</param>
    /// <param name="component">Transform for which ancestors must be considered.</param>
    /// <param name="includeSelf">Indicates whether the specified transform should be included.</param>
    /// <returns>The component. Null if it none was found.</returns>
    public static Component GetAncestorComponent(this Component component, Type type, bool includeSelf = true)
    {
        if (component == null)
        {
            throw new ArgumentNullException(nameof(component));
        }

        foreach (var transform in component.transform.EnumerateAncestors(includeSelf))
        {
            if (transform.TryGetComponent(type, out var ancestorComponent))
            {
                return ancestorComponent;
            }
        }

        return null;
    }

    /// <summary>
    /// 尝试在指定转换的祖先中查找类型为< typeparamref name="TComponent"/>的第一个组件
    /// </summary>
    /// <typeparam name="TComponent">Type of component to find.</typeparam>
    /// <param name="component">Transform for which ancestors must be considered.</param>
    /// <param name="includeSelf">Indicates whether the specified transform should be included.</param>
    /// <param name="ancestorComponent">out first ancestor Component</param>
    /// <returns>if find The component. False if it none was found.</returns>
    public static bool TryGetAncestorComponent<TComponent>(this Component component, out TComponent ancestorComponent, bool includeSelf = true)
        where TComponent : Component
    {
        if (component == null)
        {
            throw new ArgumentNullException(nameof(component));
        }

        ancestorComponent = null;
        foreach (var transform in component.transform.EnumerateAncestors(includeSelf))
        {
            if (transform.TryGetComponent(out ancestorComponent))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 尝试在指定转换的祖先中查找类型为param的第一个组件
    /// </summary>
    /// <param name="type">Type of component to find.</param>
    /// <param name="component">Transform for which ancestors must be considered.</param>
    /// <param name="includeSelf">Indicates whether the specified transform should be included.</param>
    /// <param name="ancestorComponent">out first ancestor Component</param>
    /// <returns>if find The component. False if it none was found.</returns>
    public static bool TryGetAncestorComponent(this Component component, Type type, out Component ancestorComponent, bool includeSelf = true)
    {
        if (component == null)
        {
            throw new ArgumentNullException(nameof(component));
        }

        ancestorComponent = null;
        foreach (var transform in component.transform.EnumerateAncestors(includeSelf))
        {
            if (transform.TryGetComponent(type, out ancestorComponent))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 获取指定对象的所有子节点, 不包含父节点本身
    /// </summary>
    /// <param name="component"></param>
    /// <typeparam name="TChild"></typeparam>
    /// <returns></returns>
    public static IEnumerable<TChild> GetComponentsInChildrenWithoutSelf<TChild>(this Component component)
    {
        if (component == null)
        {
            throw new ArgumentNullException(nameof(component));
        }

        return component.gameObject.GetComponentsInChildrenWithoutSelf<TChild>();
    }

    /// <summary>
    /// 获取指定对象Active的所有子节点, 不包含父节点本身
    /// </summary>
    /// <param name="component"></param>
    /// <typeparam name="TChild"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<TChild> GetComponentsInChildrenOnlyActiveWithoutSelf<TChild>(this Component component)
    {
        if (component == null)
        {
            throw new ArgumentNullException(nameof(component));
        }

        return component.gameObject.GetComponentsInChildrenOnlyActiveWithoutSelf<TChild>();
    }

    /// <summary>
    /// 获取指定对象InActive的所有子节点, 不包含父节点本身
    /// </summary>
    /// <param name="component"></param>
    /// <typeparam name="TChild"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<TChild> GetComponentsInChildrenOnlyInActiveWithoutSelf<TChild>(this Component component)
    {
        if (component == null)
        {
            throw new ArgumentNullException(nameof(component));
        }

        return component.gameObject.GetComponentsInChildrenOnlyInActiveWithoutSelf<TChild>();
    }

    /// <summary>
    /// SetActive(true)指定对象下的所有子节点
    /// </summary>
    /// <param name="component"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void SetActiveChildren(this Component component)
    {
        if (component == null)
        {
            throw new ArgumentNullException(nameof(component));
        }

        component.gameObject.SetActiveChildren();
    }

    /// <summary>
    /// SetActive(false)指定对象下的所有子节点
    /// </summary>
    /// <param name="component"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void SetInactiveAllChildren(this Component component)
    {
        if (component == null)
        {
            throw new ArgumentNullException(nameof(component));
        }

        component.gameObject.SetInactiveChildren();
    }

    /// <summary>
    /// 销毁指定对象下的所有子节点
    /// </summary>
    /// <param name="component"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void DestroyChildren(this Component component)
    {
        if (component == null)
        {
            throw new ArgumentNullException(nameof(component));
        }

        component.gameObject.DestroyChildren();
    }

    /// <summary>
    /// 立即销毁指定对象下的所有子节点
    /// </summary>
    /// <param name="component"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void DestroyChildrenImmediate(this Component component)
    {
        if (component == null)
        {
            throw new ArgumentNullException(nameof(component));
        }

        component.gameObject.DestroyChildrenImmediate();
    }

    /// <summary>
    /// 是否含有指定组件
    /// </summary>
    /// <param name="component"></param>
    /// <typeparam name="TComponent"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static bool ContainsComponent<TComponent>(this Component component) where TComponent : Component
    {
        if (component == null)
        {
            throw new ArgumentNullException(nameof(component));
        }

        return component.gameObject.ContainsComponent<TComponent>();
    }

    /// <summary>
    /// 是否含有指定组件
    /// </summary>
    /// <param name="component"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static bool ContainsComponent(this Component component, Type type)
    {
        if (component == null)
        {
            throw new ArgumentNullException(nameof(component));
        }

        return component.gameObject.ContainsComponent(type);
    }

    /// <summary>
    /// 增加组件。
    /// </summary>
    /// <typeparam name="TComponent">要增加的组件。</typeparam>
    /// <param name="component">目标组件。</param>
    /// <returns>增加的组件。</returns>
    public static TComponent AddComponent<TComponent>(this Component component) where TComponent : Component
    {
        if (component == null)
        {
            throw new ArgumentNullException(nameof(component));
        }

        return component.gameObject.AddComponent<TComponent>();
    }

    /// <summary>
    /// 增加组件。
    /// </summary>
    /// <param name="component">目标对象。</param>
    /// <param name="type">要增加的组件类型。</param>
    /// <returns>增加的组件。</returns>
    public static Component AddComponent(this Component component, Type type)
    {
        if (component == null)
        {
            throw new ArgumentNullException(nameof(component));
        }

        return component.gameObject.AddComponent(type);
    }

    /// <summary>
    /// 获取或增加组件。
    /// </summary>
    /// <typeparam name="TComponent">要获取或增加的组件。</typeparam>
    /// <param name="component">目标组件。</param>
    /// <returns>获取或增加的组件。</returns>
    public static TComponent RequireComponent<TComponent>(this Component component) where TComponent : Component
    {
        if (component == null)
        {
            throw new ArgumentNullException(nameof(component));
        }

        return component.gameObject.RequireComponent<TComponent>();
    }

    /// <summary>
    /// 获取或增加组件。
    /// </summary>
    /// <param name="component">目标对象。</param>
    /// <param name="type">要获取或增加的组件类型。</param>
    /// <returns>获取或增加的组件。</returns>
    public static Component RequireComponent(this Component component, Type type)
    {
        if (component == null)
        {
            throw new ArgumentNullException(nameof(component));
        }

        return component.gameObject.RequireComponent(type);
    }

    /// <summary>
    /// 移除指定组件
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <param name="component"></param>
    public static void RemoveComponent<TComponent>(this Component component) where TComponent : Component
    {
        if (component == null)
        {
            throw new ArgumentNullException(nameof(component));
        }

        component.gameObject.RemoveComponent<TComponent>();
    }

    /// <summary>
    /// 移除指定组件
    /// </summary>
    /// <param name="component"></param>
    /// <param name="type"></param>
    public static void RemoveComponent(this Component component, Type type)
    {
        if (component == null)
        {
            throw new ArgumentNullException(nameof(component));
        }

        component.gameObject.RemoveComponent(type);
    }

    /// <summary>
    /// 移除所有指定组件
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <param name="component"></param>
    public static void RemoveComponents<TComponent>(this Component component) where TComponent : Component
    {
        if (component == null)
        {
            throw new ArgumentNullException(nameof(component));
        }

        component.gameObject.RemoveComponents<TComponent>();
    }

    /// <summary>
    /// 移除所有指定组件
    /// </summary>
    /// <param name="component"></param>
    /// <param name="type"></param>
    public static void RemoveComponents(this Component component, Type type)
    {
        if (component == null)
        {
            throw new ArgumentNullException(nameof(component));
        }

        component.gameObject.RemoveComponents(type);
    }

    /// <summary>
    /// 获取RectTransform组件
    /// </summary>
    /// <param name="component"></param>
    /// <returns></returns>
    public static RectTransform GetRectTransform(this Component component)
    {
        if (component == null)
        {
            throw new ArgumentNullException(nameof(component));
        }

        return component.gameObject.GetRectTransform();
    }

    /// <summary>
    /// 获取Component上的gameobject上的所有组件
    /// </summary>
    /// <param name="component"></param>
    /// <returns></returns>
    public static List<Component> GetAllComponentsList(this Component component)
    {
        if (component == null)
        {
            throw new ArgumentNullException(nameof(component));
        }

        return component.gameObject.GetComponentsList();
    }

    /// <summary>
    /// 计算这个游戏对象及其所有子对象的所有碰撞器的边界
    /// </summary>
    /// <param name="component">Component of root GameObject the collider are attached to </param>
    /// <returns>The total bounds of all collider attached to this GameObject. 
    /// If no collider attached, returns a bounds of center and extents 0</returns>
    public static Bounds GetColliderBounds(this Component component)
    {
        var colliderArray = component.GetComponentsInChildren<Collider>();
        if (colliderArray.Length == 0)
        {
            return new Bounds();
        }

        var bounds = colliderArray[0].bounds;
        for (int i = 1; i < colliderArray.Length; i++)
        {
            bounds.Encapsulate(colliderArray[i].bounds);
        }

        return bounds;
    }
}