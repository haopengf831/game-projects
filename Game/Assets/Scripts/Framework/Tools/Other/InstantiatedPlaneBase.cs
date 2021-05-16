using UnityEngine;

public class InstantiatedPlaneBase : MonoBehaviour
{
    public bool IsDebug;
    protected Plane ThePlane = new Plane();
    protected GameObject VisualPlane;

    /// <summary>
    ///   <para>Normal vector of the plane.</para>
    /// </summary>
    public Vector3 Normal => ThePlane.normal;

    /// <summary>
    ///   <para>Distance from the origin to the plane.</para>
    /// </summary>
    public float Distance => ThePlane.distance;

    /// <summary>
    /// 使用位于平面内的点和法线来设定平面的方向。
    /// </summary>
    public virtual void SetNormalAndPosition(Vector3 inNormal)
    {
        try
        {
            ThePlane.SetNormalAndPosition(inNormal, transform.position);
            CreateVisualPlane();
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// 使用位于平面内的点和法线来设定平面的方向。
    /// </summary>
    protected virtual void SetNormalAndPosition()
    {
        try
        {
            var planeTransform = transform;
            var planeForward = planeTransform.forward;
            var planePosition = planeTransform.position;
            ThePlane.SetNormalAndPosition(planeForward, planePosition);
            CreateVisualPlane();
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// 给定的point是否在平面的正面?
    /// </summary>
    /// <param name="point">笔尖的位置</param>
    /// <returns>true 在正面;false 在背面</returns>
    public virtual bool GetSide(Vector3 point)
    {
        try
        {
            return ThePlane.GetSide(point);
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// 返回从平面到点的有符号距离。
    /// </summary>
    /// <param name="point">笔尖的位置</param>
    /// <returns>当在正面的时候返回正值，当在背面的时候返回负值</returns>
    public virtual float GetDistanceToPoint(Vector3 point)
    {
        try
        {
            return ThePlane.GetDistanceToPoint(point);
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// 对于给定点，返回平面上最近的点。
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public virtual Vector3 ClosestPointOnPlane(Vector3 point)
    {
        try
        {
            var closestPoint = ThePlane.ClosestPointOnPlane(point);
            return closestPoint;
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// 创建可视化平面
    /// </summary>
    public virtual void CreateVisualPlane()
    {
        try
        {
            if (!IsDebug)
            {
                if (VisualPlane != null)
                {
                    VisualPlane.Release();
                }

                return;
            }

            if (VisualPlane == null)
            {
                VisualPlane = GameObject.CreatePrimitive(PrimitiveType.Quad);
                VisualPlane.RemoveComponent<Collider>();
                VisualPlane.transform.SetParent(transform.parent);
                VisualPlane.transform.forward = ThePlane.normal;
                VisualPlane.transform.position = transform.position;
            }
        }
        catch
        {
            throw;
        }
    }
}