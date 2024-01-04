using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CropFieldWateredState : State
{   
    private CropData _cropData;
    private CropTile _cropTile;
    private CropSpawner _cropSpawner;   
    private GenericPool<Crop> _cropPool;
    private GenericPool<ParticleSystem> _cropParticle;

    private ParticleSystem _currentParticle;

    public CropFieldWateredState(CropData cropData,CropTile cropTile,CropSpawner cropSpawner,GenericPool<Crop> cropPool,GenericPool<ParticleSystem> cropParticle)
    {
        _cropPool = cropPool;
        _cropParticle = cropParticle;
        _cropData = cropData;
        _cropTile = cropTile;
        _cropSpawner = cropSpawner;
    }
    public override void EnterState()
    {
        Water();
        _cropTile.CurrentCrop = _cropSpawner.SpawnedCrop;
    }

    public override void ExitState() 
    {
        _cropTile.gameObject.LeanColor(Color.white,1); 
        _cropPool.Set(_cropTile.CurrentCrop);
        _currentParticle = _cropParticle.Get();
        Vector3 spawnOffset = new Vector3(0,0.2f,0);
        _currentParticle.transform.position = _cropTile.transform.position + spawnOffset;
        _currentParticle.gameObject.SetActive(true);
        

        CropTile.OnCropHarvested?.Invoke(_cropData.cropType);
        CropTile.OnCropHarvestedExperience?.Invoke(_cropData.experience);

    }

    public override void TickState()
    {
        
    }

    private void Water()
    {
        _cropTile.TileRenderer.gameObject.LeanColor(Color.white * .3f, 1);
        _cropSpawner.SpawnCrop(_cropTile,_cropPool);        

        _cropTile.IsWatered = true;

    }
 
}




