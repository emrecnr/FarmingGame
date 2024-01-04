using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropField : MonoBehaviour
{
    [Header("---- CROP DATA ----")]
    [SerializeField] private CropData cropData;
    [SerializeField] private GenericPool<Crop> cropPool;
    [SerializeField] private GenericPool<ParticleSystem> cropHarvestParticlePool;

    [Header("---- REFERENCES ----")]
    [SerializeField] private Transform cropTilesParent;
    [SerializeField] private List<CropTile> cropTiles = new List<CropTile>();

    [Header("--- ACTIONS ----")]
    public static Action<CropField> OnFullySown;
    public static Action<CropField> OnFullyWatered;
    public static Action<CropField> OnFullyHarvested;
    
    private int tilesSown; 
    private int tilesWatered;
    private int tilesHarvested;

    [SerializeField] private CropSpawner cropSpawner;

    private void Start()
    {       
        StoreTiles();
    } 

    private void StoreTiles()
    {
        for (int i = 0; i < cropTilesParent.childCount; i++)
        {
            cropTiles.Add(cropTilesParent.GetChild(i).GetComponent<CropTile>());
        }
    }

    public void SeedsCollidedCallback(Vector3[] seedPositions) 
    {
        for (int i = 0; i < seedPositions.Length; i++)
        {
            CropTile closestCropTile = GetClosestCropTile(seedPositions[i]);

            if (closestCropTile == null) continue;
            if (!closestCropTile.IsEmpty) continue;
            Sow(closestCropTile);
        }
    }
    public void WaterCollidedCallback(Vector3[] waterPositions)    
    {
        for (int i = 0; i < waterPositions.Length; i++)
        {
            CropTile closestCropTile = GetClosestCropTile(waterPositions[i]);

            if (closestCropTile == null) continue;
            if(!closestCropTile.IsSown)continue ;
            if(closestCropTile.IsWatered) continue;        
            Water(closestCropTile);
        }
    }
    public void HarvestCallback(Transform harvestAreaPosition)
    {
        float radius = harvestAreaPosition.localScale.x;
        for (int i = 0; i < cropTiles.Count; i++)
        {
            if(cropTiles[i].IsEmpty) continue;
            if (!cropTiles[i].IsWatered) continue;


            float distanceCropTileSphere = Vector3.Distance(harvestAreaPosition.position,cropTiles[i].transform.position);

            if(distanceCropTileSphere<radius)
            {
                Harvest(cropTiles[i]);
            }
       }

    }
    

    private void Sow(CropTile cropTile)
    {
        cropTile.SwitchState(new CropTileSownState(SeedsPool.Instance, cropSpawner,cropTile));
        tilesSown++;
        if(IsFieldFullySown()) FieldFullySown(); 
    }

    private void Water(CropTile cropTile)
    {
        cropTile.SwitchState(new CropFieldWateredState(cropData,cropTile,cropSpawner,cropPool,cropHarvestParticlePool));
        tilesWatered++;

        if(IsFieldFullyWatered()) FieldFullWatered();
            }

    private void Harvest(CropTile cropTile)
    {
        cropTile.SwitchState(new CropFieldEmptyState(cropTile));
        tilesHarvested++;
        if(IsFieldFullyHarvested()) FieldFullHarvested();
    }

    private void FieldFullySown()
    {
        OnFullySown?.Invoke(this);
    }

    private void FieldFullWatered()
    {
        OnFullyWatered?.Invoke(this);
    }

    private void FieldFullHarvested()
    {
        tilesSown = 0;
        tilesWatered = 0;
        tilesHarvested = 0;
        OnFullyHarvested?.Invoke(this);
    }

    private CropTile GetClosestCropTile(Vector3 seedPositions)
    {
        float minDistance = 2;
        int closestCropTileIndex = -1;

        for (int i = 0; i < cropTiles.Count; i++)
        {
            CropTile cropTile = cropTiles[i];
            float distanceTileSeed = Vector3.Distance(cropTile.transform.position, seedPositions);

            if (distanceTileSeed < minDistance)
            {
                minDistance = distanceTileSeed;
                closestCropTileIndex = i;
            }
        }
        if (closestCropTileIndex == -1)
        {
            return null;
        }
        return cropTiles[closestCropTileIndex];
    }
    public bool IsFieldFullySown()
    {
        return tilesSown == cropTiles.Count;
    }
    public bool IsFieldFullyWatered()
    {
        return tilesWatered == tilesSown;
    }
    public bool IsFieldFullyHarvested()
    {
        return tilesHarvested == tilesSown;
    }
}
