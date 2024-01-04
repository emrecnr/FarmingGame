using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class CropSpawner : MonoBehaviour
{
    private Crop _spawnedCrop;
    public Crop SpawnedCrop => _spawnedCrop;

    public void SpawnCrop(CropTile cropTile,GenericPool<Crop> cropPool)
    {


        Crop obj = cropPool.Get();
        obj.transform.position = cropTile.transform.position; // Instantiate(crop,cropTile.transform.position,Quaternion.identity);
        obj.gameObject.SetActive(true);
        _spawnedCrop = obj;        
    }
    public void SpawnSeed(CropTile cropTile)
    {

    }
}
