using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderTrigger : MonoBehaviour
{
    [Serializable]
    public class UnityEvent : UnityEvent<Collider>
    {
    }

    [DisplayOnly] public bool IsTrigger;
    [SerializeField] public List<Collider> Colliders { get; private set; } = new List<Collider>();

    public UnityEvent TriggerEnter;
    public UnityEvent TriggerStay;
    public UnityEvent TriggerExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            IsTrigger = true;
            TriggerEnter?.Invoke(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other != null)
        {
            Colliders?.AddUnique(other);
            IsTrigger = true;
            TriggerStay?.Invoke(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null)
        {
            Colliders?.Remove(other);
            IsTrigger = false;
            TriggerExit?.Invoke(other);
        }
    }
}