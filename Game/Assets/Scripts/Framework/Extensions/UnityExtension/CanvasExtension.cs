using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Unity Canvas类的扩展方法
/// </summary>
public static partial class CanvasExtension
{
    /// <summary>
    /// 在世界坐标中获取画布平面
    /// </summary>
    /// <param name="canvas">The canvas to get the plane from.</param>
    /// <returns>A Plane for this canvas.</returns>
    public static Plane GetPlane(this Canvas canvas)
    {
        if (canvas == null)
        {
            throw new ArgumentNullException(nameof(canvas));
        }

        var corners = canvas.GetWorldCorners();

        // Now set a plane from any of the 3 corners (clockwise) so that we can compute our gaze intersection
        var plane = new Plane(corners[0], corners[1], corners[2]);

        return plane;
    }

    /// <summary>
    /// 获取世界坐标中画布角落(从左下角顺时针方向收集)
    /// 1 -- 2
    /// |    |
    /// 0 -- 3
    /// </summary>
    /// <param name="canvas">The canvas to get the world corners from.</param>
    /// <returns>An array of Vector3s that represent the corners of the canvas in world coordinates.</returns>
    public static Vector3[] GetWorldCorners(this Canvas canvas)
    {
        if (canvas == null)
        {
            throw new ArgumentNullException(nameof(canvas));
        }

        var worldCorners = new Vector3[4];
        if (!canvas.TryGetComponent<RectTransform>(out var rect))
        {
            throw new ArgumentNullException(nameof(rect));
        }

        rect.GetWorldCorners(worldCorners);
        return worldCorners;
    }

    /// <summary>
    /// 获取局部坐标中画布角落(从左下角顺时针方向收集)
    /// 1 -- 2
    /// |    |
    /// 0 -- 3
    /// </summary>
    /// <param name="canvas">The canvas to get the local corners from.</param>
    /// <returns>An array of Vector3s that represent the corners of the canvas in local coordinates.</returns>
    public static Vector3[] GetLocalCorners(this Canvas canvas)
    {
        if (canvas == null)
        {
            throw new ArgumentNullException(nameof(canvas));
        }

        var localCorners = new Vector3[4];
        if (!canvas.TryGetComponent<RectTransform>(out var rect))
        {
            throw new ArgumentNullException(nameof(rect));
        }

        rect.GetLocalCorners(localCorners);
        return localCorners;
    }

    /// <summary>
    /// 获取视口坐标中画布角落(注意: 这些点的顺序与GetWorldCorners()中返回的数组的顺序相同)
    /// 1 -- 2
    /// |    |
    /// 0 -- 3
    /// </summary>
    /// <param name="canvas">The canvas to get the viewport corners from</param>
    /// <returns>An array of Vector3s that represent the corners of the canvas in viewport coordinates</returns>
    public static Vector3[] GetViewportCorners(this Canvas canvas)
    {
        if (canvas == null)
        {
            throw new ArgumentNullException(nameof(canvas));
        }

        var viewportCorners = new Vector3[4];

        var worldCorners = canvas.GetWorldCorners();

        for (int i = 0; i < 4; i++)
        {
            if (Camera.main != null)
            {
                viewportCorners[i] = Camera.main.WorldToViewportPoint(worldCorners[i]);
            }
        }

        return viewportCorners;
    }

    /// <summary>
    /// 获取画布角落在屏幕空间中的位置
    /// 1 -- 2
    /// |    |
    /// 0 -- 3
    /// </summary>
    /// <param name="canvas">The canvas to get the screen corners for.</param>
    public static Vector3[] GetScreenCorners(this Canvas canvas)
    {
        if (canvas == null)
        {
            throw new ArgumentNullException(nameof(canvas));
        }

        var screenCorners = new Vector3[4];
        var worldCorners = canvas.GetWorldCorners();

        for (int i = 0; i < 4; i++)
        {
            if (Camera.main != null)
            {
                screenCorners[i] = Camera.main.WorldToScreenPoint(worldCorners[i]);
            }
        }

        return screenCorners;
    }

    /// <summary>
    /// 返回包含目标画布边界的屏幕坐标矩形
    /// </summary>
    /// <param name="canvas">The canvas the get the screen rect for</param>
    public static Rect GetScreenRect(this Canvas canvas)
    {
        if (canvas == null)
        {
            throw new ArgumentNullException(nameof(canvas));
        }

        var screenCorners = canvas.GetScreenCorners();
        var x = Mathf.Min(screenCorners[0].x, screenCorners[1].x);
        var y = Mathf.Min(screenCorners[0].y, screenCorners[3].y);
        var xMax = Mathf.Max(screenCorners[2].x, screenCorners[3].x);
        var yMax = Mathf.Max(screenCorners[1].y, screenCorners[2].y);
        return new Rect(x, y, xMax - x, yMax - y);
    }

