using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public delegate void SpawnEnemy(int column, int row, int seed);
public delegate void EndLevel();
public delegate void ShowActualWave(int waveNum);
public delegate void BossFight();
public class LevelManager : MonoBehaviour
{
    const int DIFF_MATRIX_ENEMY = 1;

    //Delegates
    public SpawnEnemy spawnEnemy;
    public EndLevel endLevel;
    public ShowActualWave showActualWave;
    public BossFight bossFight;
    public Action endBossFight;

    //Level variables for dificult
    [SerializeField] private int _minEnemy = 1;
    [SerializeField] private float _levelDificult = 1.0f;
    [SerializeField] private List<GameObject> _dataPrefabList;

    //Managers
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private ViewMapManager _viewMapManager;

    [SerializeField] private Stair _stair;

    private List<ArrayLayout> _dataList = new List<ArrayLayout>();
    private ArrayLayout _dataLayout;

    //Variables for start and end game
    private int _enemyKillCount = 0;
    [SerializeField] private int _maxWave = 10;
    private int bossWave;
    private int _actualWave = 0;

    private void Awake()
    {
        bossWave = _maxWave + 2;
        NullReferenceControll();
        SetDataList();

        //Delegate
        _viewMapManager.stairObject += SetStair;
        _enemyManager.spawnedEnemies += SpawnedEnemiesCount;
    }

    private void NullReferenceControll()
    {
        if (_enemyManager == null)
        {
            Debug.LogError(message: $"{name}: Enemy Manager is null \n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        if (_dataPrefabList.Count == 0)
        {
            Debug.LogError(message: $"{name}: DataPrefabList is null \n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        /*if (_stair == null)
        {
            Debug.LogError(message: $"{name}: Stair is null \n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }*/
        if (_viewMapManager == null)
        {
            Debug.LogError(message: $"{name}: ViewMapManager is null \n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        if (_minEnemy < 0)
        {
            Debug.LogError(message: $"{name}: Min enemy is negative \n Check that and assigned positive number\nChange value to 1");
            _minEnemy = 1;
            return;
        }
    }

    private void SetDataList()
    {
        foreach (GameObject data in _dataPrefabList)
        {
            if (data == null)
            {
                Debug.LogError(message: $"{name}: GameObject is null\n Check and assigned one\nDisabling component");
                enabled = false;
                return;
            }
            if (data.gameObject.GetComponent<LevelMapData>() == null)
            {
                Debug.LogError(message: $"{name}: {data.name} dont have a MapData\n Check and assigned one\nDisabling component");
                enabled = false;
                return;
            }
            else
            {
                _dataList.Add(data.gameObject.GetComponent<LevelMapData>().ArrayLayout);
            }
        }
    }

    private void SpawnEnemiesControll(int f_column, int f_row, int seed)
    {
        if (_minEnemy == 1)
        {
            spawnEnemy?.Invoke(f_column - DIFF_MATRIX_ENEMY, f_row - DIFF_MATRIX_ENEMY, seed);
        }
        else
        {
            for (int i = 0; i <= _minEnemy; i++)
            {
                spawnEnemy?.Invoke(f_column - DIFF_MATRIX_ENEMY, f_row - DIFF_MATRIX_ENEMY, seed);
            }
        }
    }

    private void SearchSpawnEnemies()
    {
        ArrayLayout.State state;
        int seed = UnityEngine.Random.Range(0, 100);
        RandomLevel();
        for (int f_row = _dataLayout.rows.Length - 1; f_row >= 0; f_row--)
        {
            for (int f_column = 0; f_column < _dataLayout.rows[f_row].row.Length; f_column++)
            {
                state = _dataLayout.rows[f_row].row[f_column];
                switch (state)
                {
                    case ArrayLayout.State.EmptyFloor:
                        break;
                    case ArrayLayout.State.Rock:
                        break;
                    case ArrayLayout.State.Spawner:
                        SpawnEnemiesControll(f_column, f_row, seed);
                        break;
                    case ArrayLayout.State.ObjectSpawner:
                        break;
                    case ArrayLayout.State.WallRight:
                        break;
                    case ArrayLayout.State.WallLeft:
                        break;
                    case ArrayLayout.State.WallDown:
                        break;
                    case ArrayLayout.State.WallTop:
                        break;
                    case ArrayLayout.State.WallLeftDown:
                        break;
                    case ArrayLayout.State.WallRightDown:
                        break;
                }
            }
        }
    }

    private void RandomLevel()
    {
        int index = UnityEngine.Random.Range(0, _dataList.Count);
        _dataLayout = _dataList[index];
    }

    [ContextMenu("StartLevel")]
    private void StartLevel()
    {
        _actualWave++;
        _stair.isActiveStair = false;
        _stair.gameObject.SetActive(false);
        Debug.Log($"{name}: Event StartLevel is called");
        if (_actualWave == _maxWave+1) SpawnBoss();
        else
        {
            showActualWave?.Invoke(_actualWave);
            SearchSpawnEnemies();
        }
    }

    private void EndLevel()
    {
        endLevel?.Invoke();
        ReSpawnStair();
    }

    private void SpawnedEnemiesCount(BaseEnemy enemy)
    {
        _enemyKillCount++;
        enemy.enemyKill += KilledEnemiesCount;
    }

    private void KilledEnemiesCount(BaseEnemy enemy)
    {
        Debug.Log($"{enemy.name} was killed and call KilledEnemiesCount event.");
        enemy.enemyKill -= KilledEnemiesCount;
        _enemyKillCount--;
        if (_enemyKillCount <= 0)
        {
            Debug.Log("Last enemy killed, start end level moment.");
            _enemyKillCount = 0;
            if (_actualWave == _maxWave + 1) endBossFight?.Invoke();
            else EndLevel();
        }
    }

    private void SetStair(Stair stair)
    {
        _stair = stair;
        _stair.nextLevel += StartLevel;
    }

    private void ReSpawnStair()
    {
        _stair.isActiveStair = true;
        _stair.gameObject.SetActive(true);
    }

    private void SpawnBoss()
    {
        bossFight?.Invoke();
    }
}
