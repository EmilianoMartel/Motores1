using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private int _minBullets = 5;
    [SerializeField] private Bullet _bulletPrefab;
    private Bullet _bullet;
    private List<Bullet> _bulletList = new List<Bullet>();

    void Start()
    {
        GenerateBaseList();
    }

    private void GenerateBaseList()
    {
        for (int i = 0; i < _minBullets; i++)
        {
            _bullet = Instantiate(_bulletPrefab,transform.position,Quaternion.identity);
            _bulletList.Add(_bullet);
            _bullet.activeBullet = false;
            _bullet.gameObject.SetActive(false);
        }
    }

    public void Shoot(Vector3 direction)
    {
        for (int i = 0; i < _bulletList.Count; i++)
        {
            if (!_bulletList[i].activeBullet)
            {
                _bullet = _bulletList[i];
                _bullet.transform.position = transform.position;
                _bullet.gameObject.SetActive(true);
                _bullet.activeBullet = true;
                _bullet.direction = direction;
                break;
            }
            if(i == _bulletList.Count - 1 && _bulletList[i].activeBullet)
            {
                _bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
                _bulletList.Add(_bullet);
                _bullet.transform.parent = transform;
                _bullet.activeBullet = true;
                _bullet.direction = direction;
                break;
            }
        }
    }
}
