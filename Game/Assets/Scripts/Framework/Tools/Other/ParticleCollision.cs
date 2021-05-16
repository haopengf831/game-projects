using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParticleCollision : MonoBehaviour
{
    [Serializable]
    public class UnityEvent : UnityEvent<GameObject>
    {
    }

    private ParticleSystem m_ParticleSystem;
    public UnityEvent Collision;

    protected virtual void Awake()
    {
        if (!TryGetComponent(out m_ParticleSystem))
        {
            throw new ArgumentNullException(nameof(m_ParticleSystem));
        }
    }

    protected virtual void OnParticleCollision(GameObject other)
    {
        if (other == null)
        {
            return;
        }

        Collision?.Invoke(other);
    }
}