using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public delegate void EnemyKill(BaseEnemy enemy);
public class BaseEnemy : Character
{
    public EnemyKill enemyKill;

    public bool activateEnemy = true;

    //Managers
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private EnemyShoot _enemyShoot;

    //Shooting
    [SerializeField] private float _maxTimeShoot = 5.0f;
    private float _timeShoot;
    private bool _isObserver = false;
    
    void Awake()
    {
        TimeShootSelection();
        p_actualTime = 0;
        if (_enemyShoot != null)
        {
            p_characterView.endAttack += EndAttack;
            _enemyShoot.isObserver += ObserverStateEnemy;
        }
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
            if (!_isObserver)
            {
                if (_timeShoot < p_actualTime && !p_isAttacking)
                {
                    p_actualTime = 0;
                    StartAttack();
                    _enemyShoot.Shoot();
                }
            }
            else
            {

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

    public float ReturnSpeed()
    {
        return p_speed;
    }

    public void GetEnemyManager(EnemyManager enemyManager)
    {
        _enemyMovement.GetValueEnemyManager(enemyManager);
    }

    public void GetBulletManager(BulletManager bulletManager)
    {
        _enemyShoot.GetBulletManager(bulletManager);
    }

    private void ObserverStateEnemy(bool isObserver)
    {
        _isObserver = isObserver;
    }
}
