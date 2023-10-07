using System.Collections;
using System.Collections.Generic;
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
}
