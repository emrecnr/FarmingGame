
using UnityEngine;


public class Mover  
{
    private CharacterController _characterController;
    private Vector3 _direction;



    public Vector3 Direction => _direction;

    public Mover(CharacterController characterController)
    {
        _characterController = characterController;
    }

    public void Move(float moveSpeed, Vector3 moveDirection)
    {
        _direction = moveDirection * moveSpeed * Time.deltaTime / Screen.width;
        _direction.z = _direction.y;
        _direction.y = 0f;

        _characterController.Move(_direction);
    }
}
