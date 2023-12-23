using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Particles : MonoBehaviour
{
    protected virtual void HandleParticleCollision(GameObject other, Action<Vector3[]> OnCollided)
    {
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();

        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        int collisionAmount = particleSystem.GetCollisionEvents(other, collisionEvents);

        Vector3[] collisionPositions = new Vector3[collisionAmount];

        for (int i = 0; i < collisionAmount; i++)
            collisionPositions[i] = collisionEvents[i].intersection;

        OnCollided?.Invoke(collisionPositions);
    }
}
