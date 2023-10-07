using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyShoot : MonoBehaviour
{
    [SerializeField] private BulletManager _bulletManager;

    [SerializeField] protected CharacterView p_characterView;
    protected Vector2 p_directionShoot;
    protected int p_randomY = 0;
    protected int p_randomX = 0;

    private void Start()
    {
        if (_bulletManager == null)
        {
            Debug.LogError(message: $"{name}: BulletManager is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        if (p_characterView == null)
        {
            Debug.LogError(message: $"{name}: CharacterView is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        p_characterView.shootMoment += ShootMoment;
    }

    public void Shoot()
    {
        p_directionShoot = GetDirection();
    }

    private void ShootMoment()
    {
        _bulletManager.Shoot(p_directionShoot);
    }

    protected virtual Vector2 GetDirection()
    {
        Vector2 direction = new Vector2(0,0);
        return direction;
    }

    public void GetBulletManager(BulletManager bulletManager)
    {
        _bulletManager = bulletManager;
    }
}
