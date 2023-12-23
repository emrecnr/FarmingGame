using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropFieldEmptyState : State
{
    private CropTile _cropTile;


    public CropFieldEmptyState(CropTile cropTile)
    {
        _cropTile = cropTile;
    }

    public override void EnterState()
    {
       _cropTile.IsEmpty = true;
    }

    public override void ExitState()
    {
        _cropTile.IsEmpty = false;
    }

    public override void TickState()
    {
        
    }
}
