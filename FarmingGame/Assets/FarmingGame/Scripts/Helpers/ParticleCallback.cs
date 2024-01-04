using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCallback : MonoBehaviour
{
    [SerializeField] private GenericPool<ParticleSystem> pool;
    [SerializeField] private ParticleSystem particle;


    private void Start() {
        pool = GetComponentInParent<GenericPool<ParticleSystem>>();
    }

    private void OnParticleSystemStopped() {
        pool.Set(particle);
    }

}
