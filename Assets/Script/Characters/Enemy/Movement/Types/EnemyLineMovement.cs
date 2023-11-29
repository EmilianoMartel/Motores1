using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLineMovement : EnemyMovement
{
    void Update()
    {
        PatrolMovement();
    }

    protected override void GetRandomNextPosition()
    {
        int randomSwitch = Random.Range(0, 2);
        int counter = 0;
        int randomRow;
        int randomColumn;

        if (randomSwitch == 0)
        {
            randomRow = Random.Range(0, p_row);
            while (randomRow == p_currentRowPosition && counter != 3){
                randomRow = Random.Range(0, p_row);
                counter++;
            }
            p_nextPosition = _enemyManager._positionMatriz[p_currentColumnPosition, randomRow];
            p_currentRowPosition = randomRow;
        }
        else
        {
            randomColumn = Random.Range(0, p_column);
            while (randomColumn == p_currentColumnPosition && counter != 3)
            {
                randomColumn = Random.Range(0, p_row);
                counter++;
            }
            p_nextPosition = _enemyManager._positionMatriz[randomColumn, p_currentRowPosition];
            p_currentColumnPosition = randomColumn;
        }
        
    }

    protected void PatrolMovement()
    {
        p_currentPosition = transform.position;
        p_directionToNextPosition = p_nextPosition - p_currentPosition;
        p_directionToNextPosition.Normalize();

        DirectionAssigned(p_directionToNextPosition);

        if ((p_currentPosition - p_nextPosition).magnitude <= _treshold)
        {
            GetRandomNextPosition();
        }
    }
}
