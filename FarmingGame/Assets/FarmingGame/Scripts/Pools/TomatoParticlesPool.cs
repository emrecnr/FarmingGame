using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoParticlesPool : GenericPool<ParticleSystem>
{
    public static TomatoParticlesPool Instance { get; private set; }
    protected override void SingletonObject()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
