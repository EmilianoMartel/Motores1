using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    [SerializeField] protected BulletManager p_bulletManager;
    private Vector3 _inputAttack;

    private void Start()
    {
        NullReferenceController();
        SuscriptionsDelegates();
        p_characterView.shootMoment += SetShootDirection;
        p_characterView.endAttack += EndAttack;
        p_actualTime = 10;
    }

    void Update()
    {
        PlayerMovement();
        if (p_actualTime > p_shootTimeRest && (_inputAttack.x != 0 || _inputAttack.y != 0) && p_isAttacking == false)
        {
            p_attackDirection = _inputAttack;
            StartAttack();
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
        _inputAttack = inputContext.ReadValue<Vector2>();
    }

    private void PlayerMovement()
    {
        Movement(p_direction);
    }

    private void SetShootDirection()
    {
        p_bulletManager.Shoot(p_attackDirection);
    }

    protected override void StartAttack()
    {
        p_attackDirection.Normalize();
        base.StartAttack();
    }
}