using UnityEngine;

/// <summary>
/// 空心棱柱碰撞体
/// </summary>
public class HollowPrismCollider : ColliderBase
{
    #region Setting

    public float Thickness = 0.1f;

    #endregion

    #region Method

    protected override void OnDrawGizmosSelected()
    {
        if (!enabled)
        {
            return;
        }

        if (!IsShowGizmos)
        {
            return;
        }

        var radiusStep = CalculateRotationStep();
        var boxSize = CalculateBoxSize();
        var rotationY = 0.0f;
        
        for (int i = 0; i < BoxCount; i++)
        {
            var angle = Vector3.zero.ChangeY(rotationY);
            var pos = Vector3.zero.TargetPoint(angle, Radius);

            var oldMatrix = Gizmos.matrix;
            var newMatrix = Matrix4x4.TRS(pos, Quaternion.Euler(angle), Vector3.one);
            Gizmos.matrix = transform.localToWorldMatrix * newMatrix;
            Gizmos.DrawWireCube(Center, boxSize);
            Gizmos.matrix = oldMatrix;

            rotationY += radiusStep;
        }
    }

    public override void CreateCollides()
    {
        base.CreateCollides();
        var radiusStep = CalculateRotationStep();
        var boxSize = CalculateBoxSize();

        for (int i = 0; i < BoxCount; i++)
        {
            var newCollider = CreateBoxCollider(boxSize, radiusStep * i);
            newCollider.name = ColliderParent.name + "_Collider " + (i + 1);
            newCollider.transform.SetParent(transform, false);
            newCollider.transform.SetParent(ColliderParent, true);

            Collides.Add(newCollider);
        }
    }

    protected virtual BoxCollider CreateBoxCollider(Vector3 size, float rotationY)
    {
        var boxCollider = new GameObject().AddComponent<BoxCollider>();
        boxCollider.center = Center;
        boxCollider.size = size;
        var angle = Vector3.zero.ChangeY(rotationY);
        var boxColliderTransform = boxCollider.transform;
        boxColliderTransform.localPosition = Vector3.zero.TargetPoint(angle, Radius);
        boxColliderTransform.localRotation = Quaternion.Euler(angle);
        boxColliderTransform.localScale = Vector3.one;
        boxCollider.isTrigger = IsTrigger;
        boxCollider.material = PhysicMaterial;
        return boxCollider;
    }

    protected override Vector3 CalculateBoxSize()
    {
        var width = 2 * Radius * Mathf.Tan(Mathf.PI / BoxCount);
        var thickness = Radius / BoxCount * 2.0f * Thickness;
        return new Vector3(width, Height, thickness);
    }

    #endregion
}