using System.Collections.Generic;
using UnityEngine;

public abstract class ColliderBase : MonoBehaviour
{
    #region Setting

    [Header("Editor Setting")] public bool IsShowGizmos = true;
    public Color GizmosColor = Color.gray;
    public bool CouldClearCollides = true;

    [Header("Collider Setting")] [SerializeField]
    protected Transform BaseParent;

    public PhysicMaterial PhysicMaterial;
    public bool IsTrigger;

    public Transform ColliderParent
    {
        get
        {
            var colliderParent = BaseParent;
            if (colliderParent == null)
            {
                colliderParent = transform;
            }

            return colliderParent;
        }
        set => BaseParent = value;
    }

    [DisplayOnly] public List<Collider> Collides = new List<Collider>();

    [Space, Range(3, 128)] public int BoxCount = 3;
    public Vector3 Center = Vector3.zero;
    public float Radius = 0.01f;
    public float Height = 0.5f;

    #endregion

    #region Method

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = GizmosColor;
        OnDrawGizmosSelected();
    }

    protected abstract void OnDrawGizmosSelected();

    public virtual void CreateCollides()
    {
        ClearCollides();
    }

    public virtual void ClearCollides()
    {
        if (!CouldClearCollides)
        {
            return;
        }

        Collides?.ForEach(tempCollider =>
        {
            if (tempCollider != null)
            {
                tempCollider.ReleaseImmediate();
            }
        });
        Collides?.Clear();
    }

    protected abstract Vector3 CalculateBoxSize();

    protected virtual float CalculateRotationStep()
    {
        return 360.0f / BoxCount;
    }

    public virtual void ClearChildren()
    {
        ClearCollides();
        gameObject.DestroyChildrenImmediate();
    }

    #endregion
}