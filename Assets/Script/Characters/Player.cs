using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    private Vector3 _inputAttack;

    [SerializeField] private ManagerDataSourceSO _dataSourceSO;
    [SerializeField] private float _waitForManagers = 1f;

    //Damaged
    [SerializeField] private VulnerableStateController _vulnerabilityController;
    [SerializeField] private bool _isDamaged = false;
    [SerializeField] private float _damagedDelay = 1f;

    public Vector3 inputAttack { set { _inputAttack = value; } }
    public Vector3 direction { set { p_direction = value; } }
    public CharacterSO characterSO { get { return p_characterData; } }

    protected override void OnEnable()
    {
        base.OnEnable();
        transform.position = new Vector3(0, 0, -5);
        p_healthPoints.damagedEvent += IsDamaged;
        if (_dataSourceSO.gameManager)
        {
            _dataSourceSO.gameManager.resetGame += ActivePlayer;
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        p_healthPoints.damagedEvent -= IsDamaged;
    }

    protected override void Awake()
    {
        base.Awake();
        if (_dataSourceSO == null)
        {
            Debug.LogError(message: $"{name}: DataSource is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        if (_vulnerabilityController == null)
        {
            Debug.LogError(message: $"{name}: VulnerabilityController is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        if (p_characterData.maxLife % 2 != 0)
        {
            Debug.LogError(message: $"{name}: Max life is odd\n It must be even\nDisabling component");
            enabled = false;
            return;
        }
    }

    private void Start()
    {
        p_actualTime = 10;
        //TODO: TP2 - Fix - Don't use try-catch blocks where a simple null-check is enough (for performance reasons) (DONE)
        //TODO: TP2 - Should be done in OnEnable (DONE)

        StartCoroutine(SetManager());
    }

    //TODO: TP2 - Syntax - Consistency in access modifiers (private/protected/public/etc) (DONE)
    private void Update()
    {
        PlayerMovement();
        if (p_actualTime > p_shootTimeRest && (_inputAttack.x != 0 || _inputAttack.y != 0) && p_isAttacking == false)
        {
            p_attackDirection = _inputAttack;
            StartCoroutine(Shoot());
            p_actualTime = 0;
        }
        p_actualTime += Time.deltaTime;
    }

    private IEnumerator SetManager()
    {
        yield return new WaitForSeconds(_waitForManagers);
        if (_dataSourceSO.gameManager)
        {
            _dataSourceSO.gameManager.resetGame += ActivePlayer;
        }
    }

    protected override IEnumerator Dead()
    {
        p_hazard.canHarm = false;
        _vulnerabilityController.isVulnerable.Invoke(false);
        yield return new WaitForSeconds(p_deadDelay);
        _vulnerabilityController.isVulnerable.Invoke(true);
        gameObject.SetActive(false);
        p_isDead = false;
        isDeadEvent?.Invoke(false);
        p_healthPoints.maxLife = p_characterData.maxLife;
    }

    private void PlayerMovement()
    {
        Movement(p_direction);
    }

    private void ActivePlayer()
    {
        gameObject.SetActive(true);
    }

    private void IsDamaged()
    {
        if (!_isDamaged)
        {
            _isDamaged = true;
            StartCoroutine(Damaged());
        }
    }

    private IEnumerator Damaged()
    {
        p_characterView.IsDamaged();
        _vulnerabilityController.isVulnerable.Invoke(false);
        yield return new WaitForSeconds(_damagedDelay);
        _isDamaged = false;
        _vulnerabilityController.isVulnerable.Invoke(true);
    }
}