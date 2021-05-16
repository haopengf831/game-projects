using System;
using UnityEngine;
using UnityEngine.Events;

public class ParticleTrigger : MonoBehaviour
{
    public UnityEvent Trigger;
    private ParticleSystem m_ParticleSystem;

    protected virtual void Awake()
    {
        if (!TryGetComponent(out m_ParticleSystem))
        {
            throw new ArgumentNullException(nameof(m_ParticleSystem));
        }
    }

    protected virtual void OnParticleTrigger()
    {
        Trigger?.Invoke();
    }
}