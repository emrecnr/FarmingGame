using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropField : MonoBehaviour
{
    [Header("---- CROP DATA ----")]
    [SerializeField] private CropData cropData;

    [Header("---- REFERENCES ----")]
    [SerializeField] private Transform cropTilesParent;
    [SerializeField] private List<CropTile> cropTiles = new List<CropTile>();

    [Header("--- ACTIONS ----")]
    public static Action<CropField> OnFullySown;
    public static Action<CropField> OnFullyWatered;
    
    private int tilesSown;
    private int tilesWatered;
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

    private void Sow(CropTile cropTile)
    {
        cropTile.SwitchState(new CropTileSownState(cropData,cropSpawner,cropTile));
        tilesSown++;
        if(IsFieldFullySown()) FieldFullySown(); 
    }
    private void Water(CropTile cropTile)
    {
        cropTile.SwitchState(new CropFieldWateredState(cropTile));
        tilesWatered++;
        if(IsFieldFullyWatered()) FieldFullWatered();
    }

    private void FieldFullySown()
    {
        Debug.Log("Field fully Sown");
        OnFullySown?.Invoke(this);
    }
    private void FieldFullWatered()
    {
        Debug.Log("Field fully Watered");
        OnFullyWatered?.Invoke(this);
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
        return tilesWatered == cropTiles.Count;
    }
}
