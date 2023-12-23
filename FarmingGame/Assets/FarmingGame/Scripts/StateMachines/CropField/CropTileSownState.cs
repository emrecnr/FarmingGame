using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropTileSownState : State
{
    private CropData _currentCropData;
    private CropSpawner _cropSpawner;
    private CropTile _cropTile;

    public CropTileSownState(CropData currentCropData,CropSpawner cropSpawner,CropTile cropTile)
    {
        _currentCropData = currentCropData;
        _cropSpawner = cropSpawner;
        _cropTile = cropTile;
    }
    public override void EnterState()
    {
        _cropTile.IsSown = true;
        Sow(_currentCropData);
    }

    public override void TickState()
    {

    }

    public override void ExitState()
    {
        
    }

    public void Sow(CropData cropData)
    {      
        _cropSpawner.SpawnCrop(_cropTile,cropData);
        Debug.Log("Crop Sown: "+ cropData);
    }
}
