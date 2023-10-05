// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    [SerializeField] protected PlayerCtrl playerCtrl;
    public PlayerCtrl PlayerCtrl { get { return playerCtrl; } }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayerCtrl();
        LoadRigidbody();
        LoadSpriteRenderer();
    }
    protected virtual void LoadPlayerCtrl()
    {
        if (playerCtrl != null) return;
        playerCtrl = transform.parent.GetComponent<PlayerCtrl>();
    }
    protected virtual void LoadRigidbody()
    {
        if (_rb != null) return;
        _rb = playerCtrl.GetComponent<Rigidbody2D>();
    }
    protected virtual void LoadSpriteRenderer()
    {
        if (_spriteRenderer != null) return;
        _spriteRenderer = playerCtrl.PlayerAnimation.GetComponent<SpriteRenderer>();
    }

    public override void Move(Vector2 moveInput, float moveSpeed = -1)
    {
        base.Move(moveInput, moveSpeed);

        playerCtrl.PlayerAnimation.SetSpeed(_rb.velocity.magnitude);
    }
}
