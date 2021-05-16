using System;
using System.Globalization;
using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Execution;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Unity Color/Color32结构体的扩展方法
/// </summary>
public static partial class ColorExtension
{
    /// <summary>
    /// 负色
    /// </summary>
    /// <param name="color32"></param>
    /// <returns></returns>
    public static Color32 ReverseColor32(this Color32 color32)
    {
        return new Color32((byte) (byte.MaxValue - color32.r), (byte) (byte.MaxValue - color32.g), (byte) (byte.MaxValue - color32.b), (byte) (byte.MaxValue - color32.a));
    }

    /// <summary>
    /// 负色
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public static Color ReverseColor(this Color color)
    {
        return new Color(1 - color.r, 1 - color.g, 1 - color.b, 1 - color.a);
    }

    /// <summary>
    /// 用于比较两个Color32类型是不是同种颜色
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="compare"></param>
    /// <returns></returns>
    public static bool IsEqual(this Color32 origin, Color32 compare)
    {
        return origin.r == compare.r && origin.g == compare.g && origin.b == compare.b && origin.a == compare.a;
    }

    /// <summary>
    /// 同步随机颜色
    /// </summary>
    /// <returns></returns>
    public static Color SyncRandomColor()
    {
        var color = new Color {r = Random.Range(0.0f, 1.0f), g = Random.Range(0.0f, 1.0f), b = Random.Range(0.0f, 1.0f), a = 1.0f};
        return color;
    }

    /// <summary>
    /// 异步随机颜色
    /// </summary>
    /// <returns></returns>
    public static IAsyncResult<Color> AsyncRandomColor()
    {
        return Executors.RunAsync(SyncRandomColor);
    }

    /// <summary>
    /// 同步随机颜色，直到随机到与指定颜色不相同的颜色为止
    /// </summary>
    /// <param name="rejectColor"></param>
    /// <returns></returns>
    public static Color SyncRandomColorReject(this Color rejectColor)
    {
        Color color;
        do
        {
            color = SyncRandomColor();
        } while (color.Equals(rejectColor));

        return color;
    }

    /// <summary>
    /// 异步随机颜色，直到随机到与指定颜色不相同的颜色为止
    /// </summary>
    /// <param name="rejectColor"></param>
    /// <returns></returns>
    public static IAsyncResult<Color> AsyncRandomColorReject(this Color rejectColor)
    {
        return Executors.RunAsync(() => SyncRandomColorReject(rejectColor));
    }

    /// <summary>
    /// 自左乘Alpha
    /// </summary>
    /// <param name="col"></param>
    /// <returns></returns>
    public static Color PremultiplyAlpha(Color col)
    {
        col.r *= col.a;
        col.g *= col.a;
        col.b *= col.a;

        return col;
    }

    /// <summary>
    /// 自左乘Alpha
    /// </summary>
    /// <param name="col"></param>
    /// <returns></returns>
    public static Color32 PremultiplyAlpha(Color32 col)
    {
        Color floatCol = col;
        return (Color32) PremultiplyAlpha(floatCol);
    }

    /// <summary>
    /// 从十六进制代码字符串创建颜色
    /// </summary>
    public static Color ParseHexcode(string hexstring)
    {
        if (hexstring.StartsWith("#"))
        {
            hexstring = hexstring.Substring(1);
        }

        if (hexstring.StartsWith("0x"))
        {
            hexstring = hexstring.Substring(2);
        }

        if (hexstring.Length == 6)
        {
            hexstring += "FF";
        }

        if (hexstring.Length != 8)
        {
            throw new ArgumentException(string.Format("{0} is not a valid color string.", hexstring));
        }

        byte r = byte.Parse(hexstring.Substring(0, 2), NumberStyles.HexNumber);
        byte g = byte.Parse(hexstring.Substring(2, 2), NumberStyles.HexNumber);
        byte b = byte.Parse(hexstring.Substring(4, 2), NumberStyles.HexNumber);
        byte a = byte.Parse(hexstring.Substring(6, 2), NumberStyles.HexNumber);

        const float maxRgbValue = 255;
        Color c = new Color(r / maxRgbValue, g / maxRgbValue, b / maxRgbValue, a / maxRgbValue);
        return c;
    }
}