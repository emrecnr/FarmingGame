using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    [Header("References-----------")]
    [SerializeField] private RectTransform joystickOutline;
    [SerializeField] private RectTransform joystickKnob;

    [Header("Settings------")]
    [SerializeField] private float moveFactor;
    [SerializeField] private bool canMove;

    private Vector3 _clickedPosition;
    private Vector3 _moveDirection;
    public Vector3 MoveDirection => _moveDirection;

    private void Start() {
        HideJoystick();
    }

    private void Update() {
        if(!canMove) return;
        Control();
    }
    public void ClickedJoystickZoneCB()
    {
        _clickedPosition = Input.mousePosition;
        joystickOutline.position = _clickedPosition;

        ShowJoystick();
       
    } 

    private void Control()
    {
        Vector3 currentClickedPosition = Input.mousePosition;
        Vector3 moveDirection = currentClickedPosition - _clickedPosition;

        float moveMagnitude = moveDirection.magnitude * moveFactor / Screen.width;
        moveMagnitude = Mathf.Min(moveMagnitude, joystickOutline.rect.width / 2);

        _moveDirection = moveDirection.normalized * moveMagnitude;
        Vector3 targetPosition = _clickedPosition + _moveDirection;
        joystickKnob.position = targetPosition;

        if(Input.GetMouseButtonUp(0))
        {
            HideJoystick();
        }
    }

    private void ShowJoystick()
    {
            joystickOutline.gameObject.SetActive(true);
        canMove = true;
    }

    private void HideJoystick()
    {
        _moveDirection = Vector3.zero;
        joystickOutline.gameObject.SetActive(false);
        canMove = false;        
    }
}
