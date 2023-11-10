using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DropController : MonoBehaviour
{
    private HealthPoints _healthPoints;
    private LevelManager _levelManager;

    [SerializeField] private List<DropSO> _dropList;
    private int _dropRate;
    private int _dropChance;

    private void Awake()
    {
        if (_dropList == null)
        {
            Debug.LogError(message: $"{name}: Drop SO is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
    }

    private void DropOnDead()
    {
        _dropChance = UnityEngine.Random.Range(0,100);
        for (int i = 0; i < _dropList.Count; i++)
        {
            if (_dropChance <= _dropList[i].dropRate)
            {
                Instantiate(_dropList[i].dropObject);
            }
        }
    }

    public void DelegateSuscriptionDrop(HealthPoints hp)
    {
        _healthPoints = hp;
        _healthPoints.dead += DropOnDead;
    }

    public void DelegateSuscritpionDrop(LevelManager lv)
    {
        _levelManager = lv;
        _levelManager.endLevel += DropOnDead;
        _levelManager.endBossFight += DropOnDead;
    }

    private void OnDisable()
    {
        if (_healthPoints != null)
        {
            _healthPoints.dead -= DropOnDead;
        }
        if( _levelManager != null)
        {
            _levelManager.endLevel -= DropOnDead;
        }
    }
}