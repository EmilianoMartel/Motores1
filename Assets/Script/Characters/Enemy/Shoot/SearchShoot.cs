using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using static UnityEngine.UI.Image;

public class SearchShoot : EnemyShoot
{
    [SerializeField] private LayerMask _collisionLayer;
    [SerializeField] private float _rayLength = 1.0f;
    private bool _isHitting = false;
    private Vector2 _direction;

    private void Start()
    {
        isObserver?.Invoke(true);
    }

    private void Update()
    {
        if (!_isHitting)
        {
            SearchPlayer(Vector2.up);
            SearchPlayer(Vector2.down);
            SearchPlayer(Vector2.left);
            SearchPlayer(Vector2.right);
        }
        else
        {
            SearchPlayer(_direction);
        }

    }

    private void SearchPlayer(Vector2 direction)
    {
        Vector2 origin = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, _rayLength, _collisionLayer);

        if (hit.collider != null)
        {
            Debug.Log($"{name}: detected {hit.collider.gameObject.name}");
            _direction = direction;
            _isHitting = true;
        }
        else
        {
            Debug.Log($"{name}: not detected");
            _isHitting = false;
        }
    }
}
