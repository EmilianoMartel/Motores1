using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeShootEnemy : EnemyShoot
{
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
        p_directionY = Random.Range(-2, 3);
        p_directionX = Random.Range(-2, 3);
        if (p_directionX == 0 && p_directionY == 0)
        {
            int direction = Random.Range(0, 2);
            switch (direction)
            {
                case 0:
                    p_directionY++;
                    break;
                case 1:
                    p_directionY--;
                    break;
                default:
                    p_directionX++;
                    break;
            }
        }
    }
}
