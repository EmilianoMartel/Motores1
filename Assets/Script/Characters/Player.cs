using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    private Vector3 _inputAttack;
    [SerializeField] private GameManager _gameManger;

    //Damaged
    [SerializeField] private VulnerableStateController _vulnerabilityController;
    [SerializeField] private bool _isDamaged = false;
    [SerializeField] private float _damagedDelay = 1f;

    public Vector3 inputAttack { set {  _inputAttack = value; } }
    public Vector3 direction { set { p_direction = value; } }

    private void OnEnable()
    {
        transform.position = new Vector3(0,0,-5);
        p_healthPoints.dead += Kill;
        p_healthPoints.damagedEvent += IsDamaged;
        _gameManger.resetGame += ActivePlayer;
    }

    private void OnDisable()
    {
        p_healthPoints.dead -= Kill;
        p_healthPoints.damagedEvent -= IsDamaged;
        _gameManger.resetGame -= ActivePlayer;
    }

    private void Start()
    {
        NullReferenceController();
        p_actualTime = 10;
        //TODO: TP2 - Fix - Don't use try-catch blocks where a simple null-check is enough (for performance reasons) (DONE)
        //TODO: TP2 - Should be done in OnEnable (DONE)
        if (_gameManger == null)
        {
            Debug.LogError(message: $"{name}: GameManager is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        if (_vulnerabilityController == null)
        {
            Debug.LogError(message: $"{name}: VulnerabilityController is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
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