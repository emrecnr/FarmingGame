using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crop Data",menuName ="Scriptable Object/Crop Data",order =0)]
public class CropData : ScriptableObject
{
    [Header("--- SETTINGS ---")]
    public Crop crop;
    public Crop cropFull;
}
