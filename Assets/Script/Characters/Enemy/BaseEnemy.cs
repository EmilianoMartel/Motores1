using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public delegate void EnemyKill(BaseEnemy enemy);
public class BaseEnemy : Character
{
    //Delegates
    public EnemyKill enemyKill;

    public bool activateEnemy = true;

    //Constructors
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private EnemyShoot _enemyShoot;
    private SearchLogic _searchLogic;
    private VulnerableStateController _vulnerabilityController;

    //EnemyType State
    [SerializeField] private bool _isSingleShootEnemy = false;
    [SerializeField] private bool _isMoveEnemy = false;
    [SerializeField] private bool _isObserverShootEnemy = false;
    [SerializeField] private bool _isObserverMoveEnemy = false;
    [SerializeField] private bool _isVulnerableEnemyIfSee = false;
    [SerializeField] private bool _isMultipleShootEnemy = false;

    //Shooting
    [SerializeField] private float _maxTimeShoot = 5.0f;
    private float _timeShoot;
    private bool _canAttack = true;

    //SpawnedPosition Matrix
    private int _row;
    private int _col;
    public int row { get { return _row; } set { _row = value; } }
    public int col { get { return _col; } set { _col = value; } }

    public bool canAttack { get { return _canAttack; } }

    void Awake()
    {
        TimeShootSelection();
        p_actualTime = 0;
    }

    private void Update()
    {
        if (!p_isDead)
        {
            if (_enemyMovement != null)
            {
                p_direction = _enemyMovement.direction;
                Movement(p_direction);
            }
            if (_enemyShoot != null)
            {
                p_actualTime += Time.deltaTime;
                if (_canAttack)
                {
                    if (_timeShoot < p_actualTime && !p_isAttacking)
                    {
                        p_actualTime = 0;
                        if (_isMultipleShootEnemy)
                        {
                            _enemyShoot.Shoot(p_multipleShoot);
                        }
                        else
                        {
                            _enemyShoot.Shoot();
                        }

                    }
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

    public void GetEnemyManager(EnemyManager enemyManager)
    {
        if (_enemyMovement != null)
        {
            _enemyMovement.GetValueEnemyManager(enemyManager);
        }
    }

    private void CanAttackChanger(bool canAttack)
    {
        _canAttack = canAttack;
        if (_isVulnerableEnemyIfSee)
        {
            _vulnerabilityController.isVulnerable?.Invoke(_canAttack);
        }
    }

    private void GetShootDirection(Vector2 directionAttack)
    {
        p_attackDirection = directionAttack;
        StartCoroutine(Shoot());
    }

    private void GetShootDirection(List<Vector2> listDirection)
    {
        StartCoroutine(Shoot(p_multipleShoot,listDirection));
    }

    private void OnEnable()
    {
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
        if (_isSingleShootEnemy)
        {
            if (_enemyShoot == null)
            {
                Debug.LogError(message: $"{name}: EnemyShoot is null\n Check and assigned one\nDisabling component");
                enabled = false;
                return;
            }
            _enemyShoot.startAttack += GetShootDirection;
        }
        if (_isMoveEnemy)
        {
            if (_enemyMovement == null)
            {
                Debug.LogError(message: $"{name}: EnemyMovement is null\n Check and assigned one\nDisabling component");
                enabled = false;
                return;
            }
        }
        if (_isMultipleShootEnemy)
        {
            _enemyShoot.startMultipleAttack += GetShootDirection;
        }
        p_healthPoints.dead += Kill;
    }

    private void OnDisable()
    {
        if (_isObserverShootEnemy)
        {
            _searchLogic.foundPlayer -= CanAttackChanger;
        }
        if (_isSingleShootEnemy)
        {
            _enemyShoot.startAttack -= GetShootDirection;
        }
        if (_isMultipleShootEnemy)
        {
            _enemyShoot.startMultipleAttack -= GetShootDirection;
        }
        p_healthPoints.dead -= Kill;
    }
}