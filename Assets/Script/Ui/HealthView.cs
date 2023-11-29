using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private float _waitForManager = 1f;
    [SerializeField] private ManagerDataSourceSO _dataSourceSO;

    private int _heartsCount = 7;
    [SerializeField] private UiHearts _heart;
    [SerializeField] private HealthPoints _healthPoints;
    private List<UiHearts> _heartList = new List<UiHearts>();

    //Managers
    private GameManager _gameManager;

    private void OnEnable()
    {
        _healthPoints.changeLife += ShowActualLife;
        _healthPoints.ChangeFullLifeEvent += ChangeMaxLife;
        for (int i = 0; i < _heartList.Count; i++)
        {
            _heartList[i].ChangeLifeSprite(0);
        }
        if (_gameManager)
        {
            _gameManager.resetGame += ResetLife;
        }
    }

    private void OnDisable()
    {
        _healthPoints.changeLife -= ShowActualLife;
        _healthPoints.ChangeFullLifeEvent -= ChangeMaxLife;
        if (_gameManager)
        {
            _gameManager.resetGame -= ResetLife;
        }
    }

    private void Awake()
    {
        if (!_healthPoints)
        {
            Debug.LogError(message: $"{name}: HealthPoints is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        if(!_dataSourceSO)
        {
            Debug.LogError(message: $"{name}: DataSource is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
    }

    private void Start()
    {
        InstanciateHearts();
        StartCoroutine(SetManager());
    }

    private IEnumerator SetManager()
    {
        yield return new WaitForSeconds(_waitForManager);
        if (_dataSourceSO.gameManager && !_gameManager)
        {
            _gameManager = _dataSourceSO.gameManager;
            _gameManager.resetGame += ResetLife;
        }
    }

    private void ResetLife()
    {
        StartCoroutine(WaitForHeartReset());
    }

    private IEnumerator WaitForHeartReset()
    {
        yield return new WaitForSeconds(_waitForManager);
        _heartsCount = _healthPoints.maxLife / 2;
        for (int i = 0; i < _heartList.Count; i++)
        {
            if (i < _heartsCount)
            {
                _heartList[i].ChangeLifeSprite(0);
                _heartList[i].gameObject.SetActive(true);
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
            if (i >= _healthPoints.maxLife / 2)
            {
                temp.gameObject.SetActive(false);
            }
        }
    }

    private void ShowActualLife(int life)
    {
        float index = (float)life / 2;
        index--;
        for (int i = 0; i < _heartList.Count; i++)
        {
            if (i <= index)
            {
                _heartList[i].ChangeLifeSprite(0);
            }
            else if (i > index && index > i - 1)
            {
                _heartList[i].ChangeLifeSprite(1);
            }
            else if (_heartList[i].gameObject.activeSelf)
            {
                _heartList[i].ChangeLifeSprite(2);
            }
        }
    }

    private void ChangeMaxLife(int maxIndexLife)
    {
        if (_heartList.Count - 1 < maxIndexLife)
        {
            int newHearts = maxIndexLife - (_heartList.Count - 1);
            for (int i = 0; i < newHearts; i++)
            {
                UiHearts temp = Instantiate(_heart, transform.position, Quaternion.identity);
                temp.transform.parent = transform;
                _heartList.Add(temp);
            }
        }
        for (int i = 0; i < _heartList.Count; i++)
        {
            if (i < maxIndexLife)
            {
                _heartList[i].gameObject.SetActive(true);
            }
            else
            {
                _heartList[i].gameObject.SetActive(false);
            }
        }
    }
}
