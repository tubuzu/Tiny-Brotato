// using System;
// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : CharacterMovement
{
    [SerializeField] protected EnemyCtrl enemyCtrl;
    public EnemyCtrl EnemyCtrl { get { return enemyCtrl; } }

    protected Animator _anim;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadEnemyCtrl();
    }
    protected virtual void LoadEnemyCtrl()
    {
        if (!transform.parent.TryGetComponent(out enemyCtrl)) enemyCtrl = transform.GetComponent<EnemyCtrl>();
    }

    protected override void Awake()
    {
        base.Awake();
        _anim = enemyCtrl.Model.GetComponent<Animator>();
        _spriteRenderer = enemyCtrl.Model.GetComponent<SpriteRenderer>();
        _rb = enemyCtrl.GetComponent<Rigidbody2D>();
    }

    // protected override void OnEnable()
    // {
    //     base.OnEnable();
    // }

    public override void Move(Vector2 moveInput, float moveSpeed = -1)
    {
        base.Move(moveInput, moveSpeed);

        enemyCtrl.EnemyAnimation.SetSpeed(_rb.velocity.magnitude);
    }

    public virtual void AddForceToEnemy(Vector2 direction, float strength)
    {
        _rb.AddForce(direction.normalized * strength);
    }
}
