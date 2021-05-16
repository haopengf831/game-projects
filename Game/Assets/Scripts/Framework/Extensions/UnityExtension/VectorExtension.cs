using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
/// Unity Vector结构体的扩展方法
/// </summary>
public static partial class VectorExtension
{
    /// <summary>
    /// Vector2是否合法
    /// </summary>
    /// <param name="vector2"></param>
    /// <returns></returns>
    public static bool IsValidVector(this Vector2 vector2)
    {
        return IsValidVector(vector2.ToVector3Z());
    }

    /// <summary>
    /// Vector3是否合法
    /// </summary>
    /// <param name="vector3"></param>
    /// <returns></returns>
    public static bool IsValidVector(this Vector3 vector3)
    {
        return !float.IsNaN(vector3.x) && !float.IsNaN(vector3.y) && !float.IsNaN(vector3.z) &&
               !float.IsInfinity(vector3.x) && !float.IsInfinity(vector3.y) && !float.IsInfinity(vector3.z);
    }

    /// <summary>
    /// 比较两个Vector2
    /// </summary>
    /// <param name="vector2"></param>
    /// <param name="targetVector2"></param>
    /// <param name="tolerance">容忍值</param>
    /// <returns></returns>
    public static bool Compare(this Vector2 vector2, Vector2 targetVector2, float tolerance = 0.0f)
    {
        return Compare(vector2.ToVector3Z(), targetVector2.ToVector3Z(), tolerance);
    }

    /// <summary>
    /// 比较两个Vector2
    /// </summary>
    /// <param name="vector2"></param>
    /// <param name="targetVector2"></param>
    /// <param name="xTolerance"></param>
    /// <param name="yTolerance"></param>
    /// <param name="zTolerance"></param>
    /// <returns></returns>
    public static bool Compare(this Vector2 vector2, Vector2 targetVector2, float xTolerance, float yTolerance, float zTolerance)
    {
        return Compare(vector2.ToVector3Z(), targetVector2.ToVector3Z(), xTolerance, yTolerance, zTolerance);
    }

    /// <summary>
    /// 比较两个Vector3
    /// </summary>
    /// <param name="vector3"></param>
    /// <param name="targetVector3"></param>
    /// <param name="tolerance">容忍值</param>
    /// <returns></returns>
    public static bool Compare(this Vector3 vector3, Vector3 targetVector3, float tolerance = 0.0f)
    {
        return vector3.x.Approximately(targetVector3.x, tolerance)
               && vector3.y.Approximately(targetVector3.y, tolerance)
               && vector3.z.Approximately(targetVector3.z, tolerance);
    }

    /// <summary>
    /// 比较两个Vector3
    /// </summary>
    /// <param name="vector3"></param>
    /// <param name="targetVector3"></param>
    /// <param name="xTolerance"></param>
    /// <param name="yTolerance"></param>
    /// <param name="zTolerance"></param>
    /// <returns></returns>
    public static bool Compare(this Vector3 vector3, Vector3 targetVector3, float xTolerance, float yTolerance, float zTolerance)
    {
        return vector3.x.Approximately(targetVector3.x, xTolerance)
               && vector3.y.Approximately(targetVector3.y, yTolerance)
               && vector3.z.Approximately(targetVector3.z, zTolerance);
    }

