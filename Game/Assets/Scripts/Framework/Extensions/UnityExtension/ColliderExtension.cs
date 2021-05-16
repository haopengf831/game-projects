using System;
using UnityEngine;

/// <summary>
/// Unity Collider类的扩展方法
/// </summary>
public static partial class ColliderExtension
{
    /// <summary>
    /// 复制Collider
    /// </summary>
    /// <param name="collider"></param>
    /// <param name="beCopiedCollider"></param>
    public static Collider CopyCollider(this Collider collider, Collider beCopiedCollider)
    {
        if (collider == null)
        {
            throw new ArgumentNullException(nameof(collider));
        }

        if (beCopiedCollider == null)
        {
            throw new ArgumentNullException(nameof(beCopiedCollider));
        }

        var colliderType = collider.GetType();
        var beCopiedColliderType = beCopiedCollider.GetType();
        if (colliderType != beCopiedColliderType)
        {
            throw new ArgumentException("Inconsistent Collider Types" + colliderType.Name + "/" + beCopiedColliderType.Name);
        }

        if (colliderType == typeof(MeshCollider))
        {
            var meshCollider = collider as MeshCollider;
            var beCopiedColliderMeshCollider = beCopiedCollider as MeshCollider;
            if (meshCollider != null && beCopiedColliderMeshCollider != null)
            {
                meshCollider.CopyMeshCollider(beCopiedColliderMeshCollider);
            }
        }
        else
        {
            if (colliderType == typeof(BoxCollider))
            {
                var boxCollider = collider as BoxCollider;
                var beCopiedColliderBoxCollider = beCopiedCollider as BoxCollider;
                if (boxCollider != null && beCopiedColliderBoxCollider != null)
                {
                    boxCollider.CopyBoxCollider(beCopiedColliderBoxCollider);
                }
            }
            else
            {
                if (colliderType == typeof(SphereCollider))
                {
                    var sphereCollider = collider as SphereCollider;
                    var beCopiedColliderSphereCollider = beCopiedCollider as SphereCollider;
                    if (sphereCollider != null && beCopiedColliderSphereCollider != null)
                    {
                        sphereCollider.CopySphereCollider(beCopiedColliderSphereCollider);
                    }
                }
                else
                {
                    if (colliderType == typeof(CapsuleCollider))
                    {
                        var capsuleColliderCollider = collider as CapsuleCollider;
                        var beCopiedColliderCapsuleCollider = beCopiedCollider as CapsuleCollider;
                        if (capsuleColliderCollider != null && beCopiedColliderCapsuleCollider != null)
                        {
                            capsuleColliderCollider.CopyCapsuleCollider(beCopiedColliderCapsuleCollider);
                        }
                    }
                }
            }
        }

        return collider;
    }

    /// <summary>
    /// 复制MeshCollider
    /// </summary>
    /// <param name="collider"></param>
    /// <param name="beCopiedCollider"></param>
    public static Collider CopyMeshCollider(this MeshCollider collider, MeshCollider beCopiedCollider)
    {
        if (collider == null)
        {
            throw new ArgumentNullException(nameof(collider));
        }

        if (beCopiedCollider == null)
        {
            throw new ArgumentNullException(nameof(beCopiedCollider));
        }

        collider.convex = beCopiedCollider.convex;
        collider.isTrigger = beCopiedCollider.isTrigger;
        collider.cookingOptions = collider.cookingOptions;
        collider.material = beCopiedCollider.material;
        collider.sharedMaterial = beCopiedCollider.sharedMaterial;
        collider.sharedMesh = beCopiedCollider.sharedMesh;

        return collider;
    }

    /// <summary>
    /// 复制BoxCollider
    /// </summary>
    /// <param name="collider"></param>
    /// <param name="beCopiedCollider"></param>
    public static BoxCollider CopyBoxCollider(this BoxCollider collider, BoxCollider beCopiedCollider)
    {
        if (collider == null)
        {
            throw new ArgumentNullException(nameof(collider));
        }

        if (beCopiedCollider == null)
        {
            throw new ArgumentNullException(nameof(beCopiedCollider));
        }

        collider.isTrigger = beCopiedCollider.isTrigger;
        collider.material = beCopiedCollider.material;
        collider.sharedMaterial = beCopiedCollider.sharedMaterial;
        collider.center = beCopiedCollider.center;
        collider.size = beCopiedCollider.size;

        return collider;
    }

    /// <summary>
    /// 复制SphereCollider
    /// </summary>
    /// <param name="collider"></param>
    /// <param name="beCopiedCollider"></param>
    public static SphereCollider CopySphereCollider(this SphereCollider collider, SphereCollider beCopiedCollider)
    {
        if (collider == null)
        {
            throw new ArgumentNullException(nameof(collider));
        }

        if (beCopiedCollider == null)
        {
            throw new ArgumentNullException(nameof(beCopiedCollider));
        }

        collider.isTrigger = beCopiedCollider.isTrigger;
        collider.material = beCopiedCollider.material;
        collider.sharedMaterial = beCopiedCollider.sharedMaterial;
        collider.center = beCopiedCollider.center;
        collider.radius = beCopiedCollider.radius;

        return collider;
    }

    /// <summary>
    /// 复制CapsuleCollider
    /// </summary>
    /// <param name="collider"></param>
    /// <param name="beCopiedCollider"></param>
    public static CapsuleCollider CopyCapsuleCollider(this CapsuleCollider collider, CapsuleCollider beCopiedCollider)
    {
        if (collider == null)
        {
            throw new ArgumentNullException(nameof(collider));
        }

        if (beCopiedCollider == null)
        {
            throw new ArgumentNullException(nameof(beCopiedCollider));
        }

        collider.isTrigger = beCopiedCollider.isTrigger;
        collider.material = beCopiedCollider.material;
        collider.sharedMaterial = beCopiedCollider.sharedMaterial;
        collider.center = beCopiedCollider.center;
        collider.radius = beCopiedCollider.radius;
        collider.height = beCopiedCollider.height;
        collider.direction = beCopiedCollider.direction;

        return collider;
    }

    /// <summary>
    /// 设置Collider的isTrigger属性
    /// </summary>
    /// <param name="collider"></param>
    /// <param name="isTrigger"></param>
    public static Collider SetTrigger(this Collider collider, bool isTrigger)
    {
        if (collider == null)
        {
            throw new ArgumentNullException(nameof(collider));
        }

        if (collider.isTrigger != isTrigger)
        {
            var meshCollider = collider as MeshCollider;
            if (meshCollider != null)
            {
                meshCollider.convex = true;
            }

            collider.isTrigger = true;
        }

        return collider;
    }
}