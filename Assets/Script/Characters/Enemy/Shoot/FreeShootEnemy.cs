using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeShootEnemy : EnemyShoot
{
    protected override Vector2 GetRandomDirection()
    {
        ChooseDirection();
        Vector2 direction = new Vector2(p_randomX, p_randomY);
        p_randomY = 0;
        p_randomX = 0;
        return direction;
    }

    private void ChooseDirection()
    {
        p_randomY = Random.Range(-2, 3);
        p_randomX = Random.Range(-2, 3);
    }
}
