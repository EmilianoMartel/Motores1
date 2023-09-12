using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFreeMovement : EnemyMovement
{
    private void Update()
    {
        PatrolMovement();
    }

    public void GetRandomPointInMatrix()
    {
        int randomRow = Random.Range(0, p_row);
        int randomColumn = Random.Range(0, p_column);
        if(p_currentRowPosition  != randomRow && p_currentColumnPosition != randomColumn)
        {
            p_nextPosition = _enemyManager._positionMatriz[randomColumn, randomRow];
        }
        p_currentColumnPosition = randomColumn;
        p_currentRowPosition = randomRow;
    }

    protected void PatrolMovement()
    {
        p_currentPosition = transform.position;
        p_directionToNextPosition = p_nextPosition - p_currentPosition;
        p_directionToNextPosition.Normalize();

        p_direction = p_directionToNextPosition;
        DirectionAssigned(p_directionToNextPosition);

        if ((p_currentPosition - p_nextPosition).magnitude <= _treshold)
        {
            GetRandomPointInMatrix();
        }
    }
}
