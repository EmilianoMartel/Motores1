using System.Collections.Generic;
using UnityEngine;

public delegate void PlayerFound(bool foundPlayer);
public delegate void GetDirection(Vector2 direction);
public class SearchLogic : MonoBehaviour
{
    //Delegates
    public PlayerFound foundPlayer;
    public GetDirection getDirection;

    [SerializeField] private LayerMask _collisionLayer;
    [SerializeField] private float _rayLength = 1.0f;
    private bool _isHitting = false;
    private Vector2 _direction;
    [SerializeField] private List<Vector2> _listDirection = new List<Vector2>() { Vector2.up, Vector2.down , Vector2.left , Vector2.right };
    private int _index = 0;

    //TODO: TP2 - Fix - Could be in Update (DONE)
    private void Update()
    {
        if (!_isHitting)
        {
            SearchPlayer(_listDirection[_index]);
            _index++;
            if (_index == _listDirection.Count)
            {
                _index = 0;
            }
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

        if (hit.collider)
        {
            Debug.Log($"{name}: raycast hit with {hit.collider.gameObject.name}");
            foundPlayer?.Invoke(true);
            getDirection?.Invoke(direction);
            _direction = direction;
            _isHitting = true;
        }
        else
        {
            _isHitting = false;
            foundPlayer?.Invoke(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 start = transform.position;

        for (int i = 0; i < _listDirection.Count; i++)
        {
            Gizmos.DrawLine(start, _listDirection[i]);
        }
    }
}
