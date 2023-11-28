using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private float _waitForManager = 1f;
    [SerializeField] private ManagerDataSourceSO _dataSourceSO;

    private int _heartsCount = 3;
    [SerializeField] private UiHearts _heart;
    [SerializeField] private HealthPoints _healthPoints;
    private List<UiHearts> _heartList = new List<UiHearts>();
    private Vector3 _spawnPoint;

    private void OnEnable()
    {
        _healthPoints.changeLife += ShowActualLife;
        for (int i = 0; i < _heartList.Count; i++)
        {
            _heartList[i].ChangeLifeSprite(0);
        }
    }

    private void OnDisable()
    {
        _healthPoints.changeLife -= ShowActualLife;
    }

    private void Awake()
    {
        _spawnPoint = gameObject.transform.position;
        if (!_healthPoints)
        {
            Debug.LogError(message: $"{name}: HealthPoints is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
    }

    private void Start()
    {
        _heartsCount = _healthPoints.maxLife / 2;
        InstanciateHearts();
        StartCoroutine(SetManager());
    }

    private IEnumerator SetManager()
    {
        yield return new WaitForSeconds(_waitForManager);
        if (_dataSourceSO.gameManager)
        {
            _dataSourceSO.gameManager.resetGame += ResetLife;
        }
    }

    private void ResetLife()
    {
        for (int i = 0; i < _heartList.Count; i++)
        {
            if (i < _heartsCount)
            {
                _heartList[i].ChangeLifeSprite(0);
            }
            else
            {
                _heartList[i].gameObject.SetActive(false);
            }
        }
    }

    private void InstanciateHearts()
    {
        for (int i = 0; i < _heartsCount; i++)
        {
            UiHearts temp = Instantiate(_heart, transform.position, Quaternion.identity);
            temp.transform.parent = transform;
            _heartList.Add(temp);
            _spawnPoint.x++;
        }
    }

    private void ShowActualLife(int life)
    {
        if (life == 0)
        {
            _heartList[0].ChangeLifeSprite(2);
            return;
        }
        else if (life == 1)
        {
            _heartList[0].ChangeLifeSprite(1);
            return;
        }
        if (life % 2 == 0)
        {
            int index = life / 2;
            index--;
            if (index == _heartList.Count)
            {
                _heartList[index].ChangeLifeSprite(0);
            }
            else
            {
                _heartList[index].ChangeLifeSprite(0);
                _heartList[index + 1].ChangeLifeSprite(2);
            }
        }
        else
        {
            int index = life / 2;
            index--;
            _heartList[index].ChangeLifeSprite(0);
            _heartList[index + 1].ChangeLifeSprite(1);
        }
    }
}
