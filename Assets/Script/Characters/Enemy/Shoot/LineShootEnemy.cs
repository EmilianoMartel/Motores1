using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineShootEnemy : EnemyShoot
{
    private int _randomDirection;

    protected override Vector2 GetDirection()
    {
        ChooseDirection();
        Vector2 direction = new Vector2(p_directionX, p_directionY);
        p_directionY = 0;
        p_directionX = 0;
        return direction;
    }

    private void ChooseDirection()
    {
        _randomDirection = Random.Range(0, 2);
        switch (_randomDirection)
        {
            case 0:
                p_directionY = RandomDirection();
                break;
            case 1:
                p_directionX = RandomDirection();
                break;
        }
    }

    private int RandomDirection()
    {
        int direction = Random.Range(0, 2); ;
        switch (direction)
        {
            case 0:
                direction = 1;
                break;
            case 1:
                direction = -1;
                break;
        }
        return direction;
    }
}
