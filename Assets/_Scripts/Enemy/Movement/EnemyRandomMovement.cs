// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomMovement : EnemyAbstract
{
    public float range;
    public Vector3 destination;
    protected float distanceToChangeDestination = 0.5f;

    Vector3 point;

    // bool hehe = true;

    public void RandomMoving()
    {
        if ((destination - transform.position).magnitude <= distanceToChangeDestination)
        {
            if (RandomPoint(transform.position, range, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                destination = point;
            }
        }
        // if (hehe)
        // {
            // StartCoroutine(
                        enemyCtrl.EnemyMovement.Move(destination - transform.position);
                        // enemyCtrl.EnemyMovement.ChangeVelocity(destination - transform.position, 5f));
            // hehe = false;
        // }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        randomPoint.z = 0;
        if (randomPoint.x >= MapManager.Instance.mapBounds.bounds.min.x + 1f && randomPoint.x <= MapManager.Instance.mapBounds.bounds.max.x - 1f && randomPoint.y >= MapManager.Instance.mapBounds.bounds.min.y + 1f && randomPoint.y <= MapManager.Instance.mapBounds.bounds.max.y - 1f)
        {
            result = randomPoint;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
