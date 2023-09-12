using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    [SerializeField] private PlayerShooterManager _shooterManager;

    private void Start()
    {
        p_actualTime = 10;
    }

    void Update()
    {
        PlayerMovement();
        p_actualTime += Time.deltaTime;
    }

    public void SetMoveValue(InputAction.CallbackContext inputContext)
    {
        p_direction = inputContext.ReadValue<Vector2>();
    }

    public void SetShootValue(InputAction.CallbackContext inputContext)
    {
        if (p_actualTime > p_shootTimeRest)
        {
            p_actualTime = 0;
            _shooterManager.GetValueShoot(inputContext.ReadValue<Vector2>());
        }
    }

    private void PlayerMovement()
    {
        MoveInCamera(p_direction);
    }
}