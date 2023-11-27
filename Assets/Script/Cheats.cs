using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.InputSystem;
using System;
using static UnityEngine.EventSystems.EventTrigger;

public class Cheats : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private EnemyManager _enemyManager;

    [SerializeField] private float _timePress = 0.2f;

    private VulnerableStateController _controller;
    private CharacterSO _characterData;

    //Invulnerability
    private bool _isChanged = false;

    //FireRate
    private bool _isChangedFireRate = false;
    [SerializeField] private float _fireRateModify = 2f;
    private float _originalFireRate;
    private float _modifyFireRate;
    private Action<int> _takeDamageFunction;
    public Action<int> _damagedEvent;

    //Enemies Damage
    private List<BaseEnemy> _enemyList = new List<BaseEnemy>();
    private int _minLife = 100000000;
    private Action<int> _eventDamage;
    private Action<bool> _vulnerableEvent;

    private void OnEnable()
    {
        _enemyManager.spawnedEnemies += MinLifeSelection;
    }

    private void OnDisable()
    {
        _enemyManager.spawnedEnemies -= MinLifeSelection;
    }

    private void Awake()
    {
        if (!_player)
        {
            Debug.LogError(message: $"{name}: Player is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        if (!_enemyManager)
        {
            Debug.LogError(message: $"{name}: EnemyManager is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
    }

    private void Start()
    {
        if (_player.gameObject.TryGetComponent<VulnerableStateController>(out VulnerableStateController component))
        {
            _controller = component;
        }
        _characterData = _player.characterSO;
        _originalFireRate = _characterData.shootTimeRest;
        _modifyFireRate = _originalFireRate / _fireRateModify;
    }

    public void SetInvulneravilityState(InputAction.CallbackContext inputContext)
    {

        if (inputContext.started)
        {
            _isChanged = !_isChanged;
            _controller.isVulnerable.Invoke(_isChanged);
            Debug.Log($"{name}: invulneravility chat is : {_isChanged}");
        }
    }

    public void AllEnemyDamage(InputAction.CallbackContext inputContext)
    {
        if (inputContext.started)
        {
            Debug.Log($"{name}: all enemies damage is active with {_minLife / 2}");
            Damaged();
        }
    }

    public void SetFireRateMultiplier(InputAction.CallbackContext inputContext)
    {
        if (inputContext.started)
        {
            if (_isChangedFireRate)
            {
                Debug.Log($"{name}: FireRate cheat is activate");
                _characterData.shootTimeRest = _originalFireRate;
                _isChangedFireRate = false;
            }
            else
            {
                Debug.Log($"{name}: FireRate cheat is desactivated");
                _characterData.shootTimeRest = _modifyFireRate;
                _isChangedFireRate = true;
            }
        }
    }

    private void MinLifeSelection(BaseEnemy enemy)
    {
        _enemyList.Add(enemy);
        enemy.enemyKill += DeleteEnemyInList;
        if (enemy.gameObject.TryGetComponent<HealthPoints>(out HealthPoints component))
        {
            if (component.maxLife < _minLife)
            {
                _minLife = component.maxLife;
            }
            _eventDamage += component.TakeDamage;
        }
        if (enemy.gameObject.TryGetComponent<VulnerableStateController>(out VulnerableStateController comp))
        {
            _vulnerableEvent += comp.CallInvulnerability;
        }
    }

    private void DeleteEnemyInList(BaseEnemy enemy)
    {
        if (_enemyList.Contains(enemy))
        {
            _enemyList.Remove(enemy);
            enemy.enemyKill -= DeleteEnemyInList;
            if (enemy.gameObject.TryGetComponent<HealthPoints>(out HealthPoints component))
            {
                _eventDamage -= component.TakeDamage;
            }
            if (enemy.gameObject.TryGetComponent<VulnerableStateController>(out VulnerableStateController comp))
            {
                _vulnerableEvent -= comp.CallInvulnerability;
            }
        }
    }

    private void Damaged()
    {
        _vulnerableEvent?.Invoke(true);
        _eventDamage?.Invoke(_minLife / 2);
        _vulnerableEvent?.Invoke(false);
    }
}
