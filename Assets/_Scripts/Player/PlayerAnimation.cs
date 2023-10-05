using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : CharacterAnimation
{
    [SerializeField] protected PlayerCtrl playerCtrl;
    public PlayerCtrl PlayerCtrl { get { return playerCtrl; } }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayerCtrl();
    }
    protected virtual void LoadPlayerCtrl()
    {
        if (playerCtrl != null) return;
        playerCtrl = transform.parent.GetComponent<PlayerCtrl>();
    }

    WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    Color originalColor = Color.white;
    Color hurtColor = Color.red;

    public void SetSpeed(float speed)
    {
        if (speed > 0.2f) ChangeAnimationState(PlayerAnimationState.Move.ToString());
        else ChangeAnimationState(PlayerAnimationState.Idle.ToString());
    }

    public void Die()
    {
        ChangeAnimationState(PlayerAnimationState.Die.ToString());
        SetCanChangeAnim(false);
    }

    public void PlayHitAnimation()
    {
        StartCoroutine(HitAnimationCoroutine());
    }

    IEnumerator HitAnimationCoroutine()
    {
        float timer = 0;
        float elapsed = 0.25f;

        while (timer <= elapsed)
        {
            timer += Time.fixedDeltaTime;
            float lerpAmount = timer / elapsed;
            spriteRenderer.color = Color.Lerp(originalColor, hurtColor, lerpAmount);

            yield return waitForFixedUpdate;
        }
        timer = 0;
        while (timer <= elapsed)
        {
            timer += Time.fixedDeltaTime;
            float lerpAmount = timer / elapsed;
            spriteRenderer.color = Color.Lerp(hurtColor, originalColor, lerpAmount);

            yield return waitForFixedUpdate;
        }

        spriteRenderer.color = originalColor;
    }
}

enum PlayerAnimationState
{
    Idle,
    Move,
    Die,
}