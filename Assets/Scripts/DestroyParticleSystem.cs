using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destroys a gameobject with a particle system after it has finished its emission.
/// </summary>
public class DestroyParticleSystem : MonoBehaviour
{
    [Tooltip("Use to override how long a particle system exists for. Leave below zero to be ignored.")]
    public float destroyTimeOverride = -1;
    [Tooltip("Additional delay on the destroy time. Optional, use as needed if things are getting cut off.")]
    public float destroyDelay = 0f;

    private ParticleSystem m_ParticleSystem;
    private float destroyTime;


    private void Awake()
    {
        m_ParticleSystem = this.GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        SetDestroyTime();

        Destroy(this.gameObject, destroyTime);
    }

    private void SetDestroyTime()
    {
        if (destroyTimeOverride > 0)
            destroyTime = destroyTimeOverride;
        else
        {
            destroyTime = m_ParticleSystem.main.duration + m_ParticleSystem.main.startLifetimeMultiplier + destroyDelay;
        }
    }
}
