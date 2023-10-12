using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Damaged(int actualLife);
public delegate void Death();
public class HealthPoints : MonoBehaviour
{
    //Delegates
    public Damaged damaged;
    public Death death;

    [SerializeField] private int _originalLife = 10;
    [SerializeField] private VulnerableStateController _controller;
    private int _life;
    private bool _isVulnerable = true;

    private void Start()
    {
        _life = _originalLife;
        if (_controller)
        {
            _controller.isVulnerable = VulnerableStateChange;
        }
    }

    public void GetDamage(int damage)
    {
        if (_isVulnerable)
        {
            _life -= damage;
            damaged?.Invoke(_life);
            if (_life <= 0)
            {
                Death();
            }
        }
    }

    private void Death()
    {
        death?.Invoke();
        _life = _originalLife;
    }

    private void VulnerableStateChange(bool isVulnerable)
    {
        _isVulnerable = isVulnerable;
    }
}