    /// <summary>
    /// 取 <see cref="UnityEngine.Vector3" /> 的 (x, y, z) 转换为 <see cref="UnityEngine.Vector2" /> 的 (x, y)。
    /// </summary>
    /// <param name="vector3">要转换的 Vector3。</param>
    /// <returns>转换后的 Vector2。</returns>
    public static Vector2 IgnoreZAxis(this Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.y);
    }

    /// <summary>
    /// 取 <see cref="UnityEngine.Vector3" /> 的 (x, y, z) 转换为 <see cref="UnityEngine.Vector2" /> 的 (x, z)。
    /// </summary>
    /// <param name="vector3">要转换的 Vector3。</param>
    /// <returns>转换后的 Vector2。</returns>
    public static Vector2 IgnoreYAxis(this Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.z);
    }

    /// <summary>
    /// 取 <see cref="UnityEngine.Vector3" /> 的 (x, y, z) 转换为 <see cref="UnityEngine.Vector2" /> 的 (y, z)。
    /// </summary>
    /// <param name="vector3">要转换的 Vector3。</param>
    /// <returns>转换后的 Vector2。</returns>
    public static Vector2 IgnoreXAxis(this Vector3 vector3)
    {
        return new Vector2(vector3.y, vector3.z);
    }

    /// <summary>
    /// 取 <see cref="UnityEngine.Vector2" /> 的 (x, y) 和给定参数 x 转换为 <see cref="UnityEngine.Vector3" /> 的 (参数 x, x, y)。
    /// </summary>
    /// <param name="vector2">要转换的 Vector2。</param>
    /// <param name="x">Vector3 的 x 值。</param>
    /// <returns>转换后的 Vector3。</returns>
    public static Vector3 ToVector3X(this Vector2 vector2, float x = 0.0f)
    {
        return new Vector3(x, vector2.x, vector2.y);
    }

    /// <summary>
    /// 取 <see cref="UnityEngine.Vector2" /> 的 (x, y) 和给定参数 y 转换为 <see cref="UnityEngine.Vector3" /> 的 (x, 参数 y, y)。
    /// </summary>
    /// <param name="vector2">要转换的 Vector2。</param>
    /// <param name="y">Vector3 的 y 值。</param>
    /// <returns>转换后的 Vector3。</returns>
    public static Vector3 ToVector3Y(this Vector2 vector2, float y = 0.0f)
    {
        return new Vector3(vector2.x, y, vector2.y);
    }

    /// <summary>
    /// 取 <see cref="UnityEngine.Vector2" /> 的 (x, y) 和给定参数 z 转换为 <see cref="UnityEngine.Vector3" /> 的 (x, y, 参数 z)。
    /// </summary>
    /// <param name="vector2">要转换的 Vector2。</param>
    /// <param name="z">Vector3 的 z 值。</param>
    /// <returns>转换后的 Vector3。</returns>
    public static Vector3 ToVector3Z(this Vector2 vector2, float z = 0.0f)
    {
        return new Vector3(vector2.x, vector2.y, z);
    }

    /// <summary>
    /// 改变Vector3的x,y,z值
    /// </summary>
    /// <param name="vector3"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static Vector3 Change(this Vector3 vector3, float x, float y, float z)
    {
        vector3.Set(x, y, z);
        return vector3;
    }

    /// <summary>
    /// 只改变Vector3的x值
    /// </summary>
    /// <param name="vector3"></param>
    /// <param name="x"></param>
    public static Vector3 ChangeX(this Vector3 vector3, float x)
    {
        vector3.Set(x, vector3.y, vector3.z);
        return vector3;
    }

    /// <summary>
    /// 只改变Vector3的y值
    /// </summary>
    /// <param name="vector3"></param>
    /// <param name="y"></param>
    public static Vector3 ChangeY(this Vector3 vector3, float y)
    {
        vector3.Set(vector3.x, y, vector3.z);
        return vector3;
    }

    /// <summary>
    /// 只改变Vector3的z值
    /// </summary>
    /// <param name="vector3"></param>
    /// <param name="z"></param>
    public static Vector3 ChangeZ(this Vector3 vector3, float z)
    {
        vector3.Set(vector3.x, vector3.y, z);
        return vector3;
    }

    /// <summary>
    /// 只改变Vector3的x,y值
    /// </summary>
    /// <param name="vector3"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public static Vector3 ChangeXY(this Vector3 vector3, float x, float y)
    {
        vector3.Set(x, y, vector3.z);
        return vector3;
    }

    /// <summary>
    /// 只改变Vector3的x,z值
    /// </summary>
    /// <param name="vector3"></param>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public static Vector3 ChangeXZ(this Vector3 vector3, float x, float z)
    {
        vector3.Set(x, vector3.y, z);
        return vector3;
    }

    /// <summary>
    /// 只改变Vector3的y,z值
    /// </summary>
    /// <param name="vector3"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public static Vector3 ChangeYZ(this Vector3 vector3, float y, float z)
    {
        vector3.Set(vector3.x, y, z);
        return vector3;
    }

    /// <summary>
    /// 只增加Vector3的x值
    /// </summary>
    /// <param name="vector3"></param>
    /// <param name="x"></param>
    public static Vector3 AddX(this Vector3 vector3, float x)
    {
        vector3.Set(vector3.x + x, vector3.y, vector3.z);
        return vector3;
    }

    /// <summary>
    /// 只增加Vector3的y值
    /// </summary>
    /// <param name="vector3"></param>
    /// <param name="y"></param>
    public static Vector3 AddY(this Vector3 vector3, float y)
    {
        vector3.Set(vector3.x, vector3.y + y, vector3.z);
        return vector3;
    }

    /// <summary>
    /// 只增加Vector3的z值
    /// </summary>
    /// <param name="vector3"></param>
    /// <param name="z"></param>
    public static Vector3 AddZ(this Vector3 vector3, float z)
    {
        vector3.Set(vector3.x, vector3.y, vector3.z + z);
        return vector3;
    }

    /// <summary>
    /// 增加Vector3的值
    /// </summary>
    /// <param name="self"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static Vector3 Add(this Vector3 self, Vector3 target)
    {
        self.Set(self.x + target.x, self.y + target.y, self.z + target.z);
        return self;
    }

    /// <summary>
    /// 增加Vector3的值
    /// </summary>
    /// <param name="self"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static Vector3 Add(this Vector3 self, float x, float y, float z)
    {
        self.Set(self.x + x, self.y + y, self.z + z);
        return self;
    }

    /// <summary>
    /// 只减掉Vector3的x值
    /// </summary>
    /// <param name="vector3"></param>
    /// <param name="x"></param>
    public static Vector3 MinusX(this Vector3 vector3, float x)
    {
        vector3.Set(vector3.x - x, vector3.y, vector3.z);
        return vector3;
    }

    /// <summary>
    /// 只减掉Vector3的y值
    /// </summary>
    /// <param name="vector3"></param>
    /// <param name="y"></param>
    public static Vector3 MinusY(this Vector3 vector3, float y)
    {
        vector3.Set(vector3.x, vector3.y - y, vector3.z);
        return vector3;
    }

    /// <summary>
    /// 只减掉Vector3的z值
    /// </summary>
    /// <param name="vector3"></param>
    /// <param name="z"></param>
    public static Vector3 MinusZ(this Vector3 vector3, float z)
    {
        vector3.Set(vector3.x, vector3.y, vector3.z - z);
        return vector3;
    }

    /// <summary>
    /// 计算目标点坐标
    /// </summary>
    /// <param name="angle">角度</param>
    /// <param name="startPoint">起点</param>
    /// <param name="distance">距离</param>
    /// <returns>终点坐标</returns>
    public static Vector3 TargetPoint(this Vector3 self, Vector3 angle, float distance)
    {
        var rotation = Quaternion.Euler(angle);
        var forward = rotation * Vector3.forward;
        var endPoint = self + forward * distance;
        return endPoint;
    }

    /// <summary>
    /// 计算目标点角度
    /// </summary>
    /// <param name="self"></param>
    /// <param name="target"></param>
    /// <param name="distance"></param>
    /// <returns></returns>
    public static float TargetAngle(this Vector3 start, Vector3 end)
    {
        Vector2 angleStart = new Vector3(start.x + Vector2.Distance(start, end), start.y) - start;
        Vector2 angleEnd = end - start;
        if (end.y < start.y)
        {
            return 180 - Vector2.Angle(angleStart, angleEnd) + 180;
        }
        else
            return Vector2.Angle(angleStart, angleEnd);
    }

    /// <summary>
    /// 只乘X
    /// </summary>
    /// <param name="self"></param>
    /// <param name="factor"></param>
    /// <returns></returns>
    public static Vector3 MultiplyX(this Vector3 self, float factor)
    {
        self.x *= factor;
        return self;
    }

    /// <summary>
    /// 只乘Y
    /// </summary>
    /// <param name="self"></param>
    /// <param name="factor"></param>
    /// <returns></returns>
    public static Vector3 MultiplyY(this Vector3 self, float factor)
    {
        self.y *= factor;
        return self;
    }

    /// <summary>
    /// 只乘Z
    /// </summary>
    /// <param name="self"></param>
    /// <param name="factor"></param>
    /// <returns></returns>
    public static Vector3 MultiplyZ(this Vector3 self, float factor)
    {
        self.z *= factor;
        return self;
    }

    /// <summary>
    /// Vector3乘法
    /// </summary>
    /// <param name="self"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static Vector3 Multiply(this Vector3 self, float x, float y, float z)
    {
        self.x *= x;
        self.y *= y;
        self.z *= z;
        return self;
    }

    /// <summary>
    /// 乘法
    /// </summary>
    /// <param name="self"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static Vector3 Multiply(this Vector3 self, Vector3 target)
    {
        self.x *= target.x;
        self.y *= target.y;
        self.z *= target.z;
        return self;
    }

    /// <summary>
    /// 指定向量的垂直向量
    /// </summary>
    /// <param name="vector3"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static Vector3 Perpendicular(this Vector3 vector3, float angle)
    {
        if (vector3 == Vector3.zero)
        {
            return Vector3.zero;
        }

        return (Quaternion.LookRotation(vector3) * Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right).normalized;
    }

    /// <summary>
    /// 垂直于给定正前方的正上方向
    /// </summary>
    /// <param name="forward"></param>
    /// <returns></returns>
    public static Vector3 Up(this Vector3 forward)
    {
        return forward.Perpendicular(90).normalized;
    }

    /// <summary>
    /// 垂直于给定正前方的正上方向
    /// </summary>
    /// <param name="forward"></param>
    /// <returns></returns>
    public static Vector3 Down(this Vector3 forward)
    {
        return forward.Perpendicular(270).normalized;
    }

    /// <summary>
    /// 垂直于给定正前方的正左方向
    /// </summary>
    /// <param name="forward"></param>
    /// <returns></returns>
    public static Vector3 Left(this Vector3 forward)
    {
        return forward.Perpendicular(180).normalized;
    }

    /// <summary>
    /// 垂直于给定正前方的正右方向
    /// </summary>
    /// <param name="forward"></param>
    /// <returns></returns>
    public static Vector3 Right(this Vector3 forward)
    {
        return forward.Perpendicular(360).normalized;
    }

    /// <summary>
    /// 垂直于给定正前方的正后方向
    /// </summary>
    /// <param name="forward"></param>
    /// <returns></returns>
    public static Vector3 Back(this Vector3 forward)
    {
        return (forward * -1).normalized;
    }

    /// <summary>
    /// 旋转环绕
    /// </summary>
    /// <param name="point"></param>
    /// <param name="pivot"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public static Vector3 RotateAround(this Vector3 point, Vector3 pivot, Quaternion rotation)
    {
        return rotation * (point - pivot) + pivot;
    }

    /// <summary>
    /// 旋转环绕
    /// </summary>
    /// <param name="point"></param>
    /// <param name="pivot"></param>
    /// <param name="eulerAngles"></param>
    /// <returns></returns>
    public static Vector3 RotateAround(this Vector3 point, Vector3 pivot, Vector3 eulerAngles)
    {
        return RotateAround(point, pivot, Quaternion.Euler(eulerAngles));
    }

    /// <summary>
    /// 转换点
    /// </summary>
    /// <param name="point"></param>
    /// <param name="translation"></param>
    /// <param name="rotation"></param>
    /// <param name="lossyScale"></param>
    /// <returns></returns>
    public static Vector3 TransformPoint(this Vector3 point, Vector3 translation, Quaternion rotation, Vector3 lossyScale)
    {
        return rotation * Vector3.Scale(lossyScale, point) + translation;
    }

    /// <summary>
    /// 反向转换点
    /// </summary>
    /// <param name="point"></param>
    /// <param name="translation"></param>
    /// <param name="rotation"></param>
    /// <param name="lossyScale"></param>
    /// <returns></returns>
    public static Vector3 InverseTransformPoint(this Vector3 point, Vector3 translation, Quaternion rotation, Vector3 lossyScale)
    {
        var scaleInv = new Vector3(1 / lossyScale.x, 1 / lossyScale.y, 1 / lossyScale.z);
        return Vector3.Scale(scaleInv, (Quaternion.Inverse(rotation) * (point - translation)));
    }

    /// <summary>
    /// 根据求和与最大数量求平均Vector2
    /// </summary>
    /// <param name="vectors"></param>
    /// <returns></returns>
    public static Vector2 Average(this IEnumerable<Vector2> vectors)
    {
        var x = 0f;
        var y = 0f;
        var count = 0;

        foreach (var pos in vectors)
        {
            x += pos.x;
            y += pos.y;
            count++;
        }

        if (count == 0)
        {
            return Vector2.zero;
        }

        return new Vector2(x / count, y / count);
    }

    /// <summary>
    /// 根据求和与最大数量求平均Vector3
    /// </summary>
    /// <param name="vectors"></param>
    /// <returns></returns>
    public static Vector3 Average(this IEnumerable<Vector3> vectors)
    {
        var x = 0f;
        var y = 0f;
        var z = 0f;
        var count = 0;

        foreach (var pos in vectors)
        {
            x += pos.x;
            y += pos.y;
            z += pos.z;
            count++;
        }

        if (count == 0)
        {
            return Vector3.zero;
        }

        return new Vector3(x / count, y / count, z / count);
    }

    /// <summary>
    /// 根据求和与最大数量求平均Vector2
    /// </summary>
    /// <param name="vectors"></param>
    /// <returns></returns>
    public static Vector2 Average(this ICollection<Vector2> vectors)
    {
        var count = vectors.Count;
        if (count == 0)
        {
            return Vector2.zero;
        }

        var x = 0f;
        var y = 0f;

        foreach (var pos in vectors)
        {
            x += pos.x;
            y += pos.y;
        }

        return new Vector2(x / count, y / count);
    }

    /// <summary>
    /// 根据求和与最大数量求平均Vector3
    /// </summary>
    /// <param name="vectors"></param>
    /// <returns></returns>
    public static Vector3 Average(this ICollection<Vector3> vectors)
    {
        var count = vectors.Count;

        if (count == 0)
        {
            return Vector3.zero;
        }

        var x = 0f;
        var y = 0f;
        var z = 0f;

        foreach (var pos in vectors)
        {
            x += pos.x;
            y += pos.y;
            z += pos.z;
        }

        return new Vector3(x / count, y / count, z / count);
    }

    /// <summary>
    /// 求中位数Vector2
    /// </summary>
    /// <param name="vectors"></param>
    /// <returns></returns>
    public static Vector2 Median(this IEnumerable<Vector2> vectors)
    {
        var enumerable = vectors as Vector2[] ?? vectors.ToArray();
        var count = enumerable.Length;
        return count == 0 ? Vector2.zero : enumerable.OrderBy(v => v.sqrMagnitude).ElementAt(count / 2);
    }

    /// <summary>
    /// 求中位数Vector3
    /// </summary>
    /// <param name="vectors"></param>
    /// <returns></returns>
    public static Vector3 Median(this IEnumerable<Vector3> vectors)
    {
        var enumerable = vectors as Vector3[] ?? vectors.ToArray();
        var count = enumerable.Length;
        return count == 0 ? Vector3.zero : enumerable.OrderBy(v => v.sqrMagnitude).ElementAt(count / 2);
    }

    /// <summary>
    /// 求中位数Vector2
    /// </summary>
    /// <param name="vectors"></param>
    /// <returns></returns>
    public static Vector2 Median(this ICollection<Vector2> vectors)
    {
        var count = vectors.Count;
        return count == 0 ? Vector2.zero : vectors.OrderBy(v => v.sqrMagnitude).ElementAt(count / 2);
    }

    /// <summary>
    /// 求中位数Vector3
    /// </summary>
    /// <param name="vectors"></param>
    /// <returns></returns>
    public static Vector3 Median(this ICollection<Vector3> vectors)
    {
        var count = vectors.Count;
        return count == 0 ? Vector3.zero : vectors.OrderBy(v => v.sqrMagnitude).ElementAt(count / 2);
    }

    /// <summary>
    /// 基于源Vector3和球面映射半径获得相对映射
    /// </summary>
    /// <param name="source">The source <see href="https://docs.unity3d.com/ScriptReference/Vector3.html">Vector3</see> to be mapped to sphere</param>
    /// <param name="radius">This is a <see cref="float"/> for the radius of the sphere</param>
    public static Vector3 SphericalMapping(Vector3 source, float radius)
    {
        var circ = 2f * Mathf.PI * radius;

        var xAngle = source.x / circ * 360f;
        var yAngle = -(source.y / circ) * 360f;

        source.Set(0.0f, 0.0f, radius);

        var rot = Quaternion.Euler(yAngle, xAngle, 0.0f);
        source = rot * source;

        return source;
    }

    /// <summary>
    /// 基于源Vector3和圆柱体映射半径获得相对映射
    /// </summary>
    /// <param name="source">The source <see href="https://docs.unity3d.com/ScriptReference/Vector3.html">Vector3</see> to be mapped to cylinder</param>
    /// <param name="radius">This is a <see cref="float"/> for the radius of the cylinder</param>
    public static Vector3 CylindricalMapping(Vector3 source, float radius)
    {
        var circ = 2f * Mathf.PI * radius;

        var xAngle = source.x / circ * 360f;

        source.Set(0.0f, source.y, radius);

        var rot = Quaternion.Euler(0.0f, xAngle, 0.0f);
        source = rot * source;

        return source;
    }

    /// <summary>
    /// 基于源Vector3和径向映射半径获得相对映射
    /// </summary>
    /// <param name="source">The source <see href="https://docs.unity3d.com/ScriptReference/Vector3.html">Vector3</see> to be mapped to cylinder</param>
    /// <param name="radialRange">The total range of the radial in degrees as a <see cref="float"/></param>
    /// <param name="radius">This is a <see cref="float"/> for the radius of the radial</param>
    /// <param name="row">The current row as a <see cref="int"/> for the radial calculation</param>
    /// <param name="totalRows">The total rows as a <see cref="int"/> for the radial calculation</param>
    /// <param name="column">The current column as a <see cref="int"/> for the radial calculation</param>
    /// <param name="totalColumns">The total columns as a <see cref="int"/> for the radial calculation</param>
    public static Vector3 RadialMapping(Vector3 source, float radialRange, float radius, int row, int totalRows, int column, int totalColumns)
    {
        var radialCellAngle = radialRange / totalColumns;

        source.x = 0f;
        source.y = 0f;
        source.z = radius / totalRows * row;

        var yAngle = radialCellAngle * (column - totalColumns * 0.5f) + radialCellAngle * .5f;

        var rot = Quaternion.Euler(0.0f, yAngle, 0.0f);
        source = rot * source;

        return source;
    }

    /// <summary>
    /// 基于源Vector3和随机距离半径的随机映射
    /// </summary>
    /// <param name="source">The source <see href="https://docs.unity3d.com/ScriptReference/Vector3.html">Vector3</see> to be mapped to cylinder</param>
    /// <param name="radius">This is a <see cref="float"/> for the radius of the cylinder</param>
    public static Vector3 ScatterMapping(Vector3 source, float radius)
    {
        source.x = UnityEngine.Random.Range(-radius, radius);
        source.y = UnityEngine.Random.Range(-radius, radius);
        return source;
    }

    /// <summary>
    /// 获取物体LookAt后的旋转值
    /// </summary>
    /// <param name="originalPoint"></param>
    /// <param name="targetPoint"></param>
    /// <returns></returns>
    public static Vector3 GetLookAtEuler(this Vector3 originalPoint, Vector3 targetPoint)
    {
        //计算物体在朝向某个向量后的正前方
        var forwardDir = targetPoint - originalPoint;
        //计算朝向这个正前方时的物体四元数值
        var lookAtRot = Quaternion.LookRotation(forwardDir);
        //把四元数值转换成角度
        var resultEuler = lookAtRot.eulerAngles;
        return resultEuler;
    }

    /// <summary>
    /// 点到直线距离
    /// </summary>
    /// <param name="point">点坐标</param>
    /// <param name="startLinePoint">直线上一个点的坐标</param>
    /// <param name="endLinePoint">直线上另一个点的坐标</param>
    /// <returns></returns>
    public static float DistanceOfPointToLine(this Vector3 point, Vector3 startLinePoint, Vector3 endLinePoint)
    {
        var vector1 = point - startLinePoint;
        var vector2 = endLinePoint - startLinePoint;
        var vectorProject = Vector3.Project(vector1, vector2);
        var m1 = Vector3.Magnitude(vector1);
        var p1 = Mathf.Pow(m1, 2);
        var m2 = Vector3.Magnitude(vectorProject);
        var p2 = Mathf.Pow(m2, 2);
        var distance = Mathf.Sqrt(p1 - p2);
        return distance;
    }

    /// <summary>
    /// 位置变换到目标身后
    /// </summary>
    /// <param name="self"></param>
    /// <param name="target"></param>
    /// <param name="smoothTime"></param>
    /// <param name="distance"></param>
    /// <param name="yVelocity"></param>
    public static void GoBehind(this Transform self, Transform target, float smoothTime = 0.0f, float distance = 1.0f, float yVelocity = 0.0f)
    {
        var yAngle = Mathf.SmoothDampAngle(self.eulerAngles.y, target.eulerAngles.y, ref yVelocity, smoothTime);
        var tempPosition = target.position;
        tempPosition += Quaternion.Euler(0, yAngle, 0) * new Vector3(0, 0, -distance);
        self.position = tempPosition;
    }
}