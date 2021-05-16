using System;
using UnityEngine;

/// <summary>
/// Unity LineRenderer类的扩展方法
/// </summary>
public static partial class LineRendererExtension
{
    /// <summary>
    /// 划线
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static LineRenderer DrawLineRenderer(Vector3 origin, Vector3 end)
    {
        var lineRenderer = new GameObject().RequireComponent<LineRenderer>();
        lineRenderer.startWidth = 0.005f;
        lineRenderer.endWidth = 0.001f;
        lineRenderer.SetPositions(new[]
        {
            origin,
            end,
        });
        return lineRenderer;
    }

    /// <summary>
    /// 划线
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="direction"></param>
    /// <param name="lineLength"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static LineRenderer DrawLineRendererByDirection(Vector3 origin, Vector3 direction, float lineLength, Transform parent = null)
    {
        var lineRenderer = new GameObject().RequireComponent<LineRenderer>();
        lineRenderer.startWidth = 0.005f;
        lineRenderer.endWidth = 0.001f;
        lineRenderer.SetPositions(new[]
        {
            origin,
            origin + direction * lineLength,
        });
        if (parent != null)
        {
            lineRenderer.transform.SetParent(parent);
        }

        return lineRenderer;
    }

    /// <summary>
    /// 划线
    /// </summary>
    /// <param name="lineRenderer"></param>
    /// <param name="origin"></param>
    /// <param name="direction"></param>
    /// <param name="lineLength"></param>
    public static void DrawLineRenderer(this LineRenderer lineRenderer, Vector3 origin, Vector3 direction, float lineLength)
    {
        if (lineRenderer == null)
        {
            throw new ArgumentNullException(nameof(lineRenderer));
        }
        
        lineRenderer.startWidth = 0.005f;
        lineRenderer.endWidth = 0.001f;
        lineRenderer.SetPositions(new[]
        {
            origin,
            origin + direction * lineLength,
        });
    }
}