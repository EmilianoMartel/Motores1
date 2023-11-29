using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public delegate void SpawnedEnemyCount(BaseEnemy enemy);
public class EnemyManager : MonoBehaviour
{
    //Struct
    private struct EnemyType
    {
        public string name;
        public List<BaseEnemy> enemyList;
    }

    //Delegates
    public SpawnedEnemyCount spawnedEnemies;

    //Managers
    [SerializeField] private float _waitForManager = 1f;
    [SerializeField] private ManagerDataSourceSO _dataSource;
    private ViewMapManager _viewMapManager;
    private LevelManager _levelManager;
    [SerializeField] private BulletManager _bulletManager;
    private DropPoolManager _dropPoolManager;

    private int _row = 1;
    private int _column = 1;
    public Vector2[,] _positionMatriz;


    //EnemyList
    [SerializeField] private List<BaseEnemy> _enemyListPrefab;
    [SerializeField] private List<BaseEnemy> _bossListPrefab;
    private List<EnemyType> _enemyTypeList = new List<EnemyType>();
    private List<BaseEnemy> _bossList = new List<BaseEnemy>();
    private EnemyType _enemyType;
    private BaseEnemy _enemy;

    private System.Random _random;

    private void OnEnable()
    {
        foreach (var enemyType in _enemyTypeList)
        {
            foreach (BaseEnemy enemy in enemyType.enemyList)
            {
                if (enemy.activateEnemy)
                {
                    enemy.gameObject.SetActive(false);
                    enemy.activateEnemy = false;
                }
            }
        }
        for (int i = 0; i < _bossList.Count; i++)
        {
            if (_bossList[i].activateEnemy)
            {
                _bossList[i].activateEnemy = false;
                _bossList[i].gameObject.SetActive(false);
            }
        }
        //TODO: TP2 - Fix - foreach (var enemyType in _enemyTypeList) (DONE)
        //TODO: TP2 - Spelling error/Code in spanish/Code in spanglish(DONE)

        //Subscription to delegates

        if (_levelManager)
        {
            _levelManager.spawnEnemy += SpawnEnemyLogic;
            _levelManager.bossFight += SpawnBossLogic;
        }
        if (_viewMapManager)
        {
            _viewMapManager.floorPosition += PositionListSpawner;
        }
    }

    private void OnDisable()
    {
        if (_viewMapManager)
        {
            _viewMapManager.floorPosition -= PositionListSpawner;
        }
        if (_levelManager)
        {
            _levelManager.spawnEnemy -= SpawnEnemyLogic;
            _levelManager.bossFight -= SpawnBossLogic;
        }
        
    }

    private void Awake()
    {
        //TODO: TP2 - Fix - These null checks should all be in awake (you sometimes have them in start/OnEnable) (DONE)
        if (!_dataSource)
        {
            Debug.LogError(message: $"{name}: DataSource is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        _dataSource.enemyManager = this;
        StartCoroutine(SetManager());
    }

    private void Start()
    {
        _row = _dataSource.viewMapManager._row - 2;
        _column = _dataSource.viewMapManager._column - 2;
        _positionMatriz = new Vector2[_column, _row];
        InvokeEnemyList();
    }

    private IEnumerator SetManager()
    {
        yield return new WaitForSeconds(_waitForManager);
        if (_dataSource.viewMapManager && !_viewMapManager)
        {
            _viewMapManager = _dataSource.viewMapManager;
            _viewMapManager.floorPosition += PositionListSpawner;
        }
        if (_dataSource.levelManager && !_levelManager)
        {
            _levelManager = _dataSource.levelManager;
            _levelManager.spawnEnemy += SpawnEnemyLogic;
            _levelManager.bossFight += SpawnBossLogic;
        }
        if (_dataSource.dropManager)
        {
            _dropPoolManager = _dataSource.dropManager;
        }
    }

    private void PositionListSpawner(Vector2 position, int column, int row)
    {
        if (column > _column || row > _row || column < 0 || row < 0)
        {
            Debug.LogError(message: $"{name}: Out of range \n Check column and row\nDisabling component");
            enabled = false;
            return;
        }
        _positionMatriz[column, row] = position;
    }

    private void InvokeEnemyList()
    {
        for (int i = 0; i < _enemyListPrefab.Count; i++)
        {
            _enemy = Instantiate(_enemyListPrefab[i], transform.position, Quaternion.identity);
            _enemy.GetEnemyManager(this);
            _enemy.bulletManager = _bulletManager;
            _enemyType.name = _enemy.name;
            _enemyType.enemyList = new List<BaseEnemy>
            {
                _enemy
            };
            _enemyTypeList.Add(_enemyType);
            _enemy.activateEnemy = false;
            _enemy.gameObject.SetActive(false);
        }
        for (int i = 0; i < _bossListPrefab.Count; i++)
        {
            _enemy = Instantiate(_bossListPrefab[i], transform.position, Quaternion.identity);
            _enemy.transform.position = new Vector3(_positionMatriz[0, 0].x, _positionMatriz[0, 0].y, -2);
            _enemy.row = 0;
            _enemy.col = 0;
            _enemy.bulletManager = _bulletManager;
            _enemy.GetEnemyManager(this);
            _enemy.activateEnemy = false;
            _enemy.gameObject.SetActive(false);
            _bossList.Add(_enemy);
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
            if (!_enemyTypeList[index].enemyList[i].activateEnemy)
            {
                _enemy = _enemyTypeList[index].enemyList[i];
                _enemy.transform.position = new Vector3(_positionMatriz[f_column, f_row].x, _positionMatriz[f_column, f_row].y, -2);
                _enemy.row = f_row;
                _enemy.col = f_column;
                _enemy.activateEnemy = true;
                _enemy.gameObject.SetActive(true);
                spawnedEnemies?.Invoke(_enemy);
                return;
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
            _enemy.bulletManager = _bulletManager;
            _enemyTypeList[index].enemyList.Add(_enemy);
            _enemy.activateEnemy = false;
            _enemy.gameObject.SetActive(false);
        }
    }

    public void SetSeed(int seed)
    {
        _random = new System.Random(seed);
    }

    private void SpawnBossLogic()
    {
        Debug.Log($"{name}: Boss Fight");
        int index = _random.Next(0, _bossList.Count);
        _enemy = _bossList[index];
        _enemy.activateEnemy = true;
        _enemy.gameObject.SetActive(true);
        spawnedEnemies?.Invoke(_enemy);
    }
}
