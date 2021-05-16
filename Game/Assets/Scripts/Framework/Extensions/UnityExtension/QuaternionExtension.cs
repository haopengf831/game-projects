using UnityEngine;

/// <summary>
/// Unity Quaternion结构体的扩展方法
/// </summary>
public static class QuaternionExtension
{
    /// <summary>
    /// 是否是合法的Quaternion
    /// </summary>
    /// <param name="quaternion"></param>
    /// <returns></returns>
    public static bool IsValidRotation(this Quaternion quaternion)
    {
        return !float.IsNaN(quaternion.x) && !float.IsNaN(quaternion.y) && !float.IsNaN(quaternion.z) && !float.IsNaN(quaternion.w) &&
               !float.IsInfinity(quaternion.x) && !float.IsInfinity(quaternion.y) && !float.IsInfinity(quaternion.z) && !float.IsInfinity(quaternion.w);
    }

    /// <summary>
    /// 比较Quaternion
    /// </summary>
    /// <param name="quaternion"></param>
    /// <param name="targetQuaternion"></param>
    /// <param name="tolerance">容忍度</param>
    /// <returns></returns>
    public static bool Compare(this Quaternion quaternion, Quaternion targetQuaternion, float tolerance = 0.0f)
    {
        return quaternion.x.Approximately(targetQuaternion.x, tolerance)
               && quaternion.y.Approximately(targetQuaternion.y, tolerance)
               && quaternion.z.Approximately(targetQuaternion.z, tolerance)
               && quaternion.w.Approximately(targetQuaternion.w, tolerance);
    }
}