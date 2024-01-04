using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //public static InventoryManager Instance {get; private set;}

    private Inventory _inventory;
    private InventoryDisplay _inventoryDisplay;
    private string dataPath;

    private void Awake()
    {
        dataPath = Application.dataPath + "/inventoryData.txt";
        LoadInventoty();
        ConfigureInventoryDisplay();

    }

    private void OnEnable()
    {
        CropTile.OnCropHarvested += CropHarvestedHandler;
    }
    private void OnDisable()
    {
        CropTile.OnCropHarvested -= CropHarvestedHandler;
    }

    private void ConfigureInventoryDisplay()
    {
        _inventoryDisplay = GetComponent<InventoryDisplay>();
        _inventoryDisplay.Configure(_inventory);
    }
    private void CropHarvestedHandler(CropType type)
    {
        _inventory.CropHarvestedCallback(type);
        _inventoryDisplay.UpdateDisplay(_inventory);
        SaveInventory();
    }
    public Inventory GetInventory()

    {
        return _inventory;
    }
    public void ClearInventory()
    {
        _inventory.Clear();
        _inventoryDisplay.UpdateDisplay(_inventory);

        SaveInventory();
    }
    private void LoadInventoty()
    {
        string data = "";
        if (File.Exists(dataPath))
        {
            data = File.ReadAllText(dataPath);
            _inventory = JsonUtility.FromJson<Inventory>(data);
            if (_inventory == null)
            {
                _inventory = new Inventory();
            }
        }

        else
        {
            File.Create(dataPath);
            _inventory = new Inventory();
        }

    }
    private void SaveInventory()
    {
        string data = JsonUtility.ToJson(_inventory, true);
        File.WriteAllText(dataPath, data);
    }


}
