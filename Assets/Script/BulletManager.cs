using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _pointShootList;
    [SerializeField] private int _minBullets = 5;
    [SerializeField] private Bullet _bulletPrefab;
    private Bullet _bullet;
    private List<Bullet> _bulletList = new List<Bullet>();

    void Start()
    {
        if (_pointShootList.Count == 0)
        {
            Debug.LogError(message: $"{name}: ShootList is null\n Check and assigned one\nDisabling component");
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
        for (int i = 0; i < _bulletList.Count; i++)
        {
            if (!_bulletList[i].activeBullet)
            {
                _bullet = _bulletList[i];
                GetPointShoot(direction);

                _bullet.gameObject.SetActive(true);
                _bullet.activeBullet = true;
                break;
            }
            if (i == _bulletList.Count - 1 && _bulletList[i].activeBullet)
            {
                _bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
                _bulletList.Add(_bullet);
                _bullet.activeBullet = true;
                GetPointShoot(direction);
                break;
            }
        }
    }

    private void GetPointShoot(Vector2 direction)
    {
        if (direction == new Vector2(0, 1))
        {
            _bullet.transform.position = _pointShootList[0].transform.position;
            _bullet.direction = _pointShootList[0].transform.up;
        }
        else if (direction == new Vector2(1, 0))
        {
            _bullet.transform.position = _pointShootList[1].transform.position;
            _bullet.direction = _pointShootList[1].transform.up;
        }
        else if (direction == new Vector2(0, -1))
        {
            _bullet.transform.position = _pointShootList[2].transform.position;
            _bullet.direction = _pointShootList[2].transform.up;
        }
        else if (direction == new Vector2(-1, 0))
        {
            _bullet.transform.position = _pointShootList[3].transform.position;
            _bullet.direction = _pointShootList[3].transform.up;
        }
    }
}
