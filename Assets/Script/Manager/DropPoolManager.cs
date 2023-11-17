using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class DropPoolManager : MonoBehaviour
{
    private List<DropController> _controllersList = new List<DropController>();
    private Dictionary<string, List<GameObject>> _dropDictionary = new Dictionary<string, List<GameObject>>();

    [SerializeField] private ManagerDataSourceSO _dataSourceSO;

    [SerializeField] private int _poolCount = 5;

    [SerializeField] private LevelManager _levelManager;

    private void OnEnable()
    {
                
    }

    private void OnDisable()
    {
        
    }

    private void Awake()
    {
        if (_dataSourceSO)
        {
            _dataSourceSO.dropManager = this;
        }
    }

    public void SpawnDropObject(string name, GameObject dropObject, Vector3 spawnPosition)
    {
        int count = 0;
        if (_dropDictionary != null)
        {
            if (_dropDictionary.ContainsKey(name))
            {
                for (int i = 0; i < _dropDictionary[name].Count; i++)
                {
                    if (!_dropDictionary[name][i].activeSelf)
                    {
                        _dropDictionary[name][i].SetActive(true);
                        _dropDictionary[name][i].transform.position = spawnPosition;
                        count++;
                        return;
                    }
                }
                if (count > 0)
                {
                    GameObject temp;
                    temp = Instantiate(dropObject,spawnPosition,Quaternion.identity);
                    _dropDictionary[name].Add(temp);
                }
            }
        }
    }

    public void CreatePool(string name, GameObject dropObject)
    {
        if (_dropDictionary == null)
        {
            _dropDictionary.Add(name, CreateList(dropObject));
        }
        else
        {
            if (_dropDictionary.ContainsKey(name))
            {
                return;
            }
            else
            {
                _dropDictionary.Add(name, CreateList(dropObject));
            }
        }
    }

    private List<GameObject> CreateList(GameObject dropObject)
    {
        List<GameObject> tempList = new List<GameObject>();
        GameObject temp;
        for (int i = 0; i < _poolCount; i++)
        {
            temp = Instantiate(dropObject);
            tempList.Add(temp);
            temp.SetActive(false);
        }
        return tempList;
    }

    public void AddController(DropController drop)
    {
        _controllersList.Add(drop);
    }

    public void DeleteController(DropController drop)
    {
        if (_controllersList.Contains(drop))
        {
            _controllersList.Remove(drop);
        }
    }
}
