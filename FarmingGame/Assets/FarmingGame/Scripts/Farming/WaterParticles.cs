using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaterParticles : Particles
{
    public static Action<Vector3[]> onWaterCollided;

    private void OnParticleCollision(GameObject other)
    {
        HandleParticleCollision(other, onWaterCollided);
    }
    protected override void HandleParticleCollision(GameObject other, Action<Vector3[]> OnCollided)
    {
        base.HandleParticleCollision(other, OnCollided);
    }
}
