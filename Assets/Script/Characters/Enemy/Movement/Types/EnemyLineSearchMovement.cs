using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLineSearchMovement : EnemyLineMovement
{
    [SerializeField] private SearchLogic _searchLogic;
    [SerializeField] private float _distanceToPlayerTeshold = 1.5f;
    private bool _canFreeMovement = true;

    private void OnEnable()
    {
        _searchLogic.foundPlayer += MoveToPlayer;
        _searchLogic.getDirection += DirectionToPlayer;
    }

    private void OnDisable()
    {
        _searchLogic.foundPlayer -= MoveToPlayer;
        _searchLogic.getDirection -= DirectionToPlayer;
    }

    private void Awake()
    {
        if (!_searchLogic)
        {
            Debug.LogError($"{name}: Search Logic is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
    }

    protected override void PatrolMovement()
    {
        if (_canFreeMovement)
        {
            base.PatrolMovement();
        }
        else
        {
            if (p_directionToNextPosition.magnitude <= _distanceToPlayerTeshold)
            {
                _canFreeMovement = true;
                return;
            }
            DirectionAssigned(p_directionToNextPosition);
        }
    }

    private void MoveToPlayer(bool move)
    {
        _canFreeMovement = !move;
    }

    private void DirectionToPlayer(Vector2 direction)
    {
        p_directionToNextPosition = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _canFreeMovement = true;
    }
}
