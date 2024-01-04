using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornParticlesPool : GenericPool<ParticleSystem>
{
    public static CornParticlesPool Instance {get; private set;}
    protected override void SingletonObject()
    {
        if(Instance == null)
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
