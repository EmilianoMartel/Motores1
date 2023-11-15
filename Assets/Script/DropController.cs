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
    [SerializeField] private Transform _spawnPoint;
    private int _dropRate;
    private int _dropChance;

    public List<DropSO> dropList {  get { return _dropList; } }

    private void Awake()
    {
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
            DropPoolManager.INSTANCE.CreatePool(_dropList[i].dropObject.name, _dropList[i].dropObject);
        }
    }

    [ContextMenu("Active Drop")]
    private void DropLogic()
    {
        Debug.Log($"{name}: try to drop objects.");
        _dropChance = UnityEngine.Random.Range(0,100);
        for (int i = 0; i < _dropList.Count; i++)
        {
            if (_dropChance <= _dropList[i].dropRate)
            {
                DropPoolManager.INSTANCE.SpawnDropObject(_dropList[i].dropObject.name, _dropList[i].dropObject, _spawnPoint.position);
            }
        }
    }

    public void DelegateSuscriptionDrop(HealthPoints hp)
    {
        _healthPoints = hp;
        _healthPoints.dead += DropLogic;
    }

    public void DelegateSuscriptionDrop(LevelManager lv)
    {
        _levelManager = lv;
        _levelManager.endLevel += DropLogic;
        _levelManager.endBossFight += DropLogic;
    }

    private void OnDisable()
    {
        if (_healthPoints != null)
        {
            _healthPoints.dead -= DropLogic;
        }
        if( _levelManager != null)
        {
            _levelManager.endLevel -= DropLogic;
        }
    }
}