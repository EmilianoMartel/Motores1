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
    [SerializeField] private HealthPoints _healthPoints;
    [SerializeField] private LayerMask _wallLayerMask;
    private float _actualTime;

    private void Start()
    {
        if (_healthPoints == null)
        {
            Debug.LogError(message: $"{name}: HealthPoints is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        _healthPoints.death += DisableBullet;
    }

    void Update()
    {
        transform.Translate(direction * _speed * Time.deltaTime);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_wallLayerMask & (1 << collision.gameObject.layer)) != 0)
        {
            DisableBullet();
        }
    }
}
