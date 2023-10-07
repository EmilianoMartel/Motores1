using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public delegate void FloorPosition(Vector2 vector2, int column, int row);
public delegate void StairObject(Stair stair);
public class ViewMapManager : MonoBehaviour
{
    //Delegates
    public FloorPosition floorPosition;
    public StairObject stairObject;

    public int _row; //High
    public int _column; //Width

    //Table
    [SerializeField] private Grid _grid;
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private GameObject _floorPrefab;
    private GameObject _spawnMap;
    [SerializeField] private MapDataSO _mapData;
    [SerializeField] private ArrayLayout data;
    [SerializeField] private List<GameObject> _dataPrefabList;
    private List<ArrayLayout> _dataList;
    private ArrayLayout.State _state;

    //Spawner
    [SerializeField] private Stair _StairPrefab;
    private Stair _stair;

    //FloorList
    [SerializeField] private GameObject _floorListParent;

    //Index
    private int index;

    private void Start()
    {
        NullReferenceControll();
        FirstSpawnData();
        InstantiateStairSpawner();
    }

    private void NullReferenceControll()
    {
        if (_column <= 0 || _row <= 0)
        {
            Debug.LogError(message: $"{name}: The column and row is too small \n Check column and row\nDisabling component");
            enabled = false;
            return;
        }
        if (_wallPrefab == null)
        {
            Debug.LogError(message: $"{name}: WallPrefab is null \n Check the parameter\nDisabling component");
            enabled = false;
            return;
        }
        if (_floorPrefab == null)
        {
            Debug.LogError(message: $"{name}: FloorPrefab is null \n Check the parameter\nDisabling component");
            enabled = false;
            return;
        }
        if (_mapData == null)
        {
            Debug.LogError(message: $"{name}: MapData is null \n Check the parameter\nDisabling component");
            enabled = false;
            return;
        }
        if (_StairPrefab == null)
        {
            Debug.LogError(message: $"{name}: StairPrefab is null \n Check the parameter\nDisabling component");
            enabled = false;
            return;
        }
    }

    private void SpawnWalls(int f_column, int f_row, ArrayLayout.State stateFunction)
    {
        _spawnMap = Instantiate(_wallPrefab, transform.position + _grid.CellToWorld(new Vector3Int(f_column, f_row, 10)), Quaternion.identity);
        _spawnMap.transform.parent = transform;

        SpriteRenderer spriteRenderer = _spawnMap.GetComponent<SpriteRenderer>();
        switch (stateFunction)
        {
            case ArrayLayout.State.WallRight:
                index = Random.Range(0, _mapData.RightWallList.Count);
                spriteRenderer.sprite = _mapData.RightWallList[index];
                break;
            case ArrayLayout.State.WallLeft:
                index = Random.Range(0, _mapData.LeftWallList.Count);
                spriteRenderer.sprite = _mapData.LeftWallList[index];
                break;
            case ArrayLayout.State.WallDown:
                index = Random.Range(0, _mapData.DownWallList.Count);
                spriteRenderer.sprite = _mapData.DownWallList[index];
                break;
            case ArrayLayout.State.WallTop:
                index = Random.Range(0, _mapData.TopWallList.Count);
                spriteRenderer.sprite = _mapData.TopWallList[index];
                break;
            case ArrayLayout.State.WallLeftDown:
                spriteRenderer.sprite = _mapData.LeftDownWallCorner;
                break;
            case ArrayLayout.State.WallRightDown:
                spriteRenderer.sprite = _mapData.RightDownWallCorner;
                break;
            default:
                break;
        }
    }

    private void SpawnFloor(int f_column, int f_row)
    {
        _spawnMap = Instantiate(_floorPrefab, transform.position + _grid.CellToWorld(new Vector3Int(f_column, f_row, 10)), Quaternion.identity);
        SpriteRenderer spriteRenderer = _spawnMap.GetComponent<SpriteRenderer>();
        index = Random.Range(0, _mapData.FloorList.Count);
        spriteRenderer.sprite = _mapData.FloorList[index];
        floorPosition?.Invoke(_spawnMap.transform.position, f_column -1, f_row -1);
        _spawnMap.transform.parent = _floorListParent.transform;
    }

    private void FirstSpawnData()
    {
        for (int f_row = 0; f_row < data.rows.Length; f_row++)
        {
            for (int f_column = 0; f_column < data.rows[f_row].row.Length; f_column++)
            {
                //Debug.Log($"Fila {i}, Columna {f}: {state}");
                _state = data.rows[f_row].row[f_column];
                switch (_state)
                {
                    case ArrayLayout.State.EmptyFloor:
                        SpawnFloor(f_column, f_row);
                        break;
                    case ArrayLayout.State.Rock:
                        SpawnFloor(f_column, f_row);
                        break;
                    case ArrayLayout.State.Spawner:
                        SpawnFloor(f_column, f_row);
                        break;
                    case ArrayLayout.State.ObjectSpawner:
                        SpawnFloor(f_column, f_row);
                        break;
                    case ArrayLayout.State.WallRight:
                        SpawnWalls(f_column, f_row, _state);
                        break;
                    case ArrayLayout.State.WallLeft:
                        SpawnWalls(f_column, f_row, _state);
                        break;
                    case ArrayLayout.State.WallDown:
                        SpawnWalls(f_column, f_row, _state);
                        break;
                    case ArrayLayout.State.WallTop:
                        SpawnWalls(f_column, f_row, _state);
                        break;
                    case ArrayLayout.State.WallLeftDown:
                        SpawnWalls(f_column, f_row, _state);
                        break;
                    case ArrayLayout.State.WallRightDown:
                        SpawnWalls(f_column, f_row, _state);
                        break;
                }
            }
        }
    }

    public void InstantiateStairSpawner()
    {
        _stair = Instantiate(_StairPrefab, transform.position + _grid.CellToWorld(new Vector3Int(5, 5, 10)), Quaternion.identity);
        _stair.transform.parent = transform;
        stairObject?.Invoke(_stair);
    }
}
