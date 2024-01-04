using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropInteraction : MonoBehaviour
{
    private const string _cropTag = "CropField";
    private bool _isInteractingWithCropField;
    private CropField _currentCrop;

    public bool IsInteractingWithCropField => _isInteractingWithCropField;
    public CropField CurrentCrop => _currentCrop;

    public event Action<CropField> OnTriggerCrop;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_cropTag))
        {
            _isInteractingWithCropField = true;
             _currentCrop = other.GetComponent<CropField>();
             OnTriggerCrop?.Invoke(_currentCrop);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(_cropTag))// && other.GetComponent<CropField>().IsEmpty())
        {
            _isInteractingWithCropField = false;            
            _currentCrop = null;
        }
    }
}
