using UnityEngine;

/// <summary>
/// 实心棱柱碰撞体
/// </summary>
public class SolidPrismCollider : ColliderBase
{
    #region Setting

    [Range(0.1f, 3.0f)] public float WidthScale = 1.0f;
    public bool CapTop;
    public bool CapBottom;

    #endregion

    #region Method

    protected override void OnDrawGizmosSelected()
    {
        if (enabled == false)
        {
            return;
        }

        if (!IsShowGizmos)
        {
            return;
        }

        var radiusStep = CalculateRotationStep();
        var rotationY = 0.0f;
        var boxSize = CalculateBoxSize();

        for (int i = 0; i < BoxCount; i++)
        {
            var oldMatrix = Gizmos.matrix;
            var newMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0.0f, rotationY, 0.0f), Vector3.one);
            Gizmos.matrix = transform.localToWorldMatrix * newMatrix;
            Gizmos.DrawWireCube(Center, boxSize);
            Gizmos.matrix = oldMatrix;

            rotationY += radiusStep;
        }

        var capOffset = new Vector3(0.0f, Height * 0.5f, 0.0f);
        if (CapBottom)
        {
            var oldMatrix = Gizmos.matrix;
            var newMatrix = Matrix4x4.TRS(capOffset * -1.0f, Quaternion.identity, Vector3.one);
            Gizmos.matrix = transform.localToWorldMatrix * newMatrix;
            Gizmos.DrawWireSphere(Vector3.zero, Radius);
            Gizmos.matrix = oldMatrix;
        }

        if (CapTop)
        {
            var oldMatrix = Gizmos.matrix;
            var newMatrix = Matrix4x4.TRS(capOffset, Quaternion.identity, Vector3.one);
            Gizmos.matrix = transform.localToWorldMatrix * newMatrix;
            Gizmos.DrawWireSphere(Vector3.zero, Radius);
            Gizmos.matrix = oldMatrix;
        }
    }

    public override void CreateCollides()
    {
        base.CreateCollides();

        var boxSize = CalculateBoxSize();
        var radiusStep = CalculateRotationStep();

        var tempBoxCount = BoxCount;
        if (BoxCount % 2 == 0)
        {
            tempBoxCount = BoxCount / 2;
        }

        for (int i = 0; i < tempBoxCount; i++)
        {
            var newBoxCollider = CreateBoxCollider(i, boxSize, radiusStep * i);
            newBoxCollider.name = ColliderParent.name + "_BoxCollider " + (i + 1);
            newBoxCollider.transform.SetParent(transform, false);
            newBoxCollider.transform.SetParent(ColliderParent, true);

            Collides.Add(newBoxCollider);
        }

        if (CapTop)
        {
            var newCapTopCollider = CreateCapCollider(true);
            newCapTopCollider.name = ColliderParent.name + "_CapTopCollider";
            newCapTopCollider.transform.SetParent(transform, false);
            newCapTopCollider.transform.SetParent(ColliderParent, true);

            Collides.Add(newCapTopCollider);
        }

        if (CapBottom)
        {
            var newCapBottomCollider = CreateCapCollider(false);
            newCapBottomCollider.name = ColliderParent.name + "_CapBottomCollider";
            newCapBottomCollider.transform.SetParent(transform, false);
            newCapBottomCollider.transform.SetParent(ColliderParent, true);

            Collides.Add(newCapBottomCollider);
        }
    }

    private BoxCollider CreateBoxCollider(int index, Vector3 size, float rotationY)
    {
        var newBoxCollider = new GameObject("Cylinder_Box_" + index).AddComponent<BoxCollider>();
        newBoxCollider.center = Center;
        newBoxCollider.size = size;
        newBoxCollider.transform.localRotation = Quaternion.Euler(0.0f, rotationY, 0.0f);
        newBoxCollider.isTrigger = IsTrigger;
        newBoxCollider.material = PhysicMaterial;
        return newBoxCollider;
    }

    private SphereCollider CreateCapCollider(bool isTop)
    {
        var newSphereCollider = new GameObject("Cylinder_Cap_" + (isTop ? "Top" : "Bottom")).AddComponent<SphereCollider>();
        newSphereCollider.radius = Radius;
        newSphereCollider.center = new Vector3(0.0f, Height * (isTop ? 0.5f : -0.5f), 0.0f);
        newSphereCollider.isTrigger = IsTrigger;
        newSphereCollider.material = PhysicMaterial;
        return newSphereCollider;
    }

    protected override Vector3 CalculateBoxSize()
    {
        var width = Radius / BoxCount * 2.0f * WidthScale;
        return new Vector3(width, Height, Radius * 2.0f);
    }

    #endregion
}