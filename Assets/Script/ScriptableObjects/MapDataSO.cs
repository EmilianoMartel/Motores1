using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMap Data", menuName = "NewMap")]
public class MapDataSO : ScriptableObject
{
    [SerializeField] private List<GameObject> _topWallList;
    [SerializeField] private List<GameObject> _rightWallList;
    [SerializeField] private List<GameObject> _leftWallList;
    [SerializeField] private List<GameObject> _downWallList;
    [SerializeField] private List<GameObject> _floorList;
    [SerializeField] private GameObject _leftDownWallCorner;
    [SerializeField] private GameObject _rightDownWallCorner;

    public List<GameObject> TopWallList { get { return _topWallList; } }
    public List<GameObject> RightWallList { get { return _rightWallList; } }
    public List<GameObject> LeftWallList { get { return _leftWallList; } }
    public List<GameObject> DownWallList { get { return _downWallList; } }
    public List<GameObject> FloorList { get { return _floorList; } }
    public GameObject LeftDownWallCorner  { get { return _leftDownWallCorner; } }
    public GameObject RightDownWallCorner { get { return _rightDownWallCorner; } }
}
