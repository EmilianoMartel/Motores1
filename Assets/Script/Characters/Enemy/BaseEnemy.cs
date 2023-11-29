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
    [SerializeField] private bool _vulnerableEnemyWhenSeen = false;
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

    protected override void OnEnable()
    {
        base.OnEnable();

        //TODO: TP2 - Unclear name(DONE)
        if (_vulnerableEnemyWhenSeen)
        {
            if (gameObject.TryGetComponent<VulnerableStateController>(out VulnerableStateController output) && gameObject.TryGetComponent<SearchLogic>(out SearchLogic output1))
            {
                _vulnerabilityController = output;
                _searchLogic = output1;
                _searchLogic.foundPlayer += ChangeVulnerability;
                _canAttack = false;
            }
        }

        if (_isSingleShootEnemy)
        {
            _enemyShoot.startAttack += GetShootDirection;
        }

        if (_isMultipleShootEnemy)
        {
            _enemyShoot.startMultipleAttack += GetShootDirection;
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        if (_isSingleShootEnemy)
        {
            _enemyShoot.startAttack -= GetShootDirection;
        }
        if (_isMultipleShootEnemy)
        {
            _enemyShoot.startMultipleAttack -= GetShootDirection;
        }
        if (_vulnerableEnemyWhenSeen)
        {

            _searchLogic.foundPlayer -= ChangeVulnerability;
        }
    }

    //TODO: TP2 - Syntax - Consistency in access modifiers (private/protected/public/etc)(DONE)
    protected override void Awake()
    {
        base.Awake();
        if (_vulnerableEnemyWhenSeen)
        {
            if (!gameObject.TryGetComponent<VulnerableStateController>(out VulnerableStateController output))
            {
                Debug.LogError(message: $"{name}: VulenableStateController is null\n Check and assigned one\nDisabling component");
                enabled = false;
                return;
            }
            if (!gameObject.TryGetComponent<SearchLogic>(out SearchLogic output1))
            {
                Debug.LogError(message: $"{name}: SearchLogic is null\n Check and assigned one\nDisabling component");
                enabled = false;
                return;
            }
        }
        if (_isMoveEnemy && !_enemyMovement)
        {
            Debug.LogError(message: $"{name}: EnemyMovement is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        if (_isSingleShootEnemy && !_enemyShoot)
        {
            Debug.LogError(message: $"{name}: EnemyShoot is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }

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
            if (_isMoveEnemy)
            {
                p_direction = _enemyMovement.direction;
                Movement(p_direction);
            }
            if (_enemyShoot)
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

    private void GetShootDirection(Vector2 directionAttack)
    {
        p_attackDirection = directionAttack;
        StartCoroutine(Shoot());
    }

    private void GetShootDirection(List<Vector2> listDirection)
    {
        StartCoroutine(Shoot(p_multipleShoot, listDirection));
    }

    private void ChangeVulnerability(bool change)
    {
        _vulnerabilityController.isVulnerable?.Invoke(change);
        _canAttack = change;
    }
}