using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector2 _direction;
    [SerializeField] private float _speed = 1f;

    void Update()
    {
        MovementCharacter();
    }

    public void SetMoveValue(InputAction.CallbackContext inputContext)
    {
        _direction = inputContext.ReadValue<Vector2>();
    }

    private void MovementCharacter()
    {
        transform.position += new Vector3(_direction.x, _direction.y, 0) * Time.deltaTime * _speed;
    }
}