using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderCollision : MonoBehaviour
{
    [SerializeField] public bool IsColliding { get; private set; }
    [SerializeField] public List<Collision> Collisions { get; private set; } = new List<Collision>();

    [Serializable]
    public class UnityEvent : UnityEvent<Collision>
    {
    }

    public UnityEvent CollisionEnter;
    public UnityEvent CollisionStay;
    public UnityEvent CollisionExit;

    private void OnCollisionEnter(Collision other)
    {
        if (other != null)
        {
            Collisions?.AddUnique(other);
            IsColliding = true;
            CollisionEnter?.Invoke(other);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other != null)
        {
            IsColliding = true;
            CollisionStay?.Invoke(other);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other != null)
        {
            Collisions?.Remove(other);
            IsColliding = false;
            CollisionExit?.Invoke(other);
        }
    }
}