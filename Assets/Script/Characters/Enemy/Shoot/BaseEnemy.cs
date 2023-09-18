using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public delegate void EnemyKill(BaseEnemy enemy);
public class BaseEnemy : Character
{
    public EnemyKill enemyKill;

    //Managers
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private EnemyShoot _enemyShoot;

    //Shooting
    [SerializeField] private float _maxTimeShoot = 5.0f;
    private float _timeShoot;

    void Awake()
    {
        p_mainCamera = FindAnyObjectByType<Camera>();
        CameraLimit();
        TimeShootSelection();
        p_actualTime = 0;
    }

    private void Update()
    {
        
        if (_enemyMovement != null)
        {
            _enemyMovement.Movement();
        }
        if (_enemyShoot != null)
        {
            p_actualTime += Time.deltaTime;
            if (p_shootTimeRest < p_actualTime)
            {
                p_actualTime = 0;
                _enemyShoot.Shoot();
            }
        }
    }

    protected void TimeShootSelection()
    {
        _timeShoot = Random.Range(p_shootTimeRest, _maxTimeShoot);
    }

    protected override void Kill()
    {
        enemyKill?.Invoke(this);
        base.Kill();
    }

    public float GetSpeed()
    {
        return p_speed;
    }
}
