using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{

    [SerializeField] protected BulletManager p_bulletManager;
    private void Start()
    {
        p_actualTime = 10;
    }

    void Update()
    {
        PlayerMovement();
        if (p_actualTime > p_shootTimeRest && (p_attackDirection.x != 0 || p_attackDirection.y != 0))
        {
            PlayerShoot();
            p_actualTime = 0;
        }
        p_actualTime += Time.deltaTime;
    }

    public void SetMoveValue(InputAction.CallbackContext inputContext)
    {
        p_direction = inputContext.ReadValue<Vector2>();
    }

    public void SetShootValue(InputAction.CallbackContext inputContext)
    {
        p_attackDirection = inputContext.ReadValue<Vector2>();
    }

    private void PlayerMovement()
    {
        Movement(p_direction);
    }

    private void PlayerShoot()
    {
        p_bulletManager.Shoot(p_attackDirection);
    }
}