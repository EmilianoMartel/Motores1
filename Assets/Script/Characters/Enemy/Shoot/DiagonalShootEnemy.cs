using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalShootEnemy : EnemyShoot
{
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
        p_randomY = RandomDirection();
        p_randomX = RandomDirection();
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
