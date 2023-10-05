// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementBehaviour : EnemyMovement
{
    [SerializeField] protected Transform player;
    protected EnemyFollowPlayer enemyFollowPlayer;
    protected EnemyRandomMovement enemyRandomMovement;

    protected bool followWhenNearPlayer = false;
    protected bool randomMoving = false;

    protected Vector2 direction;
    protected Vector3 destination;

    protected override void Awake()
    {
        base.Awake();
        if (TryGetComponent(out enemyFollowPlayer)) followWhenNearPlayer = true;
        if (TryGetComponent(out enemyRandomMovement)) randomMoving = true;
    }

    protected override void Start()
    {
        base.Start();
        player = PlayerCtrl.Instance.transform;
    }

    protected virtual void Update()
    {
        if (!GameManager.Instance.WaveInProgress)
        {
            Move(Vector2.zero);
            return;
        }
        else if (enemyCtrl.EnemyDamageReceiver.IsDead()) return;
        if (enemyCtrl.EnemyPerformAbility == null || !enemyCtrl.EnemyPerformAbility.AttackReleasing)
        {
            Vector2 direction = player.position - enemyCtrl.transform.position;
            if (followWhenNearPlayer && direction.magnitude <= enemyFollowPlayer.distanceLimitToFollowPlayer)
            {
                enemyFollowPlayer.FollowTarget(direction);
            }
            else if (randomMoving)
            {
                enemyRandomMovement.RandomMoving();
            }
            else Move(Vector2.zero);
        }
    }
}
