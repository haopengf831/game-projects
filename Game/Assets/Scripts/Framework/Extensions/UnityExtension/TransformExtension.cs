using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity Transform类的扩展方法
/// </summary>
public static partial class TransformExtension
{
    /// <summary>
    /// The Negative red axis of the transform in world space.
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Vector3 Left(this Transform transform)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        return transform.right * -1;
    }

    /// <summary>
    /// The Negative green axis of the transform in world space.
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Vector3 Down(this Transform transform)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        return transform.up * -1;
    }

    /// <summary>
    /// The Negative blue axis of the transform in world space.
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Vector3 Back(this Transform transform)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        return transform.forward * -1;
    }

    /// <summary>
    /// 自身90度正左方向
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="forward"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Vector3 Rotate90DegreeToLeft(this Transform transform, Vector3 forward)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        return (Quaternion.AngleAxis(90, forward.Down()) * forward).normalized;
    }

    /// <summary>
    /// 自身90度正右方向
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="forward"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Vector3 Rotate90DegreeToRight(this Transform transform, Vector3 forward)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        return (Quaternion.AngleAxis(90, forward.Up()) * forward).normalized;
    }

    /// <summary>
    /// 自身90度正上方向
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="forward"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Vector3 Rotate90DegreeToUp(this Transform transform, Vector3 forward)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        return (Quaternion.AngleAxis(90, forward.Left()) * forward).normalized;
    }

    /// <summary>
    /// 自身90度正下方向
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="forward"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Vector3 Rotate90DegreeToDown(this Transform transform, Vector3 forward)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        return (Quaternion.AngleAxis(90, forward.Right()) * forward).normalized;
    }

    /// <summary>
    /// 自身左上方向
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="forward"></param>
    /// <param name="leftAngle"></param>
    /// <param name="upAngle"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Vector3 RotateToLeftUp(this Transform transform, Vector3 forward, float leftAngle, float upAngle)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        return (Quaternion.AngleAxis(leftAngle, forward.Left()) * Quaternion.AngleAxis(upAngle, forward.Up()) * forward).normalized;
    }

    /// <summary>
    /// 自身右上方向
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="forward"></param>
    /// <param name="rightAngle"></param>
    /// <param name="upAngle"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Vector3 RotateRightUp(this Transform transform, Vector3 forward, float rightAngle, float upAngle)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        return (Quaternion.AngleAxis(rightAngle, forward.Right()) * Quaternion.AngleAxis(upAngle, forward.Up()) * forward).normalized;
    }

    /// <summary>
    /// 自身左下方向
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="forward"></param>
    /// <param name="leftAngle"></param>
    /// <param name="downAngle"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Vector3 RotateToLeftDown(this Transform transform, Vector3 forward, float leftAngle, float downAngle)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        return (Quaternion.AngleAxis(leftAngle, forward.Left()) * Quaternion.AngleAxis(downAngle, forward.Down()) * forward).normalized;
    }

    /// <summary>
    /// 自身右下方向
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="forward"></param>
    /// <param name="rightAngle"></param>
    /// <param name="downAngle"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Vector3 RotateToRightDown(this Transform transform, Vector3 forward, float rightAngle, float downAngle)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        return (Quaternion.AngleAxis(rightAngle, forward.Right()) * Quaternion.AngleAxis(downAngle, forward.Down()) * forward).normalized;
    }

    /// <summary>
    /// 自身45度左上方向
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="forward"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Vector3 Rotate45DegreeToLeftUp(this Transform transform, Vector3 forward)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        return transform.RotateToLeftUp(forward, 45, -45);
    }

    /// <summary>
    /// 自身45度右上方向
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="forward"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Vector3 Rotate45DegreeToRightUp(this Transform transform, Vector3 forward)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        return transform.RotateRightUp(forward, -45, 45);
    }

    /// <summary>
    /// 自身45度左下方向
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="forward"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Vector3 Rotate45DegreeToLeftDown(this Transform transform, Vector3 forward)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        return transform.RotateToLeftDown(forward, -45, 45);
    }

    /// <summary>
    /// 自身45度右下方向
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="forward"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Vector3 Rotate45DegreeToRightDown(this Transform transform, Vector3 forward)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        return transform.RotateToRightDown(forward, 45, -45);
    }

    /// <summary>
    /// 自身135度左上方向
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="forward"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Vector3 Rotate135DegreeToLeftUp(this Transform transform, Vector3 forward)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        return transform.RotateToLeftUp(forward, -45, 225);
    }

    /// <summary>
    /// 自身135度右上方向
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="forward"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Vector3 Rotate135DegreeToRightUp(this Transform transform, Vector3 forward)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        return transform.RotateRightUp(forward, 45, 135);
    }

    /// <summary>
    /// 自身135度左下方向
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="forward"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Vector3 Rotate135DegreeToLeftDown(this Transform transform, Vector3 forward)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        return transform.RotateToLeftDown(forward, 45, 135);
    }

    /// <summary>
    /// 自身135度右下方向
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="forward"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Vector3 Rotate135DegreeToRightDown(this Transform transform, Vector3 forward)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        return transform.RotateToRightDown(forward, -45, 225);
    }

    /// <summary>
    /// 设置绝对位置的 x 坐标。
    /// </summary>
    /// <param name="transform"><see cref="UnityEngine.Transform" /> 对象。</param>
    /// <param name="newValue">x 坐标值。</param>
    public static void SetPositionX(this Transform transform, float newValue)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        var v = transform.position;
        v.x = newValue;
        transform.position = v;
    }

    /// <summary>
    /// 设置绝对位置的 y 坐标。
    /// </summary>
    /// <param name="transform"><see cref="UnityEngine.Transform" /> 对象。</param>
    /// <param name="newValue">y 坐标值。</param>
    public static void SetPositionY(this Transform transform, float newValue)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        var v = transform.position;
        v.y = newValue;
        transform.position = v;
    }

    /// <summary>
    /// 设置绝对位置的 z 坐标。
    /// </summary>
    /// <param name="transform"><see cref="UnityEngine.Transform" /> 对象。</param>
    /// <param name="newValue">z 坐标值。</param>
    public static void SetPositionZ(this Transform transform, float newValue)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        var v = transform.position;
        v.z = newValue;
        transform.position = v;
    }

    /// <summary>
    /// 增加绝对位置的 x 坐标。
    /// </summary>
    /// <param name="transform"><see cref="UnityEngine.Transform" /> 对象。</param>
    /// <param name="deltaValue">x 坐标值增量。</param>
    public static void AddPositionX(this Transform transform, float deltaValue)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        var v = transform.position;
        v.x += deltaValue;
        transform.position = v;
    }

    /// <summary>
    /// 增加绝对位置的 y 坐标。
    /// </summary>
    /// <param name="transform"><see cref="UnityEngine.Transform" /> 对象。</param>
    /// <param name="deltaValue">y 坐标值增量。</param>
    public static void AddPositionY(this Transform transform, float deltaValue)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        var v = transform.position;
        v.y += deltaValue;
        transform.position = v;
    }

    /// <summary>
    /// 增加绝对位置的 z 坐标。
    /// </summary>
    /// <param name="transform"><see cref="UnityEngine.Transform" /> 对象。</param>
    /// <param name="deltaValue">z 坐标值增量。</param>
    public static void AddPositionZ(this Transform transform, float deltaValue)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        var v = transform.position;
        v.z += deltaValue;
        transform.position = v;
    }

    /// <summary>
    /// 设置相对位置的 x 坐标。
    /// </summary>
    /// <param name="transform"><see cref="UnityEngine.Transform" /> 对象。</param>
    /// <param name="newValue">x 坐标值。</param>
    public static void SetLocalPositionX(this Transform transform, float newValue)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        var v = transform.localPosition;
        v.x = newValue;
        transform.localPosition = v;
    }

    /// <summary>
    /// 设置相对位置的 y 坐标。
    /// </summary>
    /// <param name="transform"><see cref="UnityEngine.Transform" /> 对象。</param>
    /// <param name="newValue">y 坐标值。</param>
    public static void SetLocalPositionY(this Transform transform, float newValue)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        var v = transform.localPosition;
        v.y = newValue;
        transform.localPosition = v;
    }

    /// <summary>
    /// 设置相对位置的 z 坐标。
    /// </summary>
    /// <param name="transform"><see cref="UnityEngine.Transform" /> 对象。</param>
    /// <param name="newValue">z 坐标值。</param>
    public static void SetLocalPositionZ(this Transform transform, float newValue)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        var v = transform.localPosition;
        v.z = newValue;
        transform.localPosition = v;
    }

    /// <summary>
    /// 增加相对位置的 x 坐标。
    /// </summary>
    /// <param name="transform"><see cref="UnityEngine.Transform" /> 对象。</param>
    /// <param name="deltaValue">x 坐标值。</param>
    public static void AddLocalPositionX(this Transform transform, float deltaValue)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        var v = transform.localPosition;
        v.x += deltaValue;
        transform.localPosition = v;
    }

    /// <summary>
    /// 增加相对位置的 y 坐标。
    /// </summary>
    /// <param name="transform"><see cref="UnityEngine.Transform" /> 对象。</param>
    /// <param name="deltaValue">y 坐标值。</param>
    public static void AddLocalPositionY(this Transform transform, float deltaValue)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        var v = transform.localPosition;
        v.y += deltaValue;
        transform.localPosition = v;
    }

    /// <summary>
    /// 增加相对位置的 z 坐标。
    /// </summary>
    /// <param name="transform"><see cref="UnityEngine.Transform" /> 对象。</param>
    /// <param name="deltaValue">z 坐标值。</param>
    public static void AddLocalPositionZ(this Transform transform, float deltaValue)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        var v = transform.localPosition;
        v.z += deltaValue;
        transform.localPosition = v;
    }

    /// <summary>
    /// 设置相对尺寸的 x 分量。
    /// </summary>
    /// <param name="transform"><see cref="UnityEngine.Transform" /> 对象。</param>
    /// <param name="newValue">x 分量值。</param>
    public static void SetLocalScaleX(this Transform transform, float newValue)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        var v = transform.localScale;
        v.x = newValue;
        transform.localScale = v;
    }

    /// <summary>
    /// 设置相对尺寸的 y 分量。
    /// </summary>
    /// <param name="transform"><see cref="UnityEngine.Transform" /> 对象。</param>
    /// <param name="newValue">y 分量值。</param>
    public static void SetLocalScaleY(this Transform transform, float newValue)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        var v = transform.localScale;
        v.y = newValue;
        transform.localScale = v;
    }

    /// <summary>
    /// 设置相对尺寸的 z 分量。
    /// </summary>
    /// <param name="transform"><see cref="UnityEngine.Transform" /> 对象。</param>
    /// <param name="newValue">z 分量值。</param>
    public static void SetLocalScaleZ(this Transform transform, float newValue)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        var v = transform.localScale;
        v.z = newValue;
        transform.localScale = v;
    }

    /// <summary>
    /// 增加相对尺寸的 x 分量。
    /// </summary>
    /// <param name="transform"><see cref="UnityEngine.Transform" /> 对象。</param>
    /// <param name="deltaValue">x 分量增量。</param>
    public static void AddLocalScaleX(this Transform transform, float deltaValue)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        var v = transform.localScale;
        v.x += deltaValue;
        transform.localScale = v;
    }

    /// <summary>
    /// 增加相对尺寸的 y 分量。
    /// </summary>
    /// <param name="transform"><see cref="UnityEngine.Transform" /> 对象。</param>
    /// <param name="deltaValue">y 分量增量。</param>
    public static void AddLocalScaleY(this Transform transform, float deltaValue)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        var v = transform.localScale;
        v.y += deltaValue;
        transform.localScale = v;
    }

    /// <summary>
    /// 增加相对尺寸的 z 分量。
    /// </summary>
    /// <param name="transform"><see cref="UnityEngine.Transform" /> 对象。</param>
    /// <param name="deltaValue">z 分量增量。</param>
    public static void AddLocalScaleZ(this Transform transform, float deltaValue)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        var v = transform.localScale;
        v.z += deltaValue;
        transform.localScale = v;
    }

    /// <summary>
    /// 二维空间下使 <see cref="UnityEngine.Transform" /> 指向指向目标点的算法，使用世界坐标。
    /// </summary>
    /// <param name="transform"><see cref="UnityEngine.Transform" /> 对象。</param>
    /// <param name="lookAtPoint2D">要朝向的二维坐标点。</param>
    /// <remarks>假定其 forward 向量为 <see cref="UnityEngine.Vector3.up" />。</remarks>
    public static void LookAt2D(this Transform transform, Vector2 lookAtPoint2D)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        var vector = lookAtPoint2D.ToVector3Z() - transform.position;
        vector.y = 0f;

        if (vector.magnitude > 0f)
        {
            transform.rotation = Quaternion.LookRotation(vector.normalized, Vector3.up);
        }
    }

    /// <summary>
    /// 获取物体LookAt后的旋转值
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="targetPoint"></param>
    /// <returns></returns>
    public static Vector3 GetLookAtEuler(this Transform transform, Vector3 targetPoint)
    {
        if (transform == null)
        {
            throw new ArgumentNullException(nameof(transform));
        }

        return transform.position.GetLookAtEuler(targetPoint);
    }

    /// <summary>
    /// 枚举指定transform的祖先
    /// </summary>
    /// <param name="transform">Transform for which ancestors must be returned.</param>
    /// <param name="includeSelf">Indicates whether the specified transform should be included.</param>
    /// <returns>An enumeration of all ancestor transforms of the specified start transform.</returns>
    public static IEnumerable<Transform> EnumerateAncestors(this Transform transform, bool includeSelf)
    {
        if (includeSelf)
        {
            for (var tempTransform = transform; tempTransform != null; tempTransform = tempTransform.parent)
            {
                yield return tempTransform;
            }
        }
        else
        {
            for (var tempTransform = transform.parent; tempTransform != null; tempTransform = tempTransform.parent)
            {
                yield return tempTransform;
            }
        }
    }

    /// <summary>
    /// 将Size从Local转换为World
    /// </summary>
    /// <param name="transform">The transform.</param>
    /// <param name="localSize">The local size.</param>
    /// <returns>World size.</returns>
    public static Vector3 TransformSize(this Transform transform, Vector3 localSize)
    {
        Vector3 transformedSize = new Vector3(localSize.x, localSize.y, localSize.z);

        Transform t = transform;
        do
        {
            var localScale = t.localScale;
            transformedSize.x *= localScale.x;
            transformedSize.y *= localScale.y;
            transformedSize.z *= localScale.z;
            t = t.parent;
        } while (t != null);

        return transformedSize;
    }

    /// <summary>
    /// 将Size从World转换为Local
    /// </summary>
    /// <param name="transform">The transform.</param>
    /// <param name="worldSize">The world size</param>
    /// <returns>World size.</returns>
    public static Vector3 InverseTransformSize(this Transform transform, Vector3 worldSize)
    {
        Vector3 transformedSize = new Vector3(worldSize.x, worldSize.y, worldSize.z);

        Transform t = transform;
        do
        {
            var localScale = t.localScale;
            transformedSize.x /= localScale.x;
            transformedSize.y /= localScale.y;
            transformedSize.z /= localScale.z;
            t = t.parent;
        } while (t != null);

        return transformedSize;
    }
}