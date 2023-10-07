using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWhithValue : EnemyShoot
{
    [SerializeField] private SearchLogic _searchLogic;
    private Vector2 _valueDirection;

    private void Start()
    {
        _searchLogic.getDirection += GetShootDirection;
        _searchLogic.foundPlayer += CanAttack;
    }

    private void GetShootDirection(Vector2 direction)
    {
        _valueDirection = direction;
    }

    protected override Vector2 GetDirection()
    {
        return _valueDirection;
    }

    private void CanAttack(bool found)
    {

    }
}
