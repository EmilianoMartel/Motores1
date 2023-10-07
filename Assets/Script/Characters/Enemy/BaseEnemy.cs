using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public delegate void EnemyKill(BaseEnemy enemy);
public class BaseEnemy : Character
{
    public EnemyKill enemyKill;

    public bool activateEnemy = true;

    //Constructors
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private EnemyShoot _enemyShoot;
    private SearchLogic _searchLogic;
    private VulnerableStateController _vulnerabilityController;

    //EnemyType State
    [SerializeField] private bool _isObserverShootEnemy = false;
    [SerializeField] private bool _isObserverMoveEnemy = false;
    [SerializeField] private bool _isVulnerableEnemyIfSee = false;

    //Shooting
    [SerializeField] private float _maxTimeShoot = 5.0f;
    private float _timeShoot;
    private bool _canAttack = true;
    public bool canAttack {  get { return _canAttack; }  }
    
    void Awake()
    {
        TimeShootSelection();
        p_actualTime = 0;
        if (_enemyShoot != null)
        {
            p_characterView.endAttack += EndAttack;
        }
        if (_isObserverShootEnemy)
        {
            _canAttack = false;
            _searchLogic = gameObject.GetComponent<SearchLogic>();
            _searchLogic.foundPlayer += CanAttackChanger;
        }
        if (_isVulnerableEnemyIfSee)
        {
            _vulnerabilityController = gameObject.GetComponent<VulnerableStateController>();
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
            if (_canAttack)
            {
                if (_timeShoot < p_actualTime && !p_isAttacking)
                {
                    p_actualTime = 0;
                    StartAttack();
                    _enemyShoot.Shoot();
                }
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

    private void CanAttackChanger(bool canAttack)
    {
        _canAttack = canAttack;
        if (_isVulnerableEnemyIfSee)
        {
            _vulnerabilityController.isVulnerable?.Invoke(_canAttack);
        }
    }
}