    /// <summary>
    /// 用光线投射在画布上
    /// </summary>
    /// <param name="canvas">The canvas to raycast against</param>
    /// <param name="rayOrigin">The origin of the ray</param>
    /// <param name="rayDirection">The direction of the ray</param>
    /// <param name="distance">The distance of the ray</param>
    /// <param name="hitPoint">The hitpoint of the ray</param>
    /// <param name="hitChildObject">The child object that was hit or the canvas itself if it has no active children that were within the hit range.</param>
    public static bool Raycast(this Canvas canvas, Vector3 rayOrigin, Vector3 rayDirection, out float distance, out Vector3 hitPoint, out GameObject hitChildObject)
    {
        if (canvas == null)
        {
            throw new ArgumentNullException(nameof(canvas));
        }

        hitChildObject = null;
        var plane = canvas.GetPlane();
        var ray = new Ray(rayOrigin, rayDirection);

        if (plane.Raycast(ray, out distance))
        {
            // See if the point lies within the local canvas rect of the plane
            var corners = canvas.GetLocalCorners();
            hitPoint = rayOrigin + (rayDirection.normalized * distance);
            var localHitPoint = canvas.transform.InverseTransformPoint(hitPoint);
            if (localHitPoint.x >= corners[0].x
                && localHitPoint.x <= corners[3].x
                && localHitPoint.y <= corners[2].y
                && localHitPoint.y >= corners[3].y)
            {
                hitChildObject = canvas.gameObject;

                if (!canvas.TryGetComponent<RectTransform>(out var canvasRectTransform))
                {
                    throw new ArgumentNullException(nameof(canvasRectTransform));
                }

                // look for the child object that was hit
                var rectTransform = GetChildRectTransformAtPoint(canvasRectTransform, hitPoint, true, true, true);
                hitChildObject = rectTransform != null ? rectTransform.gameObject : canvas.gameObject;

                return true;
            }
        }

        hitPoint = Vector3.zero;

        return false;
    }

    /// <summary>
    /// 获取给定点和参数的子矩形转换
    /// </summary>
    /// <param name="rectTransformParent">The rect transform to look for children that may contain the projected (orthogonal to the child's normal) world point</param>
    /// <param name="worldPoint">The world point</param>
    /// <param name="recursive">Indicates if the check should be done recursively</param>
    /// <param name="shouldReturnActive">If true, will only check children that are active, otherwise it will check all children.</param>
    /// <param name="shouldReturnRaycastable">If true, will only check children that if they have a graphic and have it's member raycastTarget set to true, otherwise will ignore the raycastTarget value. Will still allow children to be checked that do not have a graphic component.</param>
    public static RectTransform GetChildRectTransformAtPoint(this RectTransform rectTransformParent, Vector3 worldPoint, bool recursive, bool shouldReturnActive,
        bool shouldReturnRaycastable)
    {
        Vector3[] localCorners = new Vector3[4];

        for (int i = rectTransformParent.childCount - 1; i >= 0; i--)
        {
            var child = rectTransformParent.GetChild(i);
            if (child.TryGetComponent<RectTransform>(out var rectTransform))
            {
                if (rectTransform.TryGetComponent<Graphic>(out var graphic))
                {
                    var shouldRaycast = shouldReturnRaycastable && graphic != null && graphic.raycastTarget || graphic == null || !shouldReturnRaycastable;

                    if (shouldReturnActive && rectTransform.gameObject.activeSelf || !shouldReturnActive)
                    {
                        rectTransform.GetLocalCorners(localCorners);
                        var childLocalPoint = rectTransform.InverseTransformPoint(worldPoint);

                        if (recursive)
                        {
                            var childRect = GetChildRectTransformAtPoint(rectTransform, worldPoint, recursive, shouldReturnActive, shouldReturnRaycastable);

                            if (childRect != null)
                            {
                                return childRect;
                            }
                        }

                        if (shouldRaycast
                            && childLocalPoint.x >= localCorners[0].x
                            && childLocalPoint.x <= localCorners[3].x
                            && childLocalPoint.y <= localCorners[2].y
                            && childLocalPoint.y >= localCorners[3].y)
                        {
                            return rectTransform;
                        }
                    }
                }
            }
        }

        return null;
    }
}