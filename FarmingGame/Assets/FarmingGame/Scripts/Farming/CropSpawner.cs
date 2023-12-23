using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropSpawner : MonoBehaviour
{
    //public CropTile cropTile;
    //public CropData cropData;



    public void SpawnCrop(CropTile cropTile,CropData cropData)
    {
        Crop crop = Instantiate(cropData.crop,cropTile.transform.position,Quaternion.identity);
        Debug.Log(crop);
    }
    public void SpawnCropFull(CropData cropData, CropTile cropTile)
    {
        Crop crops = Instantiate(cropData.cropFull,cropTile.transform.position,Quaternion.identity);
    }

}
