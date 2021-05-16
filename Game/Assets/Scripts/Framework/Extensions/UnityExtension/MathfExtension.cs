using UnityEngine;

public static partial class MathfExtension
{
    /// <summary>
    /// 角度值转弧度值
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static float AngleToRadian(this float angle)
    {
        return angle * Mathf.Deg2Rad;
    }

    /// <summary>
    /// 弧度值转角度值
    /// </summary>
    /// <param name="radian"></param>
    /// <returns></returns>
    public static float RadianToAngle(this float radian)
    {
        return radian * Mathf.Rad2Deg;
    }

    /// <summary>
    /// 圆周长
    /// </summary>
    /// <param name="radius"></param>
    /// <returns></returns>
    public static float CirclePerimeter(this float radius)
    {
        return 2 * Mathf.PI * radius;
    }

    /// <summary>
    /// 圆面积
    /// </summary>
    /// <param name="radius"></param>
    /// <returns></returns>
    public static float CircleArea(this float radius)
    {
        return Mathf.PI * Mathf.Pow(radius, 2);
    }

    /// <summary>
    /// 直角三角形中,已知一条直角边以及相邻锐角(角度值),求另一条直角边
    /// </summary>
    /// <param name="side"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static float RightAngleSideByAdjacentAcuteAngle(this float side, float angle)
    {
        if (side <= 0 || angle <= 0 || angle >= 90)
        {
            return 0;
        }

        return side * Mathf.Tan(angle.AngleToRadian());
    }

    /// <summary>
    /// 直角三角形中,已知一条直角边以及相对锐角(角度值),求另一条直角边
    /// </summary>
    /// <param name="side"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static float RightAngleSideByRelativeAcuteAngle(this float side, float angle)
    {
        if (side <= 0 || angle <= 0 || angle >= 90)
        {
            return 0;
        }

        return side * Mathf.Tan((90 - angle).AngleToRadian());
    }

    /// <summary>
    /// 直角三角形中,已知两条直角边,求斜边
    /// </summary>
    /// <param name="side1"></param>
    /// <param name="side2"></param>
    /// <returns></returns>
    public static float RightTriangleHypotenuse(this float side1, float side2)
    {
        if (side1 <= 0 || side2 <= 0)
        {
            return 0;
        }

        return Mathf.Sqrt(Mathf.Pow(side1, 2) + Mathf.Pow(side2, 2));
    }
}