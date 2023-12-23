using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [Header("--- REFERENCES ---")]
    [SerializeField] private ParticleSystem seedParticles;
    [SerializeField] private ParticleSystem waterParticles;


    private void PlaySeedParticles()
    {
        seedParticles.Play();
    }
    private void PlayWaterParticles()
    {
        waterParticles.Play(); 
    }

}

