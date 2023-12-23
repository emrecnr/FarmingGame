using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class SeedsParticles : Particles
{
    public static Action<Vector3[]> onSeedsCollided;
    
    private void OnParticleCollision(GameObject other)
    {
        HandleParticleCollision(other,onSeedsCollided);
    }
    protected override void HandleParticleCollision(GameObject other, Action<Vector3[]> OnCollided)
    {
        base.HandleParticleCollision(other, OnCollided);
    }
} 
