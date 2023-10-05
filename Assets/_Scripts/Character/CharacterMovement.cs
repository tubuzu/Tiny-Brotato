using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MyMonoBehaviour
{
    [SerializeField] protected float moveSpeed = 10f;
    [SerializeField] protected float accelerationTime = .1f;
    [SerializeField] protected float decelerationTime = .1f;

    protected Rigidbody2D _rb;
    protected SpriteRenderer _spriteRenderer;

    protected Vector2 moveDirection;
    public Vector2 MoveDirection => moveDirection;
    protected float elapsedTime = 0f;
    protected Vector2 preVelocity;
    protected Vector2 afterVelocity;
    [SerializeField] protected EnumManager.FaceDirection startFaceDir = EnumManager.FaceDirection.RIGHT;
    protected EnumManager.FaceDirection curFaceDir;
    public EnumManager.FaceDirection CurFaceDir => curFaceDir;

    public virtual void Move(Vector2 direction, float moveSpeed = -1)
    {
        if (moveSpeed == -1) moveSpeed = this.moveSpeed;

        if (moveDirection != direction.normalized)
        {
            moveDirection = direction.normalized;
            elapsedTime = 0f;
            preVelocity = _rb.velocity;
            afterVelocity = moveDirection * moveSpeed;

            FlipCharacter();
        }

        if (elapsedTime <= accelerationTime)
        {
            _rb.velocity = Vector2.Lerp(preVelocity, afterVelocity, elapsedTime / accelerationTime);
            elapsedTime += Time.deltaTime;
        }
        else _rb.velocity = afterVelocity;
    }

    public virtual IEnumerator ChangeVelocity(Vector2 direction, float moveSpeed = -1)
    {
        if (moveSpeed == -1) moveSpeed = this.moveSpeed;

        moveDirection = direction.normalized;
        elapsedTime = 0f;
        preVelocity = _rb.velocity;
        afterVelocity = moveDirection * moveSpeed;

        FlipCharacter();

        while (elapsedTime <= accelerationTime)
        {
            _rb.velocity = Vector2.Lerp(preVelocity, afterVelocity, elapsedTime / accelerationTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _rb.velocity = afterVelocity;
    }

    public virtual void FlipCharacter()
    {
        if (moveDirection.x > 0) curFaceDir = startFaceDir == EnumManager.FaceDirection.RIGHT ? EnumManager.FaceDirection.RIGHT : EnumManager.FaceDirection.LEFT;
        if (moveDirection.x < 0) curFaceDir = startFaceDir == EnumManager.FaceDirection.RIGHT ? EnumManager.FaceDirection.LEFT : EnumManager.FaceDirection.RIGHT;
        _spriteRenderer.flipX = curFaceDir == EnumManager.FaceDirection.LEFT;
    }
}
