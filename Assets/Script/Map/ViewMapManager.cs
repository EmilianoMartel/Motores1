using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewMapManager : MonoBehaviour
{
    [SerializeField] int _row; //High
    [SerializeField] int _column; //Width
    private GameObject[,] _tableDimention => new GameObject[_row, _column];
    [SerializeField] List<GameObject> _topWallList;
    [SerializeField] List<GameObject> _rightWallList;
    [SerializeField] List<GameObject> _leftWallList;
    [SerializeField] List<GameObject> _downWallList;
    [SerializeField] List<GameObject> _floorList;
    [SerializeField] GameObject _leftDownWallCorner;
    [SerializeField] GameObject _rightDownWallCorner;
    private GameObject _wall;

    private void Awake()
    {
        SpawnTable();
    }

    private void SpawnTable()
    {
        for (int f_row = 0; f_row < _row; f_row++)
        {
            for (int f_column = 0; f_column < _column; f_column++)
            {
                if (f_column == 0 && f_row == 0)
                {
                    _wall = Instantiate(_leftDownWallCorner, transform.position + new Vector3(f_column, f_row, 10), Quaternion.identity);
                    _wall.transform.parent = transform;
                }
                else if (f_column == _column - 1 && f_row == 0)
                {
                    _wall = Instantiate(_rightDownWallCorner, transform.position + new Vector3(f_column, f_row, 10), Quaternion.identity);
                    _wall.transform.parent = transform;
                }
                else if (f_row == 0)
                {
                    _wall = Instantiate(_downWallList[0], transform.position + new Vector3(f_column, f_row, 10), Quaternion.identity);
                    _wall.transform.parent = transform;
                }
                else if (f_column == 0)
                {
                    _wall = Instantiate(_leftWallList[0], transform.position + new Vector3(f_column, f_row, 10), Quaternion.identity);
                    _wall.transform.parent = transform;
                }
                else if (f_column == _column - 1)
                {
                    _wall = Instantiate(_rightWallList[0], transform.position + new Vector3(f_column, f_row, 10), Quaternion.identity);
                    _wall.transform.parent = transform;
                }
                else if(f_row == _row - 1)
                {
                    _wall = Instantiate(_topWallList[0], transform.position + new Vector3(f_column, f_row, 10), Quaternion.identity);
                    _wall.transform.parent = transform;
                }
                else
                {
                    _wall = Instantiate(_floorList[0], transform.position + new Vector3(f_column, f_row, 10), Quaternion.identity);
                    _wall.transform.parent = transform;
                }
            }
        }
    }
}
