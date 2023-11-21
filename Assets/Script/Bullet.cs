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

    private void OnEnable()
    {
        //TODO: TP2 - Should be done in OnEnable(DONE)
        _healthPoints.dead += DisableBullet;
        //TODO: TP2 - Could be a coroutine/Invoke(DONE)
        StartCoroutine(LifeTimeBullet());
    }

    private void OnDisable()
    {
        //TODO: TP2 - Should be done in OnDisable(DONE)
        _healthPoints.dead -= DisableBullet;
    }

    private void Start()
    {
        if (_healthPoints == null)
        {
            Debug.LogError(message: $"{name}: HealthPoints is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
    }

    //TODO: TP2 - Syntax - Consistency in access modifiers (private/protected/public/etc)(DONE)
    private void Update()
    {
        transform.Translate(direction * _speed * Time.deltaTime);
        
    }

    private IEnumerator LifeTimeBullet()
    {
        yield return new WaitForSeconds(_deleteTime);
        DisableBullet();
    }

    private void DisableBullet()
    {
        activeBullet = false;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_wallLayerMask & (1 << collision.gameObject.layer)) != 0)
        {
            DisableBullet();
        }
    }
}
