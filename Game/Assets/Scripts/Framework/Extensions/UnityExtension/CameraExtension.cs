using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Unity Camera类的扩展方法
/// </summary>
public static partial class CameraExtension
{
    /// <summary>
    /// 根据给定距离获取摄像机视口的四个顶点坐标的数组
    /// </summary>
    /// <param name="camera"></param>
    /// <param name="distance"></param>
    /// 0-----------1
    /// |          |
    /// |          |
    /// |          |
    /// 2----------3 
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Vector3[] FieldOfViewCornersArray(this Camera camera, float distance)
    {
        if (camera == null)
        {
            throw new ArgumentNullException(nameof(camera));
        }

        var corners = new Vector3[4];

        var halfFieldOfView = camera.fieldOfView * 0.5f * Mathf.Deg2Rad;
        var aspect = camera.aspect;

        var height = distance * Mathf.Tan(halfFieldOfView);
        var width = height * aspect;

        var cameraTransform = camera.transform;
        var cameraPosition = cameraTransform.position;
        var cameraUp = cameraTransform.up;
        var cameraForward = cameraTransform.forward;
        var cameraRight = cameraTransform.right;

        // UpperLeft
        corners[0] = cameraPosition - cameraRight * width;
        corners[0] += cameraUp * height;
        corners[0] += cameraForward * distance;

        // UpperRight
        corners[1] = cameraPosition + cameraRight * width;
        corners[1] += cameraUp * height;
        corners[1] += cameraForward * distance;

        // LowerLeft
        corners[2] = cameraPosition - cameraRight * width;
        corners[2] -= cameraUp * height;
        corners[2] += cameraForward * distance;

        // LowerRight
        corners[3] = cameraPosition + cameraRight * width;
        corners[3] -= cameraUp * height;
        corners[3] += cameraForward * distance;

        return corners;
    }

    /// <summary>
    /// 根据给定距离获取摄像机视口的四个顶点坐标的列表
    /// </summary>
    /// <param name="camera"></param>
    /// <param name="distance"></param>
    /// 0-----------1
    /// |          |
    /// |          |
    /// |          |
    /// 2----------3 
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static List<Vector3> FieldOfViewCornersList(this Camera camera, float distance)
    {
        if (camera == null)
        {
            throw new ArgumentNullException(nameof(camera));
        }

        return camera.FieldOfViewCornersArray(distance).ToList();
    }

    /// <summary>
    /// 获取摄像机视口中心的标准向量
    /// </summary>
    /// <param name="camera"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Vector3 CenterViewDirection(this Camera camera)
    {
        if (camera == null)
        {
            throw new ArgumentNullException(nameof(camera));
        }

        var corners = camera.FieldOfViewCornersArray(1);
        if (corners.Length != 4)
        {
            throw new OverflowException(nameof(corners));
        }

        var plane = new Plane(corners[0], corners[1], corners[2]).flipped;
        return plane.normal.normalized;
    }

    /// <summary>
    /// 根据给定距离获取摄像机视口中心的坐标
    /// </summary>
    /// <param name="camera"></param>
    /// <param name="distance"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Vector3 CenterViewPosition(this Camera camera, float distance)
    {
        if (camera == null)
        {
            throw new ArgumentNullException(nameof(camera));
        }

        return camera.CenterViewDirection() * distance;
    }

    /// <summary>
    /// 从立体摄像机获取水平FOV
    /// </summary>
    /// <param name="camera"></param>
    /// <returns></returns>
    public static float GetHorizontalFieldOfViewRadians(this Camera camera)
    {
        if (camera == null)
        {
            throw new ArgumentNullException(nameof(camera));
        }
        
        return 2f * Mathf.Atan(Mathf.Tan(camera.fieldOfView * Mathf.Deg2Rad * 0.5f) * camera.aspect);
    }

    /// <summary>
    /// 返回一个点是否会在任一只眼睛中呈现在屏幕上
    /// </summary>
    /// <param name="camera">The camera to check the point against</param>
    /// <param name="position">the point position</param>
    public static bool IsInFov(this Camera camera, Vector3 position)
    {
        if (camera == null)
        {
            throw new ArgumentNullException(nameof(camera));
        }

        var cameraTransform = camera.transform;
        
        Vector3 deltaPos = position - cameraTransform.position;
        Vector3 headDeltaPos = TransformUtilities.TransformDirectionFromTo(null, cameraTransform, deltaPos);

        if (headDeltaPos.z < camera.nearClipPlane || headDeltaPos.z > camera.farClipPlane)
        {
            return false;
        }

        float verticalFovHalf = camera.fieldOfView * 0.5f;
        float horizontalFovHalf = camera.GetHorizontalFieldOfViewRadians() * Mathf.Rad2Deg * 0.5f;

        headDeltaPos = headDeltaPos.normalized;
        float yaw = Mathf.Asin(headDeltaPos.x) * Mathf.Rad2Deg;
        float pitch = Mathf.Asin(headDeltaPos.y) * Mathf.Rad2Deg;

        return Mathf.Abs(yaw) < horizontalFovHalf && Mathf.Abs(pitch) < verticalFovHalf;
    }

    /// <summary>
    /// 获取距摄像机给定距离处的平截头体大小。
    /// </summary>
    /// <param name="camera">The camera to get the frustum size for</param>
    /// <param name="distanceFromCamera">The distance from the camera to get the frustum size at</param>
    public static Vector2 GetFrustumSizeForDistance(this Camera camera, float distanceFromCamera)
    {
        if (camera == null)
        {
            throw new ArgumentNullException(nameof(camera));
        }
        
        Vector2 frustumSize = new Vector2
        {
            y = 2.0f * distanceFromCamera * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad)
        };
        frustumSize.x = frustumSize.y * camera.aspect;

        return frustumSize;
    }

    /// <summary>
    /// 获取特定平截头体高度到摄像机的距离。
    /// </summary>
    /// <param name="camera">The camera to get the distance from</param>
    /// <param name="frustumHeight">The frustum height</param>
    public static float GetDistanceForFrustumHeight(this Camera camera, float frustumHeight)
    {
        if (camera == null)
        {
            throw new ArgumentNullException(nameof(camera));
        }
        
        return frustumHeight * 0.5f / Mathf.Max(0.00001f, Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad));
    }
}