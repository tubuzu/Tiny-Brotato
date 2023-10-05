// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPlayer : EnemyAbstract
{
    public float distanceLimitToFollowPlayer;
    [SerializeField] protected float distanceLimitToFearPlayer;
    [SerializeField] protected bool fearWhenNearPlayer = false;
    protected bool isFearing = false;

    float distanceToPlayer;

    public void FollowTarget(Vector2 direction)
    {
        distanceToPlayer = direction.magnitude;

        if (fearWhenNearPlayer)
        {
            if (distanceToPlayer > distanceLimitToFearPlayer + 0.25f)
            {
                isFearing = false;
                enemyCtrl.EnemyMovement.Move(direction);
            }
            if (distanceToPlayer <= distanceLimitToFearPlayer + 0.25f && distanceToPlayer > distanceLimitToFearPlayer)
            {
                if (isFearing) enemyCtrl.EnemyMovement.Move(-direction);
                else enemyCtrl.EnemyMovement.Move(Vector2.zero);
            }
            else if (distanceToPlayer <= distanceLimitToFearPlayer)
            {
                enemyCtrl.EnemyMovement.Move(-direction);
                isFearing = true;
            }
        }
        else
        {
            if (distanceToPlayer > distanceLimitToFearPlayer)
            {
                enemyCtrl.EnemyMovement.Move(direction);
            }
            else if (distanceToPlayer <= distanceLimitToFearPlayer)
            {
                enemyCtrl.EnemyMovement.Move(Vector2.zero);
            }
        }
    }
}
