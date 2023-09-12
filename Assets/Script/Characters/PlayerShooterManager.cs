using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooterManager : ShooterManager
{
    private Vector2 _direction;

    public void GetValueShoot(Vector2 direction)
    {
        _direction = direction;
        ShootAction();
    }

    [ContextMenu("Shoot")]
    public override void ShootAction()
    {
        p_bulletDirection = _direction;
        p_bulletManagerList[0].Shoot(p_bulletDirection);
    }
}
