using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CropTile : StateMachine
{
    private MeshRenderer _meshRenderer;

    private bool _isEmpty;
    private bool _isSown ;
    private bool _isWatered;

    public bool IsEmpty {get{ return _isEmpty;} set{_isEmpty = value;}}
    public bool IsSown { get { return _isSown; } set { _isSown = value; } }
    public bool IsWatered { get { return _isWatered; } set { _isWatered = value; } }

    public MeshRenderer TileRenderer => _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        Debug.Log(_meshRenderer);
    }
    private void Start()
    {
        SwitchState(new CropFieldEmptyState(this));
    }
    private void Update()
    {
        _currentState.TickState();
    }

    public void StartC()
    {
        StartCoroutine(StartCountdown());
    }
    private IEnumerator StartCountdown()
    {
        int currentTime = 10;

        while (currentTime > 0)
        {
            Debug.Log(currentTime);
            yield return new WaitForSeconds(1f); // Her saniye bekler
            currentTime--;
        }

        Debug.Log("Geri sayım tamamlandı!");
    }

}
 