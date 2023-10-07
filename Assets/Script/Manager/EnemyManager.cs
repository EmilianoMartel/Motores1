using System;
using System.Collections.Generic;
using UnityEngine;

public delegate void SpawnedEnemyCount(BaseEnemy enemy);
public class EnemyManager : MonoBehaviour
{
    //Struct
    private struct EnemyType{
        public string name;
        public List<BaseEnemy> enemyList;
    }

    //Delegates
    public SpawnedEnemyCount spawnedEnemies;

    //Managers
    [SerializeField] private ViewMapManager _viewMapManager;
    [SerializeField] private LevelManager _levelManager;

    private int _row = 1;
    private int _column = 1;
    public Vector2[,] _positionMatriz;
    

    //EnemyList
    [SerializeField] private List<BaseEnemy> _enemyListPrefab;
    private List<EnemyType> _enemyTypeList = new List<EnemyType>();
    private EnemyType _enemyType;
    private BaseEnemy _enemy;

    private System.Random _random;

    private void Awake()
    {
        if (_viewMapManager == null)
        {
            Debug.LogError(message: $"{name}: ViewManager is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        if (_levelManager == null)
        {
            Debug.LogError(message: $"{name}: LevelManager is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        _row = _viewMapManager._row - 2;
        _column = _viewMapManager._column - 2;
        _positionMatriz = new Vector2[_column, _row];

        //Delegates suscriptions
        _viewMapManager.floorPosition += PositionListSpawner;
        _levelManager.spawnEnemy += SpawnEnemyLogic;
    }

    private void Start()
    {
        InvokeEnemyList();
    }

    private void PositionListSpawner(Vector2 position, int column, int row)
    {
        if (column > _column || row > _row || column < 0 || row < 0)
        {
            Debug.LogError(message:$"{name}: Out of range \n Check column and row\nDisabling component");
            enabled = false;
            return;
        }
        _positionMatriz[column,row] = position;
    }

    private void InvokeEnemyList()
    {
        for (int i = 0; i < _enemyListPrefab.Count; i++)
        {
            _enemy = Instantiate(_enemyListPrefab[i], transform.position, Quaternion.identity);
            _enemy.GetEnemyManager(this);
            _enemyType.name = _enemy.name;
            _enemyType.enemyList = new List<BaseEnemy>();
            _enemyType.enemyList.Add(_enemy);
            _enemyTypeList.Add(_enemyType);
            _enemy.activateEnemy = false;
            _enemy.gameObject.SetActive(false);
        }
    }

    [ContextMenu("SpawnEnemy")]
    private void SpawnEnemyLogic(int f_column = 0, int f_row = 0, int seed = 0)
    {
        SetSeed(seed);
        int index = RandomIndexEnemy();
        CheckEnemyListActive(index);
        for (int i = 0; i < _enemyTypeList[index].enemyList.Count; i++)
        {
            if (_enemyTypeList[index].enemyList[i].activateEnemy == false)
            {
                _enemy = _enemyTypeList[index].enemyList[i];
                _enemy.transform.position = new Vector3(_positionMatriz[f_column,f_row].x , _positionMatriz[f_column, f_row].y, -2);
                _enemy.activateEnemy = true;
                _enemy.gameObject.SetActive(true);
                spawnedEnemies?.Invoke(_enemy);
            }
        }
    }

    private int RandomIndexEnemy()
    {
        return _random.Next(0, _enemyTypeList.Count);
    }

    private void CheckEnemyListActive(int index)
    {
        int count = 0;
        foreach (BaseEnemy enemy in _enemyTypeList[index].enemyList)
        {
            if (enemy.activateEnemy)
            {
                count++;
            }
        }
        if (count == _enemyTypeList[index].enemyList.Count)
        {
            _enemy = Instantiate(_enemyTypeList[index].enemyList[0], transform.position, Quaternion.identity);
            _enemy.GetEnemyManager(this);
            _enemyTypeList[index].enemyList.Add(_enemy);
            _enemy.activateEnemy = false;
            _enemy.gameObject.SetActive(false);
        }
    }

    public void SetSeed(int seed)
    {
        _random = new System.Random(seed);
    }
}