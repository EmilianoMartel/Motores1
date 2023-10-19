using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.U2D.Animation;
using UnityEngine;
using static UnityEngine.UI.Image;

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

    private void FixedUpdate()
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

        if (hit.collider != null)
        {
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
        Vector2 up = transform.position + (Vector3.up * _rayLength);
        Vector2 right = transform.position + (Vector3.right * _rayLength);
        Vector2 left = transform.position + (Vector3.left * _rayLength);
        Vector2 down = transform.position + (Vector3.down * _rayLength);

        Gizmos.DrawLine(start, up);
        Gizmos.DrawLine(start, right);
        Gizmos.DrawLine(start, left);
        Gizmos.DrawLine(start, down);
    }
}
