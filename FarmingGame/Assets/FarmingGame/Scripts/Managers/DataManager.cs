using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance {get; private set;}

    [Header("----- Data -----")]
    [SerializeField] private CropData[] cropDatas;

    private void Awake() {
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

    public Sprite GetCropIconFropCropType(CropType cropType)
    {
        for (int i = 0; i < cropDatas.Length; i++)
        {
            if(cropDatas[i].cropType == cropType)
                                    return cropDatas[i].icon;
        }

        Debug.LogError("No cropData found of that type");
        return null;
    }

    public int GetCropPriceFromCropType(CropType cropType)
    {
        for (int i = 0; i < cropDatas.Length; i++)
        {
            if (cropDatas[i].cropType == cropType)
                return cropDatas[i].price;
        }

        return 0;
    }
}
