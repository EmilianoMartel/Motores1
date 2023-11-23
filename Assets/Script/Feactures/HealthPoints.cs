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

    [SerializeField] private int _maxLife = 10;
    [SerializeField] private VulnerableStateController _controller;
    private DropController _dropController;
    private int _life;
    private bool _isVulnerable = true;

    public int maxLife { get { return _maxLife; } set { _maxLife = value; } }

    private void OnEnable()
    {
        _life = _maxLife;
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
        _life = _maxLife;
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
        if (_life + health <= _maxLife)
        {
            _life += health;
            changeLife?.Invoke(_life);
            return true;
        }
        return false;
    }

    public void TakeDamage(int damage)
    {
        if (_isVulnerable)
        {
            _life -= damage;
            Debug.Log($"{name} was damaged, life: {_life}");
            changeLife?.Invoke(_life);
            damagedEvent?.Invoke();
            if (_life <= 0)
            {
                Dead();
            }
        }
    }

    [ContextMenu("Take 1 point of damage")]
    private void BasicDamage()
    {
        _life--;
        changeLife?.Invoke(_life);
        damagedEvent?.Invoke();
        if (_life <= 0)
        {
            Dead();
        }
    }

    [ContextMenu("Take total damage")]
    private void TakeTotalDamage()
    {
        TakeDamage(_life);
    }

    private void Dead()
    {
        dead?.Invoke();
        _life = _maxLife;
    }

    private void VulnerableStateChange(bool isVulnerable)
    {
        _isVulnerable = isVulnerable;
    }

    public void IncrementFullLife(int incrementedLife)
    {
        _maxLife += incrementedLife;
    }
}
