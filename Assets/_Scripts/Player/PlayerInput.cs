using UnityEngine;

public class PlayerInput : PlayerAbstract
{
    private Vector2 input;

    bool isActive = true;

    protected override void OnEnable()
    {
        base.OnEnable();
        GameManager.Instance.OnWaveStart += OnGameStart;
        GameManager.Instance.OnWaveStop += OnGameStop;
    }

    protected virtual void OnDisable()
    {
        GameManager.Instance.OnWaveStart -= OnGameStart;
        GameManager.Instance.OnWaveStop -= OnGameStop;
    }

    private void Update()
    {
        if (!isActive) return;

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        playerCtrl.PlayerMovement.Move(input);
    }

    public void SetCanInput(bool can) => isActive = can;

    void OnGameStart()
    {
        isActive = true;
    }
    void OnGameStop()
    {
        isActive = false;
        StartCoroutine(playerCtrl.PlayerMovement.ChangeVelocity(Vector2.zero));
        playerCtrl.PlayerAnimation.SetSpeed(0f);
    }
}
