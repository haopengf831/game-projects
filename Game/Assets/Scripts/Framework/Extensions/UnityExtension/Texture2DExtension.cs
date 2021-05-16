using System;
using UnityEngine;

/// <summary>
/// Unity Texture2d类的扩展方法
/// </summary>
public static class Texture2DExtension
{
    /// <summary>
    /// 填充像素(需要调用Texture2D.Apply()才能进行实际更改的应用)
    /// </summary>
    /// <param name="texture2D">The Texture2D.</param>
    /// <param name="row">The row to start filling at.</param>
    /// <param name="col">The column to start filling at.</param>
    /// <param name="width">The width to fill.</param>
    /// <param name="height">The height to fill.</param>
    /// <param name="fillColor">Color of the fill.</param>
    /// <remarks>This function considers row 0 to be left and col 0 to be top.</remarks>
    public static void FillPixels(this Texture2D texture2D, int row, int col, int width, int height, Color fillColor)
    {
        if (texture2D == null)
        {
            throw new ArgumentNullException(nameof(texture2D));
        }

        if (col + width > texture2D.width || row + height > texture2D.height)
        {
            throw new IndexOutOfRangeException();
        }

        Color32 toColor = fillColor; //Implicit cast
        var colors = new Color32[width * height];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = toColor;
        }

        texture2D.SetPixels32(col, (texture2D.height - row) - height, width, height, colors);
    }

    /// <summary>
    /// 用单一颜色填充纹理
    /// </summary>
    /// <param name="texture2D">The Texture2D.</param>
    /// <param name="fillColor">Color of the fill.</param>
    public static void FillPixels(this Texture2D texture2D, Color fillColor)
    {
        if (texture2D == null)
        {
            throw new ArgumentNullException(nameof(texture2D));
        }

        texture2D.FillPixels(0, 0, texture2D.width, texture2D.height, fillColor);
    }

    /// <summary>
    /// 捕获屏幕的一个区域，并将其作为纹理返回
    /// </summary>
    /// <param name="x">x position of the screen to capture from (bottom-left)</param>
    /// <param name="y">y position of the screen to capture from (bottom-left)</param>
    /// <param name="width">width of the screen area to capture</param>
    /// <param name="height">height of the screen area to capture</param>
    /// <returns>A Texture2D containing pixels from the region of the screen specified</returns>
    /// <remarks>You should call this in OnPostRender.</remarks>
    public static Texture2D CaptureScreenRegion(int x, int y, int width, int height)
    {
        var tex = new Texture2D(width, height);
        tex.ReadPixels(new Rect(x, y, Screen.width, Screen.height), 0, 0);
        tex.Apply();
        return tex;
    }

    /// <summary>
    /// 从定义的区域创建纹理
    /// </summary>
    /// <param name="texture2D">The Texture2D.</param>
    /// <param name="x">x position of this texture to get the texture from</param>
    /// <param name="y">y position of this texture to get the texture from</param>
    /// <param name="width">width of the region to capture</param>
    /// <param name="height">height of the region to capture</param>
    public static Texture2D CreateTextureFromRegion(this Texture2D texture2D, int x, int y, int width, int height)
    {
        if (texture2D == null)
        {
            throw new ArgumentNullException(nameof(texture2D));
        }

        var pixels = texture2D.GetPixels(x, y, width, height);

        var destTex = new Texture2D(width, height);
        destTex.SetPixels(pixels);
        destTex.Apply();

        return destTex;
    }
}