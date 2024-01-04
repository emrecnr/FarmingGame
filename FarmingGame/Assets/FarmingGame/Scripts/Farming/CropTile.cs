using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CropTile : StateMachine
{
    private MeshRenderer _meshRenderer;

    private Crop _currentCrop;
    private CropData _cropData;


    private bool _isEmpty;
    private bool _isSown ;
    private bool _isWatered;

    public bool IsEmpty {get{ return _isEmpty;} set{_isEmpty = value;}}
    public bool IsSown { get { return _isSown; } set { _isSown = value; } }
    public bool IsWatered { get { return _isWatered; } set { _isWatered = value; } }
    public Crop CurrentCrop { get { return _currentCrop; } set { _currentCrop = value; } }
    public MeshRenderer TileRenderer => _meshRenderer;

    [Header("Events")]
    public static Action<CropType> OnCropHarvested;
    public static Action<float> OnCropHarvestedExperience;


    private void Awake()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }
    private void Start()
    {
        SwitchState(new CropFieldEmptyState(this));
    }
    private void Update()
    {
        _currentState.TickState();
    }


}
 