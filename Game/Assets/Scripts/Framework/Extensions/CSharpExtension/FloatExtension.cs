using System;

public static partial class FloatExtension
{
    /// <summary>
    /// 转换为正浮点数
    /// </summary>
    /// <param name="floating"></param>
    /// <returns></returns>
    public static float Positive(this float floating)
    {
        return floating >= 0 ? floating : Math.Abs(floating);
    }

    /// <summary>
    /// 转换为负浮点数
    /// </summary>
    /// <param name="floating"></param>
    /// <returns></returns>
    public static float Negative(this float floating)
    {
        return floating <= 0 ? floating : -floating;
    }

    /// <summary>
    /// 检查两个浮点数是否大致相等(与<see href="https://docs.unity3d.com/ScriptReference/Mathf.Approximately.html">Mathf.Approximately(float, float)</see>相似,但公差可手动调整)
    /// </summary>
    /// <param name="number">One of the numbers to compare.</param>
    /// <param name="other">The other number to compare.</param>
    /// <param name="tolerance">The amount of tolerance to allow while still considering the numbers approximately equal.</param>
    /// <returns>True if the difference between the numbers is less than or equal to the tolerance, false otherwise.</returns>
    public static bool Approximately(this float number, float other, float tolerance)
    {
        return Math.Abs(number - other) <= tolerance;
    }
}