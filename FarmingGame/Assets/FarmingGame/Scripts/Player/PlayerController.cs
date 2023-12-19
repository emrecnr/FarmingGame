using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("--- References ---")]
    [SerializeField] private Joystick _joystick;
    private Animator _animator;

    private CharacterController _characterController;
    private PlayerAnimator _playerAnimator;

    [Header("--- Settings ---")]
    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _playerAnimator = new PlayerAnimator(_animator);

    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 moveDirection = _joystick.MoveDirection * moveSpeed * Time.deltaTime / Screen.width;
        moveDirection.z = moveDirection.y;
        moveDirection.y = 0f;

        _characterController.Move(moveDirection);
        _playerAnimator.Action(moveDirection);
    }

    private void NormalizeMoveVector()
    {

    }
}
