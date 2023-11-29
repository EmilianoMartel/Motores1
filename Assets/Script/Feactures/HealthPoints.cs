using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HealthPoints : MonoBehaviour
{
    //Delegates
    public Action<int> changeLife;
    public Action damagedEvent;
    public Action dead;
    public Action<int> ChangeFullLifeEvent;

    [SerializeField] private int _maxLife = 10;
    [SerializeField] private VulnerableStateController _controller;
    private DropController _dropController;
    private int _actualLife;
    private bool _isVulnerable = true;

    [SerializeField] private float _eventCallTime = 0.05f;

    public int maxLife { get { return _maxLife; } set { _maxLife = value; } }

    private void OnEnable()
    {
        _actualLife = _maxLife;
        if (TryGetComponent(out _controller))
        {
            _controller.isVulnerable += VulnerableStateChange;
        }
        if (TryGetComponent(out _dropController))
        {
            _dropController.DelegateSuscriptionDrop(this);
        }
    }

    private void OnDisable()
    {
        if (TryGetComponent(out _controller))
        {
            _controller.isVulnerable -= VulnerableStateChange;
        }
    }

    private void Start()
    {
        _actualLife = _maxLife;
        if (TryGetComponent(out _controller))
        {
            _controller.isVulnerable += VulnerableStateChange;
        }
        if (TryGetComponent(out _dropController))
        {
            _dropController.DelegateSuscriptionDrop(this);
        }
    }

    //TODO: TP2 - Unclear name(DONE)
    public bool Healing(int health)
    {
        if (_actualLife + health <= _maxLife)
        {
            _actualLife += health;
            changeLife?.Invoke(_actualLife);
            return true;
        }
        return false;
    }

    public void TakeDamage(int damage)
    {
        if (_isVulnerable)
        {
            _actualLife -= damage;
            Debug.Log($"{name} was damaged, life: {_actualLife}");
            if (_actualLife <= 0)
            {
                Dead();
                return;
            }
            changeLife?.Invoke(_actualLife);
            damagedEvent?.Invoke();

        }
    }

    [ContextMenu("Take 1 point of damage")]
    private void BasicDamage()
    {
        _actualLife--;
        changeLife?.Invoke(_actualLife);
        damagedEvent?.Invoke();
        if (_actualLife <= 0)
        {
            Dead();
        }
    }

    [ContextMenu("Take total damage")]
    private void TakeTotalDamage()
    {
        TakeDamage(_actualLife);
    }

    private void Dead()
    {
        dead?.Invoke();
        _actualLife = _maxLife;
    }

    private void VulnerableStateChange(bool isVulnerable)
    {
        _isVulnerable = isVulnerable;
    }

    public void ChangeFullLife(int incrementedLife)
    {
        if (_maxLife + incrementedLife <= 0)
        {
            return;
        }
        _maxLife += incrementedLife;
        if (_maxLife < _actualLife)
        {
            _actualLife = _maxLife;
        }
        else
        {
            _actualLife += incrementedLife;
        }
        StartCoroutine(EventCallChangeFullLife());
    }

    public IEnumerator EventCallChangeFullLife()
    {
        ChangeFullLifeEvent?.Invoke(_maxLife / 2);
        yield return new WaitForSeconds(_eventCallTime);
        changeLife?.Invoke(_actualLife);
    }
}
