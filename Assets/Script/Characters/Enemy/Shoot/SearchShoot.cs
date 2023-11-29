using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchShoot : EnemyShoot
{
    [SerializeField] private SearchLogic _searchLogic;
    private bool _canShoot = false;

    private void OnEnable()
    {
        _searchLogic.getDirection += SetDirectionSearch;
        _searchLogic.foundPlayer += CanShootLogic;
    }

    private void OnDisable()
    {
        _searchLogic.getDirection -= SetDirectionSearch;
        _searchLogic.foundPlayer -= CanShootLogic;
    }

    protected override void Awake()
    {
        base.Awake();
        if (!_searchLogic)
        {
            Debug.LogError(message: $"{name}: Search Logic is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
    }

    public override void Shoot(int numShoot)
    {
        if (_canShoot)
        {
            base.Shoot(numShoot);
        }
    }

    public override void Shoot()
    {
        if (_canShoot)
        {
            base.Shoot();
        }
    }

    protected override Vector2 GetDirection()
    {
        Vector2 dir = new Vector2(p_directionX, p_directionY);
        dir.Normalize();
        return dir;
    }

    private void CanShootLogic(bool canShoot)
    {
        _canShoot = canShoot;
    }

    private void SetDirectionSearch(Vector2 direction)
    {
        p_directionX = (int)direction.x;
        p_directionY = (int)direction.y;
    }
}
