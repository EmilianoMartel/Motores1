using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineShootEnemy : EnemyShoot
{
    private int _randomDirection;

    protected override Vector2 GetDirection()
    {
        ChooseDirection();
        Vector2 direction = new Vector2(p_randomX, p_randomY);
        p_randomY = 0;
        p_randomX = 0;
        return direction;
    }

    private void ChooseDirection()
    {
        _randomDirection = Random.Range(0, 2);
        switch (_randomDirection)
        {
            case 0:
                p_randomY = RandomDirection();
                break;
            case 1:
                p_randomX = RandomDirection();
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
