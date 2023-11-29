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
    //TODO: TP1 - Unused method/variable
    [SerializeField] private bool _isObserverMoveEnemy = false;
    [SerializeField] private bool _vulnerableEnemyIWhenSeen = false;
    [SerializeField] private bool _isMultipleShootEnemy = false;

    //Move
    private bool _canFreeMove = true;

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

    protected override void OnEnable()
    {
        base.OnEnable();

        if (_isObserverShootEnemy)
        {
            _canAttack = false;
            _searchLogic = gameObject.GetComponent<SearchLogic>();
            _searchLogic.foundPlayer += CanAttackChanger;
        }
        //TODO: TP2 - Unclear name(DONE)
        if (_vulnerableEnemyIWhenSeen)
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
        if (_isObserverMoveEnemy)
        {
            _searchLogic = gameObject.GetComponent<SearchLogic>();
            _searchLogic.foundPlayer += CanMoveToPlayerLogic;
            _searchLogic.getDirection += GetDirectionToSearch;
        }
        if (_isMultipleShootEnemy)
        {
            _enemyShoot.startMultipleAttack += GetShootDirection;
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();

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
        if (_isObserverMoveEnemy)
        {
            _searchLogic.foundPlayer -= CanMoveToPlayerLogic;
            _searchLogic.getDirection -= GetDirectionToSearch;
        }
    }

    //TODO: TP2 - Syntax - Consistency in access modifiers (private/protected/public/etc)(DONE)
    protected override void Awake()
    {
        base.Awake();
        TimeShootSelection();
        p_actualTime = 0;
    }

    private void Update()
    {
        //TODO: TP2 - Fix - Clean code(Done)
        //if(p_isDead)
        //  return;
        if (p_isDead)
        {
            return;
        }
        if (!p_isDead)
        {
            if (_isMoveEnemy && _canFreeMove)
            {
                p_direction = _enemyMovement.direction;
                Movement(p_direction);
            }else if (_isMoveEnemy && !_canFreeMove)
            {
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
        if (_vulnerableEnemyIWhenSeen)
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

    private void CanMoveToPlayerLogic(bool canMove)
    {
        _canFreeMove = !canMove;
    }

    private void GetDirectionToSearch(Vector2 directionToMove)
    {
        p_direction = directionToMove;
    }
}