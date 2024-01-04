using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : StateMachine
{
    #region FIELDS
    [Header("--- References ---")]
    [SerializeField] private Joystick _joystick;
    [SerializeField] private CropInteraction cropInteraction;
    [SerializeField] private ToolSelector toolSelector;
    [SerializeField] private GameObject wateringCan;
    [SerializeField] private ParticleSystem waterParticles;
    [SerializeField] private PlayerAnimationEvents playerAnimationEvents;
    [SerializeField] private Transform harvestSphereArea;


    private float _moveSpeed = 50f;
    private CharacterController _characterController;
    private Animator _animator;
    private Mover _mover;
    private PlayerAnimator _playerAnimator;

    #endregion
    # region PROPERTIES
    public float MoveSpeed => _moveSpeed;
    public Mover Mover => _mover;
    public Joystick Joystick =>_joystick;
    public PlayerAnimator PlayerAnimator => _playerAnimator;
    public CropInteraction CropInteract => cropInteraction;
    public GameObject WateringCan => wateringCan;
    public PlayerAnimationEvents PlayerAnimationEvents => playerAnimationEvents;
    public Transform HarvestSphereArea => harvestSphereArea;
    #endregion

    #region METHODS

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _mover = new Mover(_characterController);
        _animator = GetComponentInChildren<Animator>();
        _playerAnimator = new PlayerAnimator(_animator,waterParticles);
        cropInteraction = GetComponent<CropInteraction>();

    }
    private void OnEnable()
    {
        toolSelector.OnSowSelected += OnSowSelectedHandler;
        toolSelector.OnWaterSelected += OnWaterSelectedHandler;
        toolSelector.OnHarvestSelected += OnHarvestSelectedHandler;
        toolSelector.OnNoneSelected += OnNoneSelectedHandler;
    }

    private void OnDisable()
    {
        toolSelector.OnSowSelected -= OnSowSelectedHandler;
        toolSelector.OnWaterSelected -= OnWaterSelectedHandler;
        toolSelector.OnHarvestSelected -= OnHarvestSelectedHandler;
        toolSelector.OnNoneSelected -= OnNoneSelectedHandler;
    }

    private void Start()
    {
        SwitchState(new IdleState(this));
    }

    private void Update()
    {
        _currentState?.TickState();        
    }

    #endregion

    #region Event Handlers

    private void OnHarvestSelectedHandler()
    {
        SwitchState(new PlayerHarvestingState(this));
    }

    private void OnWaterSelectedHandler()
    {
        SwitchState(new PlayerWateringState(this));
    }

    private void OnSowSelectedHandler()
    {
        SwitchState(new PlayerSowingState(this));
    }
    private void OnNoneSelectedHandler()
    {
        SwitchState(new IdleState(this));
    }

    #endregion



}
