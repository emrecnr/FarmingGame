using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropFieldWateredState : State
{
    private CropTile _cropTile;
    private CropSpawner _cropSpawner;
    private CropData _currentCropFullData;

    private float buyumeHizi = 10;
    private float time;

    public CropFieldWateredState(CropTile cropTile,CropSpawner cropSpawner,CropData cropDataFull)
    {
        _cropTile = cropTile;
        _cropSpawner = cropSpawner;
        _currentCropFullData = cropDataFull;
    }
    public override void EnterState()
    {
        Water();
        _cropTile.StartC();
    }

    public override void ExitState()
    {
        
    }

    public override void TickState()
    {
        
    }

    private void Water()
    {
        //_cropTile.TileRenderer.material.color = Color.white *.3f;
        _cropTile.TileRenderer.gameObject.LeanColor(Color.white * .3f, 1);
        _cropSpawner.SpawnCropFull(_currentCropFullData,_cropTile);
        

        _cropTile.IsWatered = true;

    }
 
}




