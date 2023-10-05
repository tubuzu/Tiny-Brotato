// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MyMonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer spriteRenderer;

    protected string currentState;

    protected bool canChangeAnim = true;

    protected override void Awake()
    {
        base.Awake();
        transform.GetComponent<SpriteRenderer>().sprite = null;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAnimator();
        LoadSpriteRenderer();
    }

    protected virtual void LoadAnimator()
    {
        if (animator != null) return;
        animator = GetComponent<Animator>();
    }

    protected virtual void LoadSpriteRenderer()
    {
        if (spriteRenderer != null) return;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void ChangeAnimationState(string newState)
    {
        if (currentState == newState || !canChangeAnim) return;

        animator.Play(newState, -1, 0);

        currentState = newState;
    }

    public virtual void SetCanChangeAnim(bool can) => canChangeAnim = can;
}