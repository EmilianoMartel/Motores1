using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Bullet : MonoBehaviour
{
    public Vector2 direction;
    public bool activeBullet;
    [SerializeField] private float _speed;
    [SerializeField] private float _deleteTime = 6f;
    [SerializeField] HealthPoints _healthPoints;
    private float _actualTime;
    private Character _character;

    private void Start()
    {
        _healthPoints.death += DisableBullet;
    }

    void Update()
    {
        gameObject.transform.Translate(direction * _speed * Time.deltaTime);
        _actualTime += Time.deltaTime;
        if (_actualTime >= _deleteTime)
        {
            _actualTime = 0;
            DisableBullet();
        }
    }

    private void DisableBullet()
    {
        activeBullet = false;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _healthPoints.death -= DisableBullet;
    }
}
