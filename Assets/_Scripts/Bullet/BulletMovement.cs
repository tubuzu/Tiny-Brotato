using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : BulletAbstract
{
    protected Vector3 startPosition;

    Rigidbody2D _rigidbody;

    protected override void Awake()
    {
        base.Awake();
        _rigidbody = transform.parent.GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 moveDirection, float speed)
    {
        _rigidbody.velocity = speed * moveDirection.normalized;
    }
}
