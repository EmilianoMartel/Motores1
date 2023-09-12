using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Bullet : MonoBehaviour
{
    public Vector2 direction;
    public bool activeBullet;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private float _deleteTime = 6f;
    private float _actualTime;
    private Character _character;


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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            _character = other.gameObject.GetComponent<Character>();
            _character.Damage(_damage);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _character = other.gameObject.GetComponent<Character>();
            _character.Damage(_damage);
        }
        DisableBullet();
    }

    private void DisableBullet()
    {
        activeBullet = false;
        gameObject.SetActive(false);
    }
}
