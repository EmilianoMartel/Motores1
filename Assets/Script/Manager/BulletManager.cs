using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private int _minBullets = 5;
    [SerializeField] private Bullet _bulletPrefab;
    private Bullet _bullet;
    private List<Bullet> _bulletList = new List<Bullet>();

    void Start()
    {
        if(_bulletPrefab == null)
        {
            Debug.LogError(message: $"{name}: BulletPrefab is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        GenerateBaseList();
    }

    private void GenerateBaseList()
    {
        for (int i = 0; i < _minBullets; i++)
        {
            _bullet = Instantiate(_bulletPrefab, transform.position,Quaternion.identity);
            _bulletList.Add(_bullet);
            _bullet.activeBullet = false;
            _bullet.gameObject.SetActive(false);
        }
    }

    public void Shoot(Vector2 direction, Vector2 pointShoot)
    {
        ElectionBullet();
        _bullet.transform.position = pointShoot;
        _bullet.direction = direction.normalized;
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
}
