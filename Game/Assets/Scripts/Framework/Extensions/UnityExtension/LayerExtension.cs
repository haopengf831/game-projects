using UnityEngine;

/// <summary>
/// Unity GameObject类的扩展方法
/// </summary>
public static partial class LayerExtension
{
    /// <summary>
    /// 是否是相同的层
    /// </summary>
    /// <param name="layerMask"></param>
    /// <param name="layer"></param>
    /// <returns></returns>
    public static bool IsEqual(this LayerMask layerMask, int layer)
    {
        return (layerMask & (1 << layer)) != 0;
    }

    /// <summary>
    /// 是否是相同的层
    /// </summary>
    /// <param name="layerMask"></param>
    /// <param name="go"></param>
    /// <returns></returns>
    public static bool IsEqual(this LayerMask layerMask, GameObject go)
    {
        if (go == null)
        {
            return false;
        }

        return (layerMask & (1 << go.layer)) != 0;
    }
}