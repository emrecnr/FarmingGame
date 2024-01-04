using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimationEvents : MonoBehaviour
{
    [Header("--- REFERENCES ---")]
    [SerializeField] private ParticleSystem seedParticles;
    [SerializeField] private ParticleSystem waterParticles;

    public event Action startHarvestingEvent;
    public event Action  stopHarvestingEvent;

    private void PlaySeedParticles()
    {
        seedParticles.Play();
    }

    private void PlayWaterParticles()
    {
        waterParticles.Play();
    }

    private void StartHarvestingEvent()
    {
        startHarvestingEvent?.Invoke();
    }

    private void StopHarvestingEvent()
    {
        stopHarvestingEvent?.Invoke();
    }

}

