using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropTileSownState : State
{
    private CropTile _cropTile;
    //private CropData _cropData;
    private CropSpawner _cropSpawner;
    GenericPool<Crop> cropPool;

    public CropTileSownState(GenericPool<Crop> cropPool,CropSpawner cropSpawner,CropTile cropTile)
    {   
       this.cropPool = cropPool;
        _cropSpawner = cropSpawner;
        _cropTile = cropTile;
    }
    public override void EnterState()
    {
        _cropTile.IsSown = true;
        Sow();
        _cropTile.CurrentCrop = _cropSpawner.SpawnedCrop;
    }

    public override void TickState()
    {

    }

    public override void ExitState()
    {
        cropPool.Set(_cropTile.CurrentCrop); // Back to pool
    }

    public void Sow()
    {      
        _cropSpawner.SpawnCrop(_cropTile, cropPool);        
    }
}
