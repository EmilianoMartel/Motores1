using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DropController : MonoBehaviour
{
    private HealthPoints _healthPoints;
    private LevelManager _levelManager;

    [SerializeField] private ManagerDataSourceSO _managerDataSourceSO;
    [SerializeField] private List<DropSO> _dropList;
    [SerializeField] private Transform _spawnPoint;
    private int _dropRate;
    private int _dropChance;

    public List<DropSO> dropList {  get { return _dropList; } }

    private void Awake()
    {
        if (!_managerDataSourceSO)
        {
            Debug.LogError(message: $"{name}: DataSource is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        if (_dropList == null)
        {
            Debug.LogError(message: $"{name}: Drop SO is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        if (_spawnPoint == null)
        {
            Debug.LogError(message: $"{name}: SpawnPoint is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
    }

    private void Start()
    {
        for (int i = 0; i < _dropList.Count; i++)
        {
            if (_managerDataSourceSO.dropManager)
            {
                _managerDataSourceSO.dropManager.CreatePool(_dropList[i].dropObject.name, _dropList[i].dropObject);
            }
        }
    }

    [ContextMenu("Active Drop")]
    private void DropItem()
    {
        Debug.Log($"{name}: try to drop objects.");
        _dropChance = UnityEngine.Random.Range(0,100);
        for (int i = 0; i < _dropList.Count; i++)
        {
            if (_dropChance <= _dropList[i].dropRate)
            {
                if (_managerDataSourceSO.dropManager)
                {
                    _managerDataSourceSO.dropManager.SpawnDropObject(_dropList[i].dropObject.name, _dropList[i].dropObject, _spawnPoint.position);
                }
            }
        }
    }

    public void DelegateSuscriptionDrop(HealthPoints hp)
    {
        _healthPoints = hp;
        _healthPoints.dead += DropItem;
    }

    public void DelegateSuscriptionDrop(LevelManager lv)
    {
        _levelManager = lv;
        _levelManager.endLevel += DropItem;
        _levelManager.endBossFight += DropItem;
    }

    private void OnDisable()
    {
        if (_healthPoints != null)
        {
            _healthPoints.dead -= DropItem;
        }
        if( _levelManager != null)
        {
            _levelManager.endLevel -= DropItem;
        }
    }
}