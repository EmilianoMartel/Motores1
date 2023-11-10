using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    [SerializeField] private Player _player;

    public void SetMoveValue(InputAction.CallbackContext inputContext)
    {
        _player.direction = inputContext.ReadValue<Vector2>();
    }

    public void SetShootValue(InputAction.CallbackContext inputContext)
    {
        _player.inputAttack = inputContext.ReadValue<Vector2>();
    }
}
