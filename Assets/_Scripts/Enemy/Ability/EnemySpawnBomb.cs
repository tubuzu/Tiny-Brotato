using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnBomb : EnemyAbility
{
    [Range(0.25f, 5f)]
    [SerializeField] protected float spawnRadius;
    [Range(1, 30)]
    [SerializeField] protected int numberOfBombs;
    [Range(1, 5)]
    [SerializeField] protected float bombAliveDuration;
    [SerializeField] protected EnumManager.BulletCode bulletCode;
    [SerializeField] protected LayerMask shooterLayer;
    [SerializeField] protected LayerMask enemyLayer;

    WaitForSeconds waitDelayBeforeSpawn = new(.15f);
    WaitForFixedUpdate waitForNextSpawn = new();



    public override IEnumerator ReleaseAttack()
    {
        StartCoroutine(enemyCtrl.EnemyMovement.ChangeVelocity(Vector2.zero));

        Vector3 spawnPosition;
        Vector3 playerPosition = playerDamageReceiver.Collider.bounds.center;
        enemyCtrl.EnemyAnimation.SetCanChangeAnim(true);
        enemyCtrl.EnemyAnimation.ChangeAnimationState(EnemyAnimationState.SpawnBomb.ToString());
        enemyCtrl.EnemyAnimation.SetCanChangeAnim(false);
        yield return waitDelayBeforeSpawn;
        for (int i = 0; i < numberOfBombs; i++)
        {
            yield return waitForNextSpawn;
            // int max = 0;
            // do
            // {
            //     spawnPosition = GetRandomPositionAroundPlayer(playerPosition);
            // } while (IsSpawnPositionTooCloseToPlayer(spawnPosition, playerDamageReceiver.Collider.bounds.center, 0.85f) && max < 100);
            spawnPosition = GetRandomPositionAroundPlayer(playerPosition);
            GameObject bullet = BulletSpawner.Instance.Spawn(bulletCode.ToString(), spawnPosition, Quaternion.identity);
            bullet.GetComponentInChildren<BulletDespawnByTime>().SetAliveTime(bombAliveDuration);
            bullet.GetComponentInChildren<BulletImpact>().Setup(shooterLayer, enemyLayer, damage, 1);
        }

        enemyCtrl.EnemyAnimation.SetCanChangeAnim(true);
    }
    // bool IsSpawnPositionTooCloseToPlayer(Vector3 spawnPosition, Vector3 playerPosition, float minDistance)
    // {
    //     return (playerPosition - spawnPosition).magnitude < minDistance;
    // }
    private Vector3 GetRandomPositionAroundPlayer(Vector3 playerPosition)
    {
        Vector2 randomOffset = Random.insideUnitCircle.normalized * Random.Range(0.8f, spawnRadius);
        Vector3 spawnPosition = playerPosition + (Vector3)randomOffset;
        return spawnPosition;
    }
}
