using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private Transform _pointShoot;
    [SerializeField] private int _minBullets = 5;
    [SerializeField] private Bullet _bulletPrefab;
    private Bullet _bullet;
    private List<Bullet> _bulletList = new List<Bullet>();

    private Vector2 _bulletDirection;
    private Vector2 _realPointShoot;
    private const float DIFF_X = 1.0f;
    private const float DIFF_Y = 1.0f;

    void Start()
    {
        if (_pointShoot == null)
        {
            Debug.LogError(message: $"{name}: PointShoot is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        GenerateBaseList();
    }

    private void GenerateBaseList()
    {
        for (int i = 0; i < _minBullets; i++)
        {
            _bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
            _bulletList.Add(_bullet);
            _bullet.activeBullet = false;
            _bullet.gameObject.SetActive(false);
        }
    }

    public void Shoot(Vector2 direction)
    {
        _bulletDirection = direction;
        ElectionBullet();
        ElectionSpawn();
        _bullet.transform.position = _realPointShoot;
        _bullet.direction = direction;
    }

    private void ElectionBullet()
    {
        for (int i = 0; i < _bulletList.Count; i++)
        {
            if (!_bulletList[i].activeBullet)
            {
                _bullet = _bulletList[i];
                _bullet.gameObject.SetActive(true);
                _bullet.activeBullet = true;
                break;
            }
            if (i == _bulletList.Count - 1 && _bulletList[i].activeBullet)
            {
                _bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
                _bulletList.Add(_bullet);
                _bullet.activeBullet = true;
                break;
            }
        }
    }

    private void ElectionSpawn()
    {
        if (_bulletDirection.x > 0)
        {
            _realPointShoot = _pointShoot.position;
            _realPointShoot += new Vector2(DIFF_X, 0);
        }else if (_bulletDirection.x < 0)
        {
            _realPointShoot = _pointShoot.position;
            _realPointShoot += new Vector2(-DIFF_X, 0);
        }else if (_bulletDirection.y < 1)
        {
            _realPointShoot = _pointShoot.position;
            _realPointShoot += new Vector2(0, -DIFF_Y);
        }
        else
        {
            _realPointShoot = _pointShoot.position;
            _realPointShoot += new Vector2(0, DIFF_Y);
        }
    }
}
