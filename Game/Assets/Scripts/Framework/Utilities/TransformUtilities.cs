using UnityEngine;

public static class TransformUtilities
{
    /// <summary>
    /// Takes a point in the coordinate space specified by the "from" transform and transforms it to be the correct
    /// point in the coordinate space specified by the "to" transform applies rotation, scale and translation.
    /// </summary>
    /// <returns>Point to.</returns>
    public static Vector3 TransformPointFromTo(Transform from, Transform to, Vector3 fromPoint)
    {
        Vector3 worldPoint = (from == null) ? fromPoint : from.TransformPoint(fromPoint);
        return (to == null) ? worldPoint : to.InverseTransformPoint(worldPoint);
    }

    /// <summary>
    /// Takes a direction in the coordinate space specified by the "from" transform and transforms it to be the correct direction in the coordinate space specified by the "to" transform
    /// applies rotation only, no translation or scale
    /// </summary>
    /// <returns>Direction to.</returns>
    public static Vector3 TransformDirectionFromTo(Transform from, Transform to, Vector3 fromDirection)
    {
        Vector3 worldDirection = (from == null) ? fromDirection : from.TransformDirection(fromDirection);
        return (to == null) ? worldDirection : to.InverseTransformDirection(worldDirection);
    }

    /// <summary>
    /// Takes a vector in the coordinate space specified by the "from" transform and transforms it to be the correct direction in the coordinate space specified by the "to" transform
    /// applies rotation and scale, no translation
    /// </summary>
    public static Vector3 TransformVectorFromTo(Transform from, Transform to, Vector3 vecInFrom)
    {
        Vector3 vecInWorld = (from == null) ? vecInFrom : from.TransformVector(vecInFrom);
        Vector3 vecInTo = (to == null) ? vecInWorld : to.InverseTransformVector(vecInWorld);
        return vecInTo;
    }

    /// <summary>
    /// Takes a ray in the coordinate space specified by the "from" transform and transforms it to be the correct ray in the coordinate space specified by the "to" transform
    /// </summary>
    public static Ray TransformRayFromTo(Transform from, Transform to, Ray rayToConvert)
    {
        Ray outputRay = new Ray
        {
            origin = TransformPointFromTo(from, to, rayToConvert.origin),
            direction = TransformDirectionFromTo(from, to, rayToConvert.direction)
        };

        return outputRay;
    }
}